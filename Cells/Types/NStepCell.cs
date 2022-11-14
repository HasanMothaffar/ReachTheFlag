using ReachTheFlag.Cells;

namespace ReachTheFlag.Cells
{
    class NStepCell : BoardCell
    {
        private int allowedNumberOfSteps;

        public NStepCell(int x, int y, int allowedNumberOfSteps = 1) : base(x, y)
        {
            this.Symbol = CellPrintSymbols.NStep;
            this.allowedNumberOfSteps = allowedNumberOfSteps;
        }

        public override void OnPlayerEnter()
        {
            this.Symbol = CellPrintSymbols.Player;
            this.Color = System.ConsoleColor.Yellow;
            this.IsVisited = true;
        }

        public override void OnPlayerLeave()
        {
            this.Symbol = CellPrintSymbols.NStep;
            this.allowedNumberOfSteps--;

            if (this.allowedNumberOfSteps == 0)
            {
                this.Color = System.ConsoleColor.Red;
            }
        }

        public override bool CanBeVisited()
        {
            return this.allowedNumberOfSteps > 0;
        }

        public override bool IsValid()
        {
            return !this.CanBeVisited();
        }

        public override BoardCell Clone()
        {
            BoardCell cell = CellFactory.GetCell(X, Y, this.allowedNumberOfSteps.ToString());

            return base.CopyBasePropertiesToCell(cell);
        }

        public override bool Equals(object obj)
        {
            if (obj is not NStepCell other) return false;

            return other.allowedNumberOfSteps == this.allowedNumberOfSteps && base.Equals(obj);
        }
    }
}
