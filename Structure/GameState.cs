﻿using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{

    public class GameState: ICloneable<GameState>
    {
        public readonly GameBoard Board;
        public readonly Point PlayerPosition;
        public readonly PlayerPath PlayerPath;
        
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

            this.PlayerPosition = Board.GetPlayerPosition();
            if (this.PlayerPosition is null)
            {
                throw new Exception("Player cell is missing in board. Please provide a valid board.");
            }

            this.PlayerPath = new PlayerPath();

            BoardCell playerCell = this.Board.GetCell(PlayerPosition.X, PlayerPosition.Y);
            playerCell.OnPlayerEnter();
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

        private Point getNextCellPosition(MoveDirection direction)
        {
            (int dx, int dy) = this._velocityVectors[direction];

            int x = dx + PlayerPosition.X;
            int y = dy + PlayerPosition.Y;

            /**
			 * The board's layout is like this:
			 * 
			 * (0, 0) (0, 1) (0, 2)
			 * (1, 0) (1, 1) (1, 2)
			 * (2, 0) (2, 1) (2, 2)
			 */
            return new Point(x, y);
        }

        private bool canPlayerMoveToDirection(MoveDirection direction)
        {
            Point nextCellPosition = this.getNextCellPosition(direction);
            BoardCell? cell = Board.GetCell(nextCellPosition.X, nextCellPosition.Y);

            if (cell is null) return false;
            return cell.CanBeVisited();
        }

        public void ShiftPlayerPosition(MoveDirection direction)
        {
            if (this.canPlayerMoveToDirection(direction))
            {
                Board.GetCell(PlayerPosition.X, PlayerPosition.Y).OnPlayerLeave();

                Point nextCellPosition = this.getNextCellPosition(direction);
                PlayerPosition.X = nextCellPosition.X;
                PlayerPosition.Y = nextCellPosition.Y;

                BoardCell nextCell = Board.GetCell(nextCellPosition.X, nextCellPosition.Y);

                PlayerPath.AddCell(nextCell);
                nextCell.OnPlayerEnter();
            }
        }

        public Dictionary<MoveDirection, GameState> GetAllPossibleStates()
        {
            Dictionary<MoveDirection, GameState> allPossibleStates = new Dictionary<MoveDirection, GameState>();

            foreach (MoveDirection direction in Enum.GetValues(typeof(MoveDirection)))
            {
                if (this.canPlayerMoveToDirection(direction))
                {
                    GameBoard boardClone = this.Board.Clone();
                    GameState possibleState = new GameState(boardClone);

                    possibleState.ShiftPlayerPosition(direction);

                    allPossibleStates.Add(direction, possibleState);
                }
            }

            return allPossibleStates;
        }

        public GameState Clone()
        {
            return new GameState(this.Board.Clone());
        }

        public override bool Equals(object obj)
        {
            if (obj is not GameState other) return false;

            return this.Board.Equals(other.Board);
        }
    }
}
