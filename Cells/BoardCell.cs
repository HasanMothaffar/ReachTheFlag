using ReachTheFlag.Utils;

namespace ReachTheFlag.Cells
{
    public abstract class BoardCell : ICloneable<BoardCell>, IEquatable<BoardCell?>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Weight;
        public string Symbol { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public bool IsVisited { get; private set; }
        public bool IsPlayerVisiting { get; private set; }

        public BoardCell(int x, int y, int weight = 1)
        {
            X = x;
            Y = y;
            Weight = weight;

            IsPlayerVisiting = false;
            IsVisited = false;
            Symbol = "default";
            Color = ConsoleColor.White;
        }

        public virtual void OnPlayerEnter()
        {
            IsVisited = true;
            IsPlayerVisiting = true;
        }

        public virtual void OnPlayerLeave()
        {
            IsPlayerVisiting = false;
        }

        public abstract bool CanBeVisited();
        public abstract bool IsValid();

        public BoardCell Clone()
        {
            return (BoardCell)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BoardCell);
        }

        public bool Equals(BoardCell? other)
        {
            return other is not null &&
                   Symbol == other.Symbol &&
                   Color == other.Color &&
                   IsVisited == other.IsVisited &&
                   IsPlayerVisiting == other.IsPlayerVisiting &&
                   X == other.X &&
                   Y == other.Y &&
                   Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Weight);
        }
    }
}
