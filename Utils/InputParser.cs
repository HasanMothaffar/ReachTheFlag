using ReachTheFlag.Cells;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Game
{
    public class InputParser
    {
        private static string[] readBoardFile(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename);
            }

            throw new FileNotFoundException("File " + filename + " was not found.");
        }

        public static GameBoard ParseInputBoard(string boardFilename)
        {

            string[] board = readBoardFile(boardFilename);
            BoardCell[][] cellsArray = new BoardCell[board.Length][];

            for (int i = 0; i < board.Length; i++)
            {
                string[] characters = board[i].Select(c => c.ToString()).ToArray();
                cellsArray[i] = new BoardCell[characters.Length];

                for (int j = 0; j < characters.Length; j++)
                {
                    cellsArray[i][j] = CellFactory.GetCell(i, j, characters[j]);
                }
            }

            return new GameBoard(cellsArray);
        }
    }
}
