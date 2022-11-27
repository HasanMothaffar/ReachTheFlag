using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public abstract class GraphBasedSolver : GameSolver
    {
        protected int MaximalSolutionTreeDepth = 0;
        protected int SolutionDepth = 0;

        protected GraphBasedSolver(string name, GameState node) : base(name, node)
        {
        }

        private int getMaxDepth(GameState node)
        {
            int maxDepth = 0;

            var neighbors = node.GetAllNeighboringStates();
            if (neighbors.Count == 0) return 0;

            foreach (KeyValuePair<MoveDirection, GameState> kvp in neighbors)
            {
                GameState stateNode = kvp.Value;
                maxDepth = Math.Max(maxDepth, getMaxDepth(stateNode));
            }

            return maxDepth + 1;
        }
        
        protected void CalculateMaxDepth(GameState node)
        {
            this.MaximalSolutionTreeDepth = this.getMaxDepth(node);
        }

        protected void CalculateSolutionDepth(Dictionary<GameState, GameState?> parents, GameState finalState)
        {
            int solutionDepth = 0;
            GameState? p = parents[finalState];

            while (p is not null)
            {
                solutionDepth++;
                p = parents[p];
            }

            this.SolutionDepth = solutionDepth;
        }

        protected override void PrintSpecificStatistics()
        {
            Console.WriteLine($"Solution Depth: {SolutionDepth}");
            Console.WriteLine($"Maximum Tree Depth: {MaximalSolutionTreeDepth}");
        }
    }
}
