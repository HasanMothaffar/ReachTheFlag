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

        private static BoardCell[][] convertListOfListsToJaggedArray(List<List<BoardCell>> list)
        {
            BoardCell[][] board = new BoardCell[list.Count][];

            for (int i = 0; i < list.Count; i++)
            {
                board[i] = list[i].ToArray();
            }

            return board;
        }

        public static GameBoard ParseInputBoard(string boardFilename)
        {

            string[] board = readBoardFile(boardFilename);

            List<List<BoardCell>> result = new();

            for (int i = 0; i < board.Length; i++)
            {
                List<BoardCell> cellsList = new();

                // Convert board[i] to an array of characters
                string[] characters = board[i].Select(c => c.ToString()).ToArray();

                for (int j = 0; j < characters.Length; j++)
                {
                    cellsList.Add(CellFactory.GetCell(i, j, characters[j]));
                }

                result.Add(cellsList);
            }

            BoardCell[][] cellsArray = convertListOfListsToJaggedArray(result);

            return new GameBoard(cellsArray);
        }
    }
}
