using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic.Solvers.GraphSolvers.AStar
{
    public class AStarSolver : GraphBasedSolver
    {
        private readonly int[][] _dist;
        private readonly double[][] _heuristicValues;

        private readonly BoardCell[][] _cells;

        public AStarSolver(GameState initialNode) : base("A* (star) naive", initialNode)
        {
            _cells = initialNode.Board.GetAllCells();
            _dist = getDistancesArray();

            AStarHeuristic heuristic = new AStarManhattanHeuristic();
            _heuristicValues = heuristic.GetHeuristicArrayForBoard(initialNode.Board);
        }

        private int[][] getDistancesArray()
        {
            int rowsCount = _cells.Length;
            int[][] dist = new int[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                int columnsCount = _cells[i].Length;
                dist[i] = new int[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    dist[i][j] = _cells[i][j].IsPlayerVisiting ? 0 : int.MaxValue;
                }
            }

            return dist;
        }
        public override GameStatus Solve()
        {
            GameState? finalState = null;

            PriorityQueue<GameState, double> queue = new();
            queue.Enqueue(InitialNode, 0);

            HashSet<string> visited = new()
            {
                InitialNode.ID
            };

            bool shouldBreakLoop = false;

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

                    int possibleShortestDistance = _dist[currentState.X][currentState.Y] + neighbor.Weight;
                    double priority = neighbor.Weight + _heuristicValues[neighbor.X][neighbor.Y];
                    Parents[neighbor.PlayerCell] = currentState.PlayerCell;

                    if (possibleShortestDistance < _dist[neighbor.X][neighbor.Y])
                    {
                        _dist[neighbor.X][neighbor.Y] = possibleShortestDistance;
                        priority = possibleShortestDistance + _heuristicValues[neighbor.X][neighbor.Y];
                    }

                    if (neighbor.IsFinal())
                    {
                        finalState = neighbor;
                        shouldBreakLoop = true;
                        break;
                    }

                    queue.Enqueue(neighbor, priority);

                }
            }

            Statistics.FinalState = finalState;
            return finalState is null ? GameStatus.ImpossibleToWin : GameStatus.Win;
        }
    }
}
