namespace ReachTheFlag.Cells
{
    class GapCell : BoardCell
    {
        public GapCell(int x, int y) : base(x, y, 0)
        {
            this.Symbol = CellPrintSymbols.Gap;
            this.Color = ConsoleColor.Blue;
        }

        public override void OnPlayerLeave()
        {

        }

        public override bool CanBeVisited()
        {
            return false;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void OnPlayerEnter()
        {

        }
    }
}
