using Raylib_cs;
using ReachTheFlag.Logic;
using ReachTheFlag.ExtensionMethods;
using ReachTheFlag.Game;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.Cells;
using System.Numerics;

namespace ReachTheFlag.UI
{
    public class RaylibUI : GameUI
    {
        private readonly ReachTheFlagGame _game;
        private SolverStrategy? _solverStrategy;
        private GameStatistics? _gameStatistics;
        private string? level;

        private Font _emojiFont;

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
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT);
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
                    Raylib.DrawText("Game is solved!", 12, 64, 24, Color.BLACK);
                    DisplayGameStatistics(_gameStatistics);

                    Raylib.DrawText("Press r to restart the game", 12, 600, 24, Color.BLACK);
                }

                else if (status == GameStatus.PlayerIsStuck)
                {
                    Raylib.DrawText("You lose :(", 12, 60, 24, Color.BLACK);
                    Raylib.DrawText("Press r to restart the game", 12, 600, 24, Color.BLACK);
                }

                

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        public override void DisplayGameStatistics(GameStatistics statistics)
        {
            DisplayBoard(statistics.FinalState.Board);

            int index = 0;

            int textPositionX = 12;
            int[] textPositionsY = { 100, 120, 140, 160, 180, 200 };

            Raylib.DrawText($"Elapsed seconds: {statistics.ElapsedSeconds}", textPositionX, textPositionsY[index++], 20, Color.BLACK);

            if (statistics.NumberOfPlayerMoves > 0) Raylib.DrawText($"Number of moves: {statistics.NumberOfPlayerMoves}", textPositionX, textPositionsY[index++], 20, Color.BLACK);
            if (statistics.SolutionDepth > 0) Raylib.DrawText($"Solution Depth: {statistics.SolutionDepth}", textPositionX, textPositionsY[index++], 20, Color.BLACK);
            if (statistics.NumberOfVisitedNodes > 0) Raylib.DrawText($"Number of Visited Nodes: {statistics.NumberOfVisitedNodes}", textPositionX, textPositionsY[index++], 20, Color.BLACK);
            if (statistics.ShortestPathCost > 0) Raylib.DrawText($"Shortest Path Cost: {statistics.ShortestPathCost}", textPositionX, textPositionsY[index++], 20, Color.BLACK);
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
                Raylib.DrawText($"{(int)solverType}: {solverType.DisplayName()}", 12, index * verticalSpaceBetweenItems, 24, Color.BLACK);
                index++;
            }
        }

        public override void DisplayLevelsToChooseFrom()
        {
            Raylib.DrawText("Choose one of these levels: ", 600, 0, 20, Color.BLACK);
            int[] levelsXPositions = { 20, 500, 1100 };

            for (var i = 0; i < _game.AvailableMaps.Count; i++)
            {
                var map = _game.AvailableMaps[i];

                Raylib.DrawText($"{i + 1}. {map.Name}", levelsXPositions[i] + 50, 40, 20, Color.BLACK);
                DisplayBoardInPosition(map.Board, levelsXPositions[i], 90);
            }

            int key = Raylib.GetKeyPressed() - 49;
            if (key >= 0 && key <= 2)
            {
                level = _game.AvailableMaps[key].FilePath;
                _game.SetMap(level);
            }
        }
    }
}
