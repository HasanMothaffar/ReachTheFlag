using ReachTheFlag.Cells;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
	public class GameBoard : ICloneable<GameBoard>
	{
		private BoardCell[][] _cells;
		public readonly BoardCell FlagCell;

		public GameBoard(BoardCell[][] cells)
		{
			_cells = cells;
			FlagCell = getFlagCell();
		}

        private BoardCell? getFlagCell()
        {
            foreach (var row in _cells)
            {
                foreach (var cell in row)
                {
                    if (cell.Symbol == CellPrintSymbols.Flag)
                    {
                        return cell;
                    }
                }
            }

            return null;
        }

        public bool IsCellWithinBoundaries(int x, int y)
		{
			return (x >= 0 && y >= 0 && x < _cells.Length && y < _cells[x].Length);
		}

		public bool AreAllCellsValid()
		{
			foreach (var row in _cells)
			{
				foreach (var cell in row)
				{
                    if (!cell.IsValid()) return false;
                }
			}

			return true;
		}

		public BoardCell? GetCell(int x, int y)
		{
			return IsCellWithinBoundaries(x, y) ? _cells[x][y] : null;
		}

		public BoardCell? GetPlayerCell()
		{
			foreach (var row in _cells)
			{
				foreach (var cell in row)
				{
					if (cell.IsPlayerVisiting)
					{
						return cell;
					}
				}
			}

			return null;
		}

		public BoardCell[][] GetAllCells()
		{
			return _cells;
		}

		public GameBoard Clone()
		{
            BoardCell[][] cellClones = new BoardCell[_cells.Length][];

            for (var i = 0; i < _cells.Length; i++)
            {
                int columnsCountForRow = _cells[i].Length;
                cellClones[i] = new BoardCell[columnsCountForRow];

                for (var j = 0; j < columnsCountForRow; j++)
                {
                    cellClones[i][j] = _cells[i][j].Clone();
                }
            }

            return new GameBoard(cellClones);
		}
	}
}
