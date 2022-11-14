using ReachTheFlag.Utils;

namespace ReachTheFlag.Cells
{
    public abstract class BoardCell : ICloneable<BoardCell>
    {
        public string Symbol;
        public ConsoleColor Color = ConsoleColor.White;

        public bool IsVisited = false;
        public bool IsPlayerVisiting = false;

        public int X { get; init; }
        public int Y { get; init; }

        public BoardCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public virtual void OnPlayerEnter() {
            this.IsVisited = true;
            this.IsPlayerVisiting = true;
        }

        public virtual void OnPlayerLeave() {
            this.IsPlayerVisiting = false;
        }

        public abstract bool CanBeVisited();
        public abstract bool IsValid();

        public BoardCell Clone()
        {
            return (BoardCell)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is not BoardCell other) return false;

            return ObjectComparerUtility.ObjectsAreEqual(this, other);
        }
    }
}
