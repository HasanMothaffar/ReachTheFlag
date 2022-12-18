using Raylib_cs;
using ReachTheFlag.Logic;
using ReachTheFlag.ExtensionMethods;
using ReachTheFlag.Game;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.Cells;

namespace ReachTheFlag.UI
{
    public class RaylibUI : GameUI
    {
        private readonly ReachTheFlagGame _game;
        private SolverStrategy? _solverStrategy;
        private GameStatistics? _gameStatistics;
        private string? level;

        private static Dictionary<CellColor, Color> _cellColors = new()
        {
            { CellColor.Green, Color.GREEN },
            { CellColor.Yellow, Color.YELLOW },
            { CellColor.Blue, Color.BLUE },
            { CellColor.Red, Color.RED },
            { CellColor.Magenta, Color.MAGENTA },
            { CellColor.White, Color.WHITE },
        };

        public readonly int WindowWidth;
        public readonly int WindowHeight;
        public readonly string WindowTitle;

        public RaylibUI(ReachTheFlagGame game, string windowTitle = "Reach The Flag", int windowWidth = 1366, int windowHeight = 720)
        {
            _game = game;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            WindowTitle = windowTitle;
            Raylib.InitWindow(WindowWidth, WindowHeight, WindowTitle);
        }

        private void listenForSolverStrategy()
        {
            int pressedKey = Raylib.GetKeyPressed();
            string type = ((char)pressedKey).ToString();

            //https://stackoverflow.com/questions/6741649/enum-tryparse-returns-true-for-any-numeric-values
            if (Enum.TryParse(type, out SolverStrategy solverStrategy) && Enum.IsDefined(solverStrategy))
            {
                _solverStrategy = solverStrategy;
            }
        }

        private void listenForRestart()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                _game.Restart();
                _solverStrategy = null;
                _gameStatistics = null;
            }
        }

        public override void Run()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.LIGHTGRAY);

                if (level is null)
                {
                    DisplayLevelsToChooseFrom();
                    Raylib.EndDrawing();
                    continue;
                }

                listenForRestart();

                if (_solverStrategy is null)
                {
                    DisplayAvailableStrategies();
                    listenForSolverStrategy();
                }

                else
                {
                    Raylib.DrawText($"Chosen strategy: {_solverStrategy.DisplayName()}", 12, 10, 24, Color.BLACK);
                }

                DisplayBoard(_game.CurrentState.Board);

                bool firstTimeSolving = _gameStatistics is null;
                GameStatus? status = _gameStatistics?.Status;

                if ((firstTimeSolving || status == GameStatus.Pending) && _solverStrategy is not null)
                {
                    _gameStatistics = _game.Solve((SolverStrategy)_solverStrategy, this);
                }

                else if (status == GameStatus.Win)
                {
                    Raylib.DrawText("WIN MAN", 12, 60, 24, Color.BLACK);
                    DisplayGameStatistics(_gameStatistics);
                }

                else if (status == GameStatus.PlayerIsStuck)
                {
                    Raylib.DrawText("LOSE MAN", 12, 60, 24, Color.BLACK);
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        public override void DisplayGameStatistics(GameStatistics statistics)
        {
            DisplayBoard(statistics.FinalState.Board);

            //TerminalUI.DisplayBoard(statistics.FinalState.Board);
            //TerminalUI.DisplayGameStatistics(statistics);
        }

        public override void DisplayBoardInPosition(GameBoard board, int initialPositionX, int initialPositionY)
        {
            var cells = board.GetAllCells();

            int positionX = initialPositionX;
            int positionY = initialPositionY;

            int width = 45;
            int height = 45;

            int horizontalDistanceBetweenCells = width + 10;
            int verticalDistanceBetweenCells = height + 10;

            foreach (var row in cells)
            {
                foreach (var cell in row)
                {
                    Raylib.DrawRectangle(positionX, positionY, width, height, _cellColors.GetValueOrDefault(cell.Color));
                    Raylib.DrawText(cell.Symbol, positionX + 15, positionY + 15, 20, Color.BLACK);
                    positionX += horizontalDistanceBetweenCells;
                }

                positionX = initialPositionX;
                positionY += verticalDistanceBetweenCells;
            }
        }

        public override void DisplayBoard(GameBoard board)
        {
            DisplayBoardInPosition(board, 1000, 10);
        }
        public override void DisplayAvailableStrategies()
        {
            Raylib.DrawText("Choose solving strategy:", 12, 10, 24, Color.BLACK);

            int index = 1;
            int verticalSpaceBetweenItems = 40;

            foreach (SolverStrategy solverType in Enum.GetValues(typeof(SolverStrategy)))
            {
                Raylib.DrawText($"{(int)solverType}: {solverType}", 12, index * verticalSpaceBetweenItems, 24, Color.BLACK);
                index++;
            }
        }

        public override void DisplayLevelsToChooseFrom()
        {
            Raylib.DrawText("Choose one of these levels: ", 600, 0, 20, Color.BLACK);

            for (var i = 0; i < _game.AvailableMaps.Count; i++)
            {
                var map = _game.AvailableMaps[i];

                var rowsCount = map.Board.GetAllCells().Length;
                int factor = rowsCount > 3 ? 400 : 200;

                Raylib.DrawText($"{i + 1}. {map.Name}", i * factor + 50, 40, 20, Color.BLACK);
                DisplayBoardInPosition(map.Board, i * factor, 90);

                int key = Raylib.GetKeyPressed() - 49;
                if (key == i)
                {
                    level = _game.AvailableMaps[i].FilePath;
                    _game.SetMap(level);
                }
            }
        }
    }
}
