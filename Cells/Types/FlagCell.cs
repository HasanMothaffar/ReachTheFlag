using System;

namespace ReachTheFlag.Cells
{
    class FlagCell : BoardCell
    {
        public FlagCell(int x, int y) : base(x, y)
        {
            this.Symbol = CellPrintSymbols.Flag;
            this.Color = ConsoleColor.Magenta;
        }

        public override void OnPlayerLeave()
        {
            this.Color = ConsoleColor.Magenta;
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
            this.Color = ConsoleColor.Green;
        }

        public override BoardCell Clone()
        {
            BoardCell cell = CellFactory.GetCell(X, Y, CellTypes.Flag);

            return base.CopyBasePropertiesToCell(cell);
        }
    }
}
