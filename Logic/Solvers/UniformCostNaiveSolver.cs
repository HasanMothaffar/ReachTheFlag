using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using System.Linq;

namespace ReachTheFlag.Logic
{
    internal class UniformCostNaiveSolver : GraphBasedSolver
    {
        public UniformCostNaiveSolver(GameState initialNode) : base("Uniform Cost", initialNode) { }

        public override void Solve()
        {
            GameState? finalState = null;

            PriorityQueue<GameState, int> queue = new();
            queue.Enqueue(this.InitialNode, 0);

            bool shouldBreakLoop = false;

            HashSet<string> visited = new()
            {
                InitialNode.ID
            };

            while (queue.Count > 0)
            {
                NumberOfVisitedNotes++;
                if (shouldBreakLoop) break;

                GameState currentState = queue.Dequeue();
                
                foreach (KeyValuePair<MoveDirection, GameState> kvp in currentState.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;
                    if (visited.Contains(neighbor.ID)) continue;

                    visited.Add(neighbor.ID);
                    Parents[neighbor.PlayerCell] = currentState.PlayerCell;

                    if (neighbor.IsFinal())
                    {
                        finalState = neighbor;
                        shouldBreakLoop = true;
                        break;
                    }


                    queue.Enqueue(neighbor, 1);
                }
            }

            this.FinalState = finalState;
        }
    }
}
