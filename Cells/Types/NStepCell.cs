namespace ReachTheFlag.Cells
{
    class NStepCell : BoardCell
    {
        private int allowedNumberOfSteps;

        private const ConsoleColor _playerIsVisitingColor = ConsoleColor.Yellow;
        private const ConsoleColor _noMoreAllowedStepsColor = ConsoleColor.Red;

        public NStepCell(int x, int y, int allowedNumberOfSteps = 1, int weight = 1) : base(x, y, weight)
        {
            this.Symbol = CellPrintSymbols.NStep;
            this.allowedNumberOfSteps = allowedNumberOfSteps;
        }

        public override void OnPlayerEnter()
        {
            this.Symbol = CellPrintSymbols.Player;
            this.Color = _playerIsVisitingColor;
            base.OnPlayerEnter();
        }

        public override void OnPlayerLeave()
        {
            this.Symbol = CellPrintSymbols.NStep;
            this.allowedNumberOfSteps--;

            if (this.allowedNumberOfSteps == 0)
            {
                this.Color = _noMoreAllowedStepsColor;
            }

            base.OnPlayerLeave();
        }

        public override bool CanBeVisited()
        {
            return this.allowedNumberOfSteps > 0;
        }

        public override bool IsValid()
        {
            return !this.CanBeVisited();
        }

        public override bool Equals(object obj)
        {
            if (obj is not NStepCell other) return false;

            return other.allowedNumberOfSteps == this.allowedNumberOfSteps && base.Equals(obj);
        }
    }
}
