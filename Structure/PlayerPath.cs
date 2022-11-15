using ReachTheFlag.Cells;

namespace ReachTheFlag.Structure
{
    public class PlayerPath
    {
        private List<BoardCell> _cells = new();
        public void AddCell(BoardCell cell)
        {
            _cells.Add(cell);
        }

        public List<BoardCell> GetCells()
        {
            List<BoardCell> list = new List<BoardCell>();
            _cells.ForEach(cell => list.Add(cell.Clone()));

            return list;
        }
    }
}
