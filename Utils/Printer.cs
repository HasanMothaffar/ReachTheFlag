using ReachTheFlag.Cells;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using System.Diagnostics;

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
            if (cells.Count == 0) return;

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

                // Print a new line every 10 items to make output more readable
                if (i % 10 == 0) Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void PrintStatistics(SolvedGameStatistics statistics)
        {
            Console.WriteLine("Elapsed time: {0}s", statistics.ElapsedSeconds);

            if (statistics.NumberOfVisitedNodes is not null) Console.WriteLine($"Number of visited nodes: {statistics.NumberOfVisitedNodes}");
            if (statistics.SolutionDepth is not null) Console.WriteLine($"Solution Depth: {statistics.SolutionDepth}");
            if (statistics.NumberOfPlayerMoves is not null) Console.WriteLine($"Number of moves: {statistics.NumberOfPlayerMoves}");
            if (statistics.ShortestPathCost is not null) Console.WriteLine($"Shortest Path Cost: {statistics.ShortestPathCost}");

            PrintPlayerPath(statistics.PlayerPath);

            // Shortest path array
            if (statistics.ShortestPathsArray is not null)
            {
                Console.WriteLine("All distances from player:\n");
                foreach (var row in statistics.ShortestPathsArray)
                {
                    foreach (var entry in row)
                    {
                        // https://stackoverflow.com/questions/46458165/printing-out-array-as-a-table-c-sharp
                        Console.Write(string.Format("{0, -11} ", entry));
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
