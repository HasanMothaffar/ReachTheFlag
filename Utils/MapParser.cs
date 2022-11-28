using ReachTheFlag.Cells;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Game
{
    public class MapParser
    {
        private static string[] readBoardMapFile(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename);
            }

            throw new FileNotFoundException("File " + filename + " was not found.");
        }

        private static (string cellType, int allowedNumberOfSteps, int weight) getCellInfo(string cellString)
        {
            int weight = 0;
            int allowedNumberOfSteps = 0;
            string cellType = cellString[0].ToString();

            if (cellType == CellTypes.Gap) return (cellType, allowedNumberOfSteps, weight);
            if (cellType == CellTypes.Flag)
            {
                weight = int.Parse(cellString[1].ToString());
                return (cellType, allowedNumberOfSteps, weight);
            }

            try
            {
                allowedNumberOfSteps = int.Parse(cellString[1].ToString());
                weight = int.Parse(cellString[2].ToString());
            }
            catch { }

            return (cellType, allowedNumberOfSteps, weight);
        }

        public static GameBoard ParseBoardMap(string boardFilename)
        {
            string[] board = readBoardMapFile(boardFilename);
            BoardCell[][] cellsArray = new BoardCell[board.Length][];

            for (int i = 0; i < board.Length; i++)
            {
                string[] cellsRow = board[i].Split(",");
                cellsArray[i] = new BoardCell[cellsRow.Length];

                for (var j = 0; j < cellsRow.Length; j++)
                {
                    (string cellType, int allowedNumberOfSteps, int weight) = getCellInfo(cellsRow[j]);
                    cellsArray[i][j] = CellFactory.GetCell(i, j, cellType, allowedNumberOfSteps, weight);
                }
            }

            return new GameBoard(cellsArray);
        }
    }
}
