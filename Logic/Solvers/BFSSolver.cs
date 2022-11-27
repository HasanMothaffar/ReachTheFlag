using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class BFSSolver : GraphBasedSolver
    {
        public BFSSolver(GameState initialNode) : base("BFS", initialNode) { }

        public override void Solve()
        {
            GameState? finalState = null;
            GameState initialState = this.InitialNode;

            Queue<GameState> stateQueue = new();
            stateQueue.Enqueue(initialState);

            Dictionary<GameState, GameState?> parents = new();
            parents[initialState] = null;

            while (stateQueue.Count > 0)
            {
                GameState state = stateQueue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
                {
                    GameState stateNode = kvp.Value;
                    parents[stateNode] = state;

                    if (stateNode.IsFinal())
                    {
                        finalState = stateNode;
                        break;
                    }

                    else
                    {
                        stateQueue.Enqueue(stateNode);
                    }
                }
            }

            if (finalState is null)
            {
                throw new Exception("Game is impossible to solve.");
            }

            Printer.PrintBoard(finalState.Board);
            Console.WriteLine("Game done.");

            this.CalculateMaxDepth(InitialNode);
            this.CalculateSolutionDepth(parents, finalState);
            this.PopulatePlayerPath(parents, finalState);
        }

        protected override void PrintSpecificStatistics()
        {
            Console.WriteLine($"Solution depth: {SolutionDepth}");
            Console.WriteLine($"Maximum tree depth: {MaximalSolutionTreeDepth}");
        }
    }
}
