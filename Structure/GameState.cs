using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
    public class GameState: ICloneable<GameState>
    {
        public BoardCell PlayerCell { get; protected set; }
        public readonly GameBoard Board;

        public int Weight => PlayerCell.Weight;
        public bool IsFlag => PlayerCell.IsFlag;
        public int X => PlayerCell.X;
        public int Y => PlayerCell.Y;

        // dx and dy pairs
        private readonly Dictionary<MoveDirection, (int, int)> _velocityVectors = new()
        {
            { MoveDirection.Left, (0, -1) },
            { MoveDirection.Right, (0, 1) },
            { MoveDirection.Up, (-1, 0) },
            { MoveDirection.Down, (1, 0) },
        };

        public GameState(GameBoard board)
        {
            this.Board = board;
            BoardCell playerCell = this.Board.GetPlayerCell();

            if (playerCell is null)
            {
                throw new Exception("Player cell is missing in board. Please provide a valid board.");
            }

            this.PlayerCell = playerCell;
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
            (int dx, int dy) = this._velocityVectors[direction];

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

            if (cell is null) return false;
            return cell.CanBeVisited();
        }

        public void ShiftPlayerPosition(MoveDirection direction)
        {
            if (this.canPlayerMoveToDirection(direction))
            {
                PlayerCell.OnPlayerLeave();

                BoardCell nextPlayerCell = this.getNextPlayerCell(direction);
                PlayerCell = nextPlayerCell;

                nextPlayerCell.OnPlayerEnter();
            }
        }

        public Dictionary<MoveDirection, GameState> GetAllNeighboringStates()
        {
            Dictionary<MoveDirection, GameState> neighbors = new Dictionary<MoveDirection, GameState>();

            foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
            {
                if (this.canPlayerMoveToDirection(direction))
                {
                    GameBoard boardClone = this.Board.Clone();
                    GameState possibleState = new GameState(boardClone);

                    possibleState.ShiftPlayerPosition(direction);

                    neighbors.Add(direction, possibleState);
                }
            }

            return neighbors;
        }

        public GameState Clone()
        {
            return new GameState(this.Board.Clone());
        }
    }
}
