using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Utils
{
    public static class Printer
    {
        public static void PrintBoard(GameBoard board)
        {
            int rowsCount = board.RowsCount;
            int columnsCount = board.ColumnsCount;

            BoardCell[,] cells = board.GetAllCells();

            ConsoleColor originalConsoleForgroundColor = Console.ForegroundColor;

            for (var i = 0; i < rowsCount; i++)
            {
                for (var j = 0; j < columnsCount; j++)
                {
                    Console.ForegroundColor = cells[i, j].Color;
                    Console.Write("{0} ", cells[i, j].Symbol);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = originalConsoleForgroundColor;
        }

        public static void PrintPlayerPath(List<MoveDirection> playerPath)
        {
            foreach (var direction in playerPath)
            {
                Console.WriteLine(direction);
            }
        }

        public static void PrintState(GameState state)
        {
            PrintBoard(state.Board);
            PrintPlayerPath(state.GetPlayerPath());
        }
    }
}
