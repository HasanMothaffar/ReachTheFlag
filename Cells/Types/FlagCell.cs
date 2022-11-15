using System;

namespace ReachTheFlag.Cells
{
    class FlagCell : BoardCell
    {
        private const ConsoleColor _onPlayerEnterColor = ConsoleColor.Green;
        private const ConsoleColor _originalColor = ConsoleColor.Magenta;

        public FlagCell(int x, int y) : base(x, y)
        {
            this.Symbol = CellPrintSymbols.Flag;
            this.Color = _originalColor;
        }

        public override void OnPlayerLeave()
        {
            this.Color = _originalColor;
            base.OnPlayerLeave();
        }

        public override bool CanBeVisited()
        {
            return true;
        }

        public override bool IsValid()
        {
            // Player should be standing on this cell to be considered valid
            return this.IsPlayerVisiting;
        }

        public override void OnPlayerEnter()
        {
            this.Color = _onPlayerEnterColor;
            base.OnPlayerEnter();
        }
    }
}
