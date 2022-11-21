using ReachTheFlag.Game;

namespace ReachTheFlag.Logic
{
    public class SolverFactory
    {
        public static IGameSolver GetSolverForGame(ReachTheFlagGame game, string type)
        {
            return type switch
            {
                SolverTypes.UserInput => new UserInputSolver(game),
                SolverTypes.DFS => new DFSSolver(game),
                SolverTypes.BFS => new BFSSolver(game),
                SolverTypes.UniformCost => new UniformCostSolver(game),

                _ => new UserInputSolver(game)
            };
        }
    }

    public static class SolverTypes
    {
        public const string UserInput = "1";
        public const string DFS = "2";
        public const string BFS = "3";
        public const string UniformCost = "4";
    }
}
