using ReachTheFlag.Cells;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
	public class GameBoard : ICloneable<GameBoard>
	{
		private BoardCell[][] _cells;

		public readonly int RowsCount;
		public readonly BoardCell FlagCell;

		public GameBoard(BoardCell[][] cells)
		{
			_cells = cells;
			RowsCount = cells.Count();
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
			return (x >= 0 && y >= 0 && x < RowsCount && y < _cells[x].Length);
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
			BoardCell[][] allCells = new BoardCell[RowsCount][];

			for (var i = 0; i < RowsCount; i++)
			{
				int columnsCountForRow = _cells[i].Length;
				allCells[i] = new BoardCell[columnsCountForRow];

				for (var j = 0; j < columnsCountForRow; j++)
				{
					allCells[i][j] = _cells[i][j].Clone();
				}
			}

			return allCells;
		}

		public GameBoard Clone()
		{
			BoardCell[][] cellClones = GetAllCells();
			return new GameBoard(cellClones);
		}
	}
}
