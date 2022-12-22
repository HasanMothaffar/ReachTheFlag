using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using System.Diagnostics;

namespace ReachTheFlag.Logic.Statistics {
    public class GameStatistics
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds;

        public int SolutionDepth = 0;
        public int NumberOfPlayerMoves = 0;
        public int NumberOfVisitedNodes = 0;
        public int ShortestPathCost = 0;

        public int[][]? ShortestPathsArray;
        public readonly PlayerPath PlayerPath;

        public GameState FinalState;
        public GameStatus Status = GameStatus.Pending;

        public GameStatistics() 
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
