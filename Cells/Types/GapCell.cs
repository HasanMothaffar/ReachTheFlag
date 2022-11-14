using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheFlag.Cells
{
    class GapCell : BoardCell
    {
        public GapCell(int x, int y) : base(x, y)
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

        public override BoardCell Clone()
        {
            BoardCell cell = CellFactory.GetCell(X, Y, CellTypes.Gap);

            return base.CopyBasePropertiesToCell(cell);
        }
    }
}
