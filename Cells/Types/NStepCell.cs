namespace ReachTheFlag.Cells
{
    class NStepCell : BoardCell
    {
        private int allowedNumberOfSteps;

        private static ConsoleColor _playerIsVisitingColor = ConsoleColor.Yellow;
        private static ConsoleColor _noMoreAllowedStepsColor = ConsoleColor.Red;

        public NStepCell(int x, int y, int allowedNumberOfSteps = 1, int weight = 1) : base(x, y, weight)
        {
            this.Symbol = allowedNumberOfSteps.ToString();
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
            allowedNumberOfSteps--;
            Symbol = allowedNumberOfSteps.ToString();

            if (this.allowedNumberOfSteps == 0)
            {
                this.Color = _noMoreAllowedStepsColor;
            }

            base.OnPlayerLeave();
        }

        public override bool CanBeVisited() => allowedNumberOfSteps > 0;
        public override bool IsValid() => !CanBeVisited();
    }
}
