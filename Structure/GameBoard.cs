using ReachTheFlag.Cells;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
    public class GameBoard : ICloneable<GameBoard>
    {
        private BoardCell[,] _cells;

        public readonly int RowsCount;
        public readonly int ColumnsCount;

        public GameBoard(BoardCell[,] cells)
        {
            _cells = cells;
            RowsCount = cells.GetLength(0);
            ColumnsCount = cells.GetLength(1);
        }

        public bool IsCellWithinBoundaries(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < RowsCount && y < ColumnsCount);
        }

        public bool AreAllCellsValid()
        {
            foreach (var cell in _cells)
            {
                if (!cell.IsValid()) return false;
            }

            return true;
        }

        public BoardCell? GetCell(int x, int y)
        {
            return IsCellWithinBoundaries(x, y) ? _cells[x, y] : null;
        }

        public BoardCell[,] GetAllCells()
        {
            BoardCell[,] allCells = new BoardCell[RowsCount, ColumnsCount];

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    allCells[i, j] = _cells[i, j].Clone();
                }
            }

            return allCells;
        }

        public GameBoard Clone()
        {
            BoardCell[,] cellClones = GetAllCells();
            return new GameBoard(cellClones);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not GameBoard other) return false;

            if (!(other.ColumnsCount == ColumnsCount && other.RowsCount == RowsCount)) return false;

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    if (!_cells[i, j].Equals(other._cells[i, j])) return false;
                }
            }

            return true;
        }
    }
}
