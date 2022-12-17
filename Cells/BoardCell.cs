using ReachTheFlag.Game;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Cells
{
    public abstract class BoardCell : ICloneable<BoardCell>
    {
        private static Dictionary<MoveDirection, string> _moveDirectionSymbols;

        public readonly int X;
        public readonly int Y;
        public readonly int Weight;

        public bool IsFlag { get; protected set; }
        public string Symbol { get; protected set; }
        public string NextMoveSymbol { get; protected set; }
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
            IsFlag = false;

            Symbol = "default";
            Color = ConsoleColor.White;

            _moveDirectionSymbols = new()
            {
                {MoveDirection.Left, "👈" },
                {MoveDirection.Right, "👉" },

                {MoveDirection.Up, "👆" },
                {MoveDirection.Down, "👇" },
            };
        }

        public virtual void OnPlayerEnter()
        {
            IsVisited = true;
            IsPlayerVisiting = true;
        }

        public virtual void OnPlayerLeave(MoveDirection direction)
        {
            IsPlayerVisiting = false;
            NextMoveSymbol = _moveDirectionSymbols.GetValueOrDefault(direction);
        }

        public abstract bool CanBeVisited();
        public abstract bool IsValid();

        public virtual BoardCell Clone()
        {
            return (BoardCell)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
