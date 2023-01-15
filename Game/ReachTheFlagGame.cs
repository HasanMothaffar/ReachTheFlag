using ReachTheFlag.Logic;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.UI;
using System.Xml.Linq;

namespace ReachTheFlag.Game
{
    public class ReachTheFlagGame
    {
        // For restarting the game
        private GameState _originalState;
        public GameState CurrentState { get; private set; }
        public GameUI UserInterface { get; private set; }

        public readonly List<GameMap> AvailableMaps;
        public string CurrentMap { get; private set; }

        public ReachTheFlagGame()
        {
            AvailableMaps = new List<GameMap>();

            DirectoryInfo di = new DirectoryInfo("Maps");
            FileInfo[] files = di.GetFiles("*.json");

            for (var i = 0; i < files.Length; i++)
            {
                var filename = Path.GetFileNameWithoutExtension(files[i].Name);
                GameBoard board = MapParser.ParseBoardMap(files[i].FullName);
                AvailableMaps.Add(new GameMap(i + 1, filename, board, files[i].FullName));
            }
        }

        public void SetUserInterface(AvailableGameUI ui)
        {
            if (ui == AvailableGameUI.Terminal) UserInterface = new TerminalUI(this);
            else if (ui == AvailableGameUI.Raylib) UserInterface = new RaylibUI(this);
        }

        public void SetMap(string map)
        {
            GameBoard parsedBoard = MapParser.ParseBoardMap(map);

            CurrentState = new GameState(parsedBoard);
            _originalState = CurrentState.Clone();
        }

        public void Restart()
        {
            CurrentState = _originalState.Clone();
        }

        public GameStatistics Solve(SolverStrategy strategy, GameUI? ui)
        {
            GameSolver solver = SolverFactory.GetSolverForGame(strategy, CurrentState, ui);
            GameStatistics statistics = solver.SolveAndGetStatistics();
            CurrentState = statistics.FinalState;

            return statistics;
        }
        public GameStatistics Solve(SolverStrategy strategy)
        {
            GameSolver solver = SolverFactory.GetSolverForGame(strategy, CurrentState, null);
            GameStatistics statistics = solver.SolveAndGetStatistics();
            CurrentState = statistics.FinalState;

            return statistics;
        }
    }
}
