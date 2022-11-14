using ReachTheFlag.Cells;
using ReachTheFlag.Structure;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        private static BoardCell[,] convertListOfListsTo2DArray(List<List<BoardCell>> list)
        {
            BoardCell[,] board = new BoardCell[list.Count, list[0].Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    board[i, j] = list[i][j];
                }
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

            BoardCell[,] cellsArray = convertListOfListsTo2DArray(result);

            return new GameBoard(cellsArray);
        }
    }
}
