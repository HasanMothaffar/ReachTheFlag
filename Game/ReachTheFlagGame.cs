using ReachTheFlag.Logic;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Game
{
    public class ReachTheFlagGame
    {
        // For restarting the game
        private readonly GameState _originalState;
        private GameState _currentState;

        public ReachTheFlagGame(string mapFilePath)
        {
            GameBoard parsedBoard = MapParser.ParseBoardMap(mapFilePath);

            _currentState = new GameState(parsedBoard);
            _originalState = _currentState.Clone();

            Printer.PrintBoard(this._currentState.Board);
        }

        public GameStatus GetStatus()
        {
            if (_currentState.IsFinal()) return GameStatus.Win;
            if (_currentState.IsPlayerStuck()) return GameStatus.Lose;

            return GameStatus.Playing;
        }

        public void Restart()
        {
            _currentState = _originalState;
        }

        public void SolveAndPrintSolutionStatistics(SolverStrategy strategy)
        {
            GameSolver solver = SolverFactory.GetSolverForGame(strategy, _currentState);
            solver.SolveAndPrintSolutionStatistics();
        }

        public void Solve(SolverStrategy strategy)
        {
            GameSolver solver = SolverFactory.GetSolverForGame(strategy, _currentState);
            solver.Solve();
        }
    }
}
