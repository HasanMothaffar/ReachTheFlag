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
            base.OnPlayerLeave();
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
            this.Symbol = CellPrintSymbols.Player;
            base.OnPlayerEnter();
        }
    }
}
