using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Utils;
using System.Text;

namespace ReachTheFlag.Structure
{
    public class GameState: ICloneable<GameState>
    {
        private readonly StringBuilder _id;
        public string ID => _id.ToString();
        public BoardCell PlayerCell { get; protected set; }
        public readonly GameBoard Board;

        public int Weight => PlayerCell.Weight;
        public bool IsFlag => PlayerCell.IsFlag;
        public int X => PlayerCell.X;
        public int Y => PlayerCell.Y;

        // dx and dy pairs
        private static Dictionary<MoveDirection, (int, int)> _velocityVectors = new()
        {
            { MoveDirection.Left, (0, -1) },
            { MoveDirection.Right, (0, 1) },
            { MoveDirection.Up, (-1, 0) },
            { MoveDirection.Down, (1, 0) },
        };

        public GameState(GameBoard board)
        {
            Board = board;
            PlayerCell = Board.GetPlayerCell();

            // Don't generate ID before initializing Board
            _id = new StringBuilder();
            generateID();
        }

        private void generateID()
        {
            _id.Clear();
            BoardCell[][] cells = Board.GetAllCells();
            for (var i = 0; i < cells.Length; i++)
            {
                for (var j = 0; j < cells[i].Length; j++)
                {
                    char isValid = cells[i][j].IsValid() ? '1' : '0';
                    char isVisited = cells[i][j].IsVisited ? '1' : '0';
                    char isPlayerVisiting = cells[i][j].IsPlayerVisiting ? '1' : '0';
                    _id.Append($"{isValid}{isVisited}{isPlayerVisiting}, ");
                }
                _id.Append('\n');

            }
            _id.Append('\n');

        }

        public bool IsFinal()
        {
            return Board.AreAllCellsValid();
        }

        public bool IsPlayerStuck()
        {
            bool canPlayerMove = false;

            foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
            {
                canPlayerMove = canPlayerMove || this.canPlayerMoveToDirection(direction);
            }

            return !canPlayerMove;
        }

        private BoardCell getNextPlayerCell(MoveDirection direction)
        {
            (int dx, int dy) = _velocityVectors[direction];

            int x = dx + this.X;
            int y = dy + this.Y;

            /**
			 * The board's layout is like this:
			 * 
			 * (0, 0) (0, 1) (0, 2)
			 * (1, 0) (1, 1) (1, 2)
			 * (2, 0) (2, 1) (2, 2)
			 */

            return Board.GetCell(x, y);
        }

        private bool canPlayerMoveToDirection(MoveDirection direction)
        {
            BoardCell? cell = this.getNextPlayerCell(direction);
            return (cell is not null && cell.CanBeVisited());
        }

        public void MovePlayerToDirection(MoveDirection direction)
        {
            if (this.canPlayerMoveToDirection(direction))
            {
                __unsafeMovePlayerToDirection(direction);
            }
        }

        // Don't check if player can move to given direction first. Why? Faster
        private void __unsafeMovePlayerToDirection(MoveDirection direction)
        {
            PlayerCell.OnPlayerLeave();
            PlayerCell = getNextPlayerCell(direction);
            PlayerCell.OnPlayerEnter();

            generateID();
        }

        public Dictionary<MoveDirection, GameState> GetAllNeighboringStates()
        {
            Dictionary<MoveDirection, GameState> neighbors = new ();

            foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
            {
                if (canPlayerMoveToDirection(direction))
                {
                    GameState possibleState = Clone();
                    possibleState.__unsafeMovePlayerToDirection(direction);
                    neighbors.Add(direction, possibleState);
                }
            }

            return neighbors;
        }

        public GameState Clone()
        {
            return new GameState(Board.Clone());
        }
    }
}
