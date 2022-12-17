using ReachTheFlag.Structure;
using System.Diagnostics;

namespace ReachTheFlag.Logic.Statistics {
    public class SolvedGameStatistics
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public int? SolutionDepth { get; set; }
        public int? NumberOfPlayerMoves { get; set; }
        public int? NumberOfVisitedNodes { get; set; }
        public int? ShortestPathCost { get; set; }

        public int[][]? ShortestPathsArray { get; set; }

        public readonly PlayerPath PlayerPath;

        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds;

        public SolvedGameStatistics() 
        { 
            PlayerPath = new PlayerPath();
        }

        public void StartTimer()
        {
            _stopwatch.Start();
        }

        public void StopTimer()
        {
            _stopwatch.Stop();
        }
    }
}
