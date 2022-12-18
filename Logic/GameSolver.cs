using ReachTheFlag.Exceptions;
using ReachTheFlag.Game;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.UI;

namespace ReachTheFlag.Logic
{
    public abstract class GameSolver
    {
        protected GameState InitialNode { get; private set; }
        public string Name { get; protected set; }

        protected readonly GameStatistics Statistics;
        protected readonly GameUI? UserInterface;

        public GameSolver(string name, GameState node, GameUI ui): this(name, node)
        {
            UserInterface = ui;
        }

        public GameSolver(string name, GameState node)
        {
            Name = name;
            InitialNode = node;
            Statistics = new GameStatistics();
        }

        public abstract GameStatus Solve();
        public GameStatistics SolveAndGetStatistics()
        {
            Statistics.StartTimer();
            Statistics.Status = Solve();
            Statistics.StopTimer();

            FillStatisticsData();

            return Statistics;
        }
        protected virtual void FillStatisticsData() { }
    }
}
