using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
    public class GameState: ICloneable<GameState>
    {
        private BoardCell _playerCell;

        public readonly GameBoard Board;
        public readonly PlayerPath PlayerPath;
        public GameState ParentState { get; set; }

        public int Weight => _playerCell.Weight;
        public int X => _playerCell.X;
        public int Y => _playerCell.Y;

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

            this._playerCell = playerCell;

            this.PlayerPath = new PlayerPath();
            PlayerPath.AddCell(playerCell);
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

            int x = dx + _playerCell.X;
            int y = dy + _playerCell.Y;

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
                _playerCell.OnPlayerLeave();

                BoardCell nextPlayerCell = this.getNextPlayerCell(direction);
                _playerCell = nextPlayerCell;

                PlayerPath.AddCell(nextPlayerCell);
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
