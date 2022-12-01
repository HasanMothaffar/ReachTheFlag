using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public abstract class GraphBasedSolver : GameSolver
    {
        protected int SolutionDepth = 0;
        protected int MaximalSolutionTreeDepth = 0;

        protected int NumberOfVisitedNotes = 0;

        protected Dictionary<BoardCell, BoardCell?> Parents = new();

        private bool _hasReachedMaxDepth = false;

        protected GraphBasedSolver(string name, GameState node) : base(name, node) 
        {
            Parents[this.InitialNode.PlayerCell] = null;
        }

        private void initializeMaxDepth(GameState node, int maxDepth)
        {
            if (_hasReachedMaxDepth) return;

            if (node.IsFinal())
            {
                _hasReachedMaxDepth = true;
                MaximalSolutionTreeDepth = maxDepth;
                return;
            }

            var neighbors = node.GetAllNeighboringStates();

            foreach (KeyValuePair<MoveDirection, GameState> kvp in neighbors)
            {
                GameState stateNode = kvp.Value;
                initializeMaxDepth(stateNode, maxDepth + 1);
            }
        }
        
        protected void CalculateMaxDepth()
        {
            initializeMaxDepth(InitialNode, 0);
        }

        protected void CalculateSolutionDepth()
        {
            int solutionDepth = 0;
            BoardCell? c = Parents[FinalState.PlayerCell];

            while (c is not null)
            {
                solutionDepth++;
                c = Parents[c];
            }

            SolutionDepth = solutionDepth;
        }

        protected override void PrintSpecificStatistics()
        {
            CalculateSolutionDepth();
            Console.WriteLine($"Solution Depth: {SolutionDepth}");

            CalculateMaxDepth();
            Console.WriteLine($"Maximum Tree Depth: {MaximalSolutionTreeDepth}");
            Console.WriteLine($"Number of Visited Nodes: {NumberOfVisitedNotes}");
        }

        protected override void PopulatePlayerPath()
        {
            Stack<BoardCell> stack = new();
            BoardCell? c = Parents[FinalState.PlayerCell];

            while (c is not null)
            {
                stack.Push(c);
                c = Parents[c];
            }

            while (stack.Count > 0)
            {
                this.PlayerPath.AddCell(stack.Pop());
            }
        }
    }
}
