using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic.Solvers.GraphSolvers
{
    internal class UniformCostSolver : GraphBasedSolver
    {
        public UniformCostSolver(GameState initialNode) : base("Uniform Cost", initialNode) { }

        public override GameStatus Solve()
        {
            GameState? finalState = null;

            PriorityQueue<GameState, int> queue = new();
            queue.Enqueue(InitialNode, 0);

            bool shouldBreakLoop = false;

            HashSet<string> visited = new()
            {
                InitialNode.ID
            };

            while (queue.Count > 0)
            {
                this.Statistics.NumberOfVisitedNodes++;
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

            Statistics.FinalState = finalState;
            return finalState is null ? GameStatus.ImpossibleToWin : GameStatus.Win;
        }
    }
}
