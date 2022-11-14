using System;

namespace ReachTheFlag.Cells
{
    class InfiniteStepCell : BoardCell
    {
        public InfiniteStepCell(int x, int y) : base(x, y)
        {
            this.Symbol = CellPrintSymbols.InfiniteStep;
        }

        public override void OnPlayerLeave()
        {
            this.Symbol = CellPrintSymbols.InfiniteStep;
        }

        public override bool CanBeVisited()
        {
            return true;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void OnPlayerEnter()
        {
            this.IsVisited = true;
        }

        public override BoardCell Clone()
        {
            BoardCell cell = CellFactory.GetCell(X, Y, CellTypes.InfiniteStep);

            return base.CopyBasePropertiesToCell(cell);
        }
    }
}
