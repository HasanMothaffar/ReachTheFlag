namespace ReachTheFlag.Cells
{
    class FlagCell : BoardCell
    {
        private static ConsoleColor _onPlayerEnterColor = ConsoleColor.Green;
        private static ConsoleColor _originalColor = ConsoleColor.Magenta;

        public FlagCell(int x, int y, int weight = 1) : base(x, y, weight)
        {
            Symbol = CellPrintSymbols.Flag;
            Color = _originalColor;
            IsFlag = true;
        }

        public override void OnPlayerLeave()
        {
            Color = _originalColor;
            base.OnPlayerLeave();
        }

        public override bool CanBeVisited()
        {
            return true;
        }

        public override bool IsValid()
        {
            // Player should be standing on this cell to be considered valid
            return IsPlayerVisiting;
        }

        public override void OnPlayerEnter()
        {
            Color = _onPlayerEnterColor;
            base.OnPlayerEnter();
        }
    }
}
