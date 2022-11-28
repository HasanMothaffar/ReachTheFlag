using Microsoft.VisualBasic;
using ReachTheFlag.Cells;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Game
{
    public class MapParser
    {
        private static int _flagCellsCount = 0;
        private static int _playerCellsCount = 0;

        private static string[] readBoardMapFile(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename);
            }

            throw new FileNotFoundException("File " + filename + " was not found.");
        }

        private static void validateFlagAndPlayerCellsCount(string cellType)
        {
            if (cellType == CellTypes.Player) _playerCellsCount++;
            else if (cellType == CellTypes.Flag) _flagCellsCount++;

            if (_playerCellsCount > 1) throw new Exception("Please provide only one player cell.");
            if (_flagCellsCount > 1) throw new Exception("Please provide only one flag cell.");
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
                    validateFlagAndPlayerCellsCount(cellType);

                    cellsArray[i][j] = CellFactory.GetCell(i, j, cellType, allowedNumberOfSteps, weight);
                }
            }

            return new GameBoard(cellsArray);
        }
    }
}
