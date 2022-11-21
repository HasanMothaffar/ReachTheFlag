using ReachTheFlag.Cells;
using ReachTheFlag.Game;

namespace ReachTheFlag.Logic
{
    internal class UniformCostSolver : IGameSolver
    {
        public string Name => "Uniform Cost";
        private ReachTheFlagGame _game;

        public UniformCostSolver(ReachTheFlagGame game)
        {
            _game = game;
        }

        private int[][] getDistancesArrayForCells(BoardCell[][] cells)
        {
            int rowsCount = cells.Length;
            int[][] dist = new int[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                int columnsCount = cells[i].Length;
                dist[i] = new int[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    dist[i][j] = int.MaxValue;
                }
            }

            return dist;
        }

        public void Solve()
        {
            BoardCell[][] cells = _game.CurrentState.Board.GetAllCells();
            BoardCell startingCell = _game.CurrentState.Board.GetPlayerCell();

            Dictionary<BoardCell, BoardCell> parents = new();
            parents.Add(startingCell, null);

            int[][] dist = getDistancesArrayForCells(cells);
            dist[startingCell.X][startingCell.Y] = 0;

            PriorityQueue<BoardCell, int> queue = new();
            queue.Enqueue(startingCell, startingCell.Weight);

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();

                foreach (var neighbor in cell.Neighbors)
                {
                    int possibleShortestDistance = dist[cell.X][cell.Y] + neighbor.Weight;

                    if (possibleShortestDistance < dist[neighbor.X][neighbor.Y])
                    {
                        queue.Enqueue(neighbor, possibleShortestDistance);
                        dist[neighbor.X][neighbor.Y] = possibleShortestDistance;
                        parents.Add(neighbor, cell);
                    }
                }
            }

            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    Console.Write(dist[i][j] + " ");
                }

                Console.WriteLine();
            }

            BoardCell flagCell = _game.CurrentState.Board.GetFlagCell();

            var parent = parents[flagCell];

            // Print parents of flag cell in reverse order
            while (parent is not null)
            {
                Console.WriteLine(parent);
                parent = parents[parent];
            }
        }
    }
}
