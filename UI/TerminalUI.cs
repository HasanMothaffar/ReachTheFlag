using ReachTheFlag.Cells;
using ReachTheFlag.Logic;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.ExtensionMethods;
using ReachTheFlag.Game;

namespace ReachTheFlag.UI
{
    public class TerminalUI : GameUI
    {
        private string? level;

        private readonly ReachTheFlagGame _game;
        private static Dictionary<CellColor, ConsoleColor> _cellColors = new()
        {
            { CellColor.Green, ConsoleColor.Green },
            { CellColor.Yellow, ConsoleColor.Yellow },
            { CellColor.Blue, ConsoleColor.Blue },
            { CellColor.Red, ConsoleColor.Red },
            { CellColor.Magenta, ConsoleColor.Magenta },
            { CellColor.White, ConsoleColor.White },
        };

        public TerminalUI(ReachTheFlagGame game)
        {
            _game = game;
        }

        public override void Run()
        {
            while (true)
            {
                if (level is null)
                {
                    DisplayLevelsToChooseFrom();
                    continue;
                }

                DisplayBoard(_game.CurrentState.Board);

                SolverStrategy strategy = GetSolverStrategyFromUser();
                GameStatistics statistics = _game.Solve(strategy, this);
                DisplayGameStatistics(statistics);

                Console.WriteLine("Press r to restart the game, or any other key to quit.");

                char pressedKey = Console.ReadKey(true).KeyChar;
                string pressedKeyInLowercase = pressedKey.ToString().ToLower();

                if (pressedKeyInLowercase == "r")
                {
                    Console.Clear();
                    _game.Restart();
                }

                else
                {
                    break;
                }
            }
        }
        public override void DisplayBoard(GameBoard board)
        {
            BoardCell[][] cells = board.GetAllCells();
            int rowsCount = cells.Length;

            ConsoleColor originalConsoleForgroundColor = Console.ForegroundColor;

            for (var i = 0; i < rowsCount; i++)
            {
                int columnsCount = cells[i].Length;
                for (var j = 0; j < columnsCount; j++)
                {

                    if (_cellColors.ContainsKey(cells[i][j].Color))
                    {
                        Console.ForegroundColor = _cellColors.GetValueOrDefault(cells[i][j].Color);
                    }

                    Console.Write("{0} ", cells[i][j].Symbol);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = originalConsoleForgroundColor;
            Console.WriteLine();
        }

        public override void DisplayBoardInPosition(GameBoard board, int x, int y)
        {
            DisplayBoard(board);
        }

        public override void DisplayGameStatistics(GameStatistics statistics)
        {
            if (statistics.Status == GameStatus.Win) Console.WriteLine("You won!");
            else if (statistics.Status == GameStatus.PlayerIsStuck) Console.WriteLine("Player is stuck :(");
            else if (statistics.Status == GameStatus.ImpossibleToWin) Console.WriteLine("Game is impossible to win.");

            Console.WriteLine("Elapsed time: {0}s", statistics.ElapsedSeconds);

            if (statistics.NumberOfVisitedNodes > 0) Console.WriteLine($"Number of visited nodes: {statistics.NumberOfVisitedNodes}");
            if (statistics.SolutionDepth > 0) Console.WriteLine($"Solution Depth: {statistics.SolutionDepth}");
            if (statistics.NumberOfPlayerMoves > 0) Console.WriteLine($"Number of moves: {statistics.NumberOfPlayerMoves}");
            if (statistics.ShortestPathCost > 0) Console.WriteLine($"Shortest Path Cost: {statistics.ShortestPathCost}");

            DisplayBoard(statistics.FinalState.Board);

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

        public override void DisplayAvailableStrategies()
        {
            Console.WriteLine("Choose solving strategy:");
            foreach (SolverStrategy solverType in Enum.GetValues(typeof(SolverStrategy)))
            {
                Console.WriteLine($"{(int)solverType}: {solverType.DisplayName()}");
            }

            Console.WriteLine("-----------");
        }

        public SolverStrategy GetSolverStrategyFromUser()
        {
            DisplayAvailableStrategies();

            char pressedKey = Console.ReadKey(true).KeyChar;
            string type = pressedKey.ToString();

            Console.Clear();


            if (Enum.TryParse(type, out SolverStrategy solverStrategy) && Enum.IsDefined(solverStrategy))
            {
                Console.WriteLine($"Chosen strategy: {solverStrategy.DisplayName()}");
                return solverStrategy;
            }

            else
            {
                Console.WriteLine("Unknown key was input: Falling back to user input strategy.");
                return SolverStrategy.UserInput;
            }
        }

        public override void DisplayLevelsToChooseFrom()
        {
            Console.WriteLine("Choose one of these levels: ");

            for (var i = 0; i < _game.AvailableMaps.Count; i++)
            {
                var filename = Path.GetFileNameWithoutExtension(_game.AvailableMaps[i].Name);
                GameBoard board = MapParser.ParseBoardMap(_game.AvailableMaps[i].FilePath);

                Console.WriteLine($"{i + 1}. {filename}");
                DisplayBoard(board);
            }

            char pressedKey = Console.ReadKey(true).KeyChar;
            string levelString = pressedKey.ToString();

            Console.Clear();

            if (Int32.TryParse(levelString, out int levelInt))
            {
                if (levelInt >= 1 && levelInt <= _game.AvailableMaps.Count)
                {
                    var map = _game.AvailableMaps[levelInt - 1];
                    Console.WriteLine($"Chosen Level: {map.Name}");

                    level = map.FilePath;
                    _game.SetMap(level);
                }
            }

        }
    }
}
