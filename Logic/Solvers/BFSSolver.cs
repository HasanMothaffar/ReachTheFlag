using ReachTheFlag.Exceptions;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public class BFSSolver : GraphBasedSolver
    {
        public BFSSolver(GameState initialNode) : base("BFS", initialNode) { }

        public override void Solve()
        {
            GameState? finalState = null;

            Queue<GameState> queue = new();
            queue.Enqueue(InitialNode);

            HashSet<string> visited = new()
            {
                InitialNode.ID
            };

            bool shouldQuitLoop = false;

            while (queue.Count > 0)
            {
                NumberOfVisitedNotes++;
                if (shouldQuitLoop) break;
                GameState state = queue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;

                    if (visited.Contains(neighbor.ID))
                    {
                        continue;
                    }

                    visited.Add(neighbor.ID);
                    Parents[neighbor.PlayerCell] = state.PlayerCell;

                    if (neighbor.IsFinal())
                    {
                        finalState = neighbor;
                        shouldQuitLoop = true;
                        break;
                    }

                    queue.Enqueue(neighbor);
                }
            }

            if (finalState is null)
            {
                throw new GameImpossibleToSolveException("Game is impossible to solve.");
            }

            this.FinalState = finalState;
        }
    }
}
