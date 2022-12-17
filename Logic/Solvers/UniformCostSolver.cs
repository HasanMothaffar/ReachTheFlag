using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    internal class UniformCostSolver : GraphBasedSolver
    {
        private int[][] _dist;
        private BoardCell[][] _cells;

        public UniformCostSolver(GameState initialNode) : base("Uniform Cost", initialNode) 
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
            GameState? finalState = null;

            PriorityQueue<GameState, int> queue = new();
            queue.Enqueue(this.InitialNode, 0);

            while (queue.Count > 0)
            {
                NumberOfVisitedNotes++;
                GameState currentState = queue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in currentState.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;

                    int possibleShortestDistance = _dist[currentState.X][currentState.Y] + neighbor.Weight;

                    if (possibleShortestDistance < _dist[neighbor.X][neighbor.Y])
                    {
                        queue.Enqueue(neighbor, possibleShortestDistance);
                        _dist[neighbor.X][neighbor.Y] = possibleShortestDistance;

                        Parents[neighbor.PlayerCell] = currentState.PlayerCell;

                        if (neighbor.PlayerCell.IsFlag)
                        {
                            finalState = neighbor;
                        }
                    }
                }
            }

            this.FinalState = finalState;
        }

        protected override void FillStatisticsData()
        {
            var flagCell = this.InitialNode.Board.FlagCell;
            this.Statistics.ShortestPathCost = _dist[flagCell.X][flagCell.Y];
            this.Statistics.ShortestPathsArray = _dist;
        }
    }
}
