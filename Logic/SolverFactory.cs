using ReachTheFlag.Logic.Solvers.GraphSolvers;
using ReachTheFlag.Logic.Solvers.GraphSolvers.AStar;
using ReachTheFlag.Logic.Solvers.KeyboardSolvers;
using ReachTheFlag.Structure;
using ReachTheFlag.UI;

namespace ReachTheFlag.Logic
{
    public class SolverFactory
    {
        public static GameSolver GetSolverForGame(SolverStrategy strategy, GameState initialNode, GameUI ui)
        {
            return strategy switch
            {
                SolverStrategy.UserInput => 
                        ui is RaylibUI ?
                        new RaylibUserInputSolver(initialNode, ui) :
                        new TerminalUserInputSolver(initialNode, ui),

                SolverStrategy.DFS => new DFSSolver(initialNode),
                SolverStrategy.BFS => new BFSSolver(initialNode),
                SolverStrategy.UniformCost => new UniformCostSolver(initialNode),
                SolverStrategy.AStar => new AStarSolver(initialNode),
                SolverStrategy.AStarNaive => new AStarNaiveSolver(initialNode),
                SolverStrategy.UniformCostNaive => new UniformCostNaiveSolver(initialNode),

                _ => new TerminalUserInputSolver(initialNode, ui)
            };
        }
    }
}
