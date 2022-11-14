using ReachTheFlag.Utils;
using System;

namespace ReachTheFlag.Cells
{
    public abstract class BoardCell : ICloneable<BoardCell>
    {
        public string Symbol;
        public ConsoleColor Color = ConsoleColor.White;

        public bool IsVisited = false;

        protected int X;
        protected int Y;

        public BoardCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public abstract void OnPlayerEnter();
        public abstract void OnPlayerLeave();
        public abstract bool CanBeVisited();
        public abstract bool IsValid();


        /**
         * Derived classes should implement this function and then call
         * CopyBasePropertiesToCell(cell)
         **/
        public abstract BoardCell Clone();

        public BoardCell CopyBasePropertiesToCell(BoardCell cell)
        {
            cell.IsVisited = this.IsVisited;
            cell.Symbol = this.Symbol;
            cell.Color = this.Color;

            return cell;
        }

        public override bool Equals(object obj)
        {
            if (obj is not BoardCell other) return false;

            return (IsVisited, Symbol, Color) == (other.IsVisited, other.Symbol, other.Color);
        }
    }
}
