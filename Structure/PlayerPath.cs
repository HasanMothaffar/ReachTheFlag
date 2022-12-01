using ReachTheFlag.Cells;

namespace ReachTheFlag.Structure
{
    public class PlayerPath
    {
        private readonly List<BoardCell> _cells = new();
        public void AddCell(BoardCell cell)
        {
            _cells.Add(cell);
        }

        public List<BoardCell> GetCells()
        {
            return _cells;
        }
    }
}
