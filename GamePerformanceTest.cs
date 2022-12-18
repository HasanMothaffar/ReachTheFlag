using ReachTheFlag.ExtensionMethods;
using ReachTheFlag.Game;
using ReachTheFlag.Logic;
using System.Diagnostics;

namespace ReachTheFlag
{
    public class GamePerformanceTest
    {
        private ReachTheFlagGame _game;
        public GamePerformanceTest(ReachTheFlagGame game)
        {
            _game = game;
        }

        public void CalculateAndDisplayAverageRuntimes(int iterationsCount)
        {
            Console.WriteLine($"Average runtime for algorithms ({iterationsCount} iterations):");
            foreach (SolverStrategy strategy in Enum.GetValues(typeof(SolverStrategy)))
            {
                if (strategy == SolverStrategy.UserInput) continue;

                double averageRuntime = 0;

                for (var i = 0; i < iterationsCount; i++)
                {
                    var watch = new Stopwatch();

                    watch.Start();
                    _game.Solve(strategy);
                    watch.Stop();

                    averageRuntime += watch.Elapsed.TotalSeconds;
                }

                averageRuntime /= iterationsCount;
                displayAverageRuntime(strategy.DisplayName(), averageRuntime);
            }
        }

        private void displayAverageRuntime(string strategyName, double avg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{strategyName}:\t");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{avg}s\n");
        }
    }
}
