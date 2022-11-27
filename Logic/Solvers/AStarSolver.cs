using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    internal class AStarSolver : GraphBasedSolver
    {
        private int[][] _dist;
        private BoardCell[][] _cells;

        public AStarSolver(GameState initialNode) : base("A* (star)", initialNode)
        {
            this._cells = initialNode.Board.GetAllCells();
            this._dist = getDistancesArray();
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

        public override void Solve()
        {
            GameState initialState = this.InitialNode;
            GameState? finalState = null;

            BoardCell[][] cells = initialState.Board.GetAllCells();

            // Dijkstra Data structures
            PriorityQueue<GameState, int> queue = new();

            // For printing the path
            Dictionary<GameState, GameState?> parents = new();
            parents[initialState] = null;

            queue.Enqueue(initialState, 0);

            while (queue.Count > 0)
            {
                GameState currentState = queue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in currentState.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;

                    int possibleShortestDistance = _dist[currentState.X][currentState.Y] + neighbor.Weight;

                    if (possibleShortestDistance < _dist[neighbor.X][neighbor.Y])
                    {
                        queue.Enqueue(neighbor, possibleShortestDistance);
                        _dist[neighbor.X][neighbor.Y] = possibleShortestDistance;

                        parents[neighbor] = currentState;

                        if (neighbor.PlayerCell.IsFlag)
                        {
                            finalState = neighbor;
                        }
                    }
                }
            }

            this.CalculateMaxDepth(this.InitialNode);
            this.CalculateSolutionDepth(parents, finalState);
            this.PopulatePlayerPath(parents, finalState);
        }

        private void printShortestPathCost()
        {
            var flagCell = this.InitialNode.Board.FlagCell;
            Console.WriteLine($"Shortest path cost: {_dist[flagCell.X][flagCell.Y]}");
            Console.WriteLine("All distances from player:\n");

            foreach (var row in _dist)
            {
                foreach (var entry in row)
                {
                    Console.Write($"{entry} ");
                }

                Console.WriteLine();
            }
        }

        protected override void PrintSpecificStatistics()
        {
            base.PrintSpecificStatistics();
            printShortestPathCost();
        }
    }
}
