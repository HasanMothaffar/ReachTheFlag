namespace ReachTheFlag.Logic
{
    public interface GameSolver
    {
        public void Solve();
        public System.Diagnostics.Stopwatch SolveAndGetElapsedTime()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            this.Solve();

            watch.Stop();

            return watch;
        }
    }
}
