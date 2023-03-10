using ReachTheFlag.Game;

namespace ReachTheFlag.Cells
{
    class FlagCell : BoardCell
    {
        private static CellColor _onPlayerEnterColor = CellColor.Green;
        private static CellColor _originalColor = CellColor.Magenta;

        public FlagCell(int x, int y, int weight = 1) : base(x, y, weight)
        {
            Symbol = CellPrintSymbols.Flag;
            Color = _originalColor;
            IsFlag = true;
        }

        public override void OnPlayerLeave(MoveDirection direction)
        {
            Color = _originalColor;
            base.OnPlayerLeave(direction);
        }

        public override bool CanBeVisited() => true;

        // Player should be standing on this cell to be considered valid
        public override bool IsValid() => IsPlayerVisiting;

        public override void OnPlayerEnter()
        {
            Color = _onPlayerEnterColor;
            base.OnPlayerEnter();
        }
    }
}
