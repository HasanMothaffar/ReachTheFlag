using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public abstract class GraphBasedSolver : GameSolver
    {
        protected int SolutionDepth = 0;
        protected int MaximalSolutionTreeDepth = 0;

        protected Dictionary<GameState, GameState?> Parents = new();

        protected GraphBasedSolver(string name, GameState node) : base(name, node) 
        {
            Parents[this.InitialNode] = null;
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
        
        private void calculateMaxDepth()
        {
            this.MaximalSolutionTreeDepth = this.getMaxDepth(this.FinalState);
        }

        private void calculateSolutionDepth()
        {
            int solutionDepth = 0;
            GameState? p = Parents[FinalState];

            while (p is not null)
            {
                solutionDepth++;
                p = Parents[p];
            }

            this.SolutionDepth = solutionDepth;
        }

        protected override void PrintSpecificStatistics()
        {
            calculateMaxDepth();
            calculateSolutionDepth();

            Console.WriteLine($"Solution Depth: {SolutionDepth}");
            Console.WriteLine($"Maximum Tree Depth: {MaximalSolutionTreeDepth}");
        }
    }
}
