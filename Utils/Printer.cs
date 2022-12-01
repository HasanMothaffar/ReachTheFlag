using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Utils
{
    public static class Printer
    {
        public static void PrintBoard(GameBoard board)
        {
            BoardCell[][] cells = board.GetAllCells();
            int rowsCount = cells.Length;

            ConsoleColor originalConsoleForgroundColor = Console.ForegroundColor;

            for (var i = 0; i < rowsCount; i++)
            {
                int columnsCount = cells[i].Length;
                for (var j = 0; j < columnsCount; j++)
                {
                    Console.ForegroundColor = cells[i][j].Color;
                    Console.Write("{0} ", cells[i][j].Symbol);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = originalConsoleForgroundColor;
            Console.WriteLine();
        }

        public static void PrintPlayerPath(PlayerPath path)
        {
            var cells = path.GetCells();
            Console.Write("Player path: ");

            for (var i = 0; i < cells.Count; i++)
            {
                if (i == cells.Count - 1)
                {
                    Console.Write(cells[i]);
                }

                else
                {
                    Console.Write($"{cells[i]} -> ");
                }
            }

            Console.WriteLine();
        }
    }
}
