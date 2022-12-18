using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic.Solvers.GraphSolvers
{
    public class DFSSolver : GraphBasedSolver
    {
        public DFSSolver(GameState initialNode) : base("DFS", initialNode) { }

        public override GameStatus Solve()
        {
            GameState? finalState = null;

            Stack<GameState> stack = new();
            stack.Push(InitialNode);

            bool shouldQuitLoop = false;

            HashSet<string> visited = new()
            {
                InitialNode.ID
            };

            while (stack.Count > 0)
            {
                NumberOfVisitedNotes++;
                if (shouldQuitLoop) break;

                GameState state = stack.Pop();

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

                    else
                    {
                        stack.Push(neighbor);
                    }
                }
            }

            if (finalState is null)
            {
                return GameStatus.ImpossibleToWin;
            }

            Statistics.FinalState = finalState;
            return GameStatus.Win;
        }

    }
}
