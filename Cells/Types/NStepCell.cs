using ReachTheFlag.Game;

namespace ReachTheFlag.Cells
{
    class NStepCell : BoardCell
    {
        private int allowedNumberOfSteps;

        private static CellColor _playerIsVisitingColor = CellColor.Yellow;
        private static CellColor _noMoreAllowedStepsColor = CellColor.Red;

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

        public override void OnPlayerLeave(MoveDirection direction)
        {
            allowedNumberOfSteps--;
            Symbol = allowedNumberOfSteps.ToString();

            if (this.allowedNumberOfSteps == 0)
            {
                this.Color = _noMoreAllowedStepsColor;
            }

            base.OnPlayerLeave(direction);
        }

        public override bool CanBeVisited() => allowedNumberOfSteps > 0;
        public override bool IsValid() => !CanBeVisited();
    }
}
