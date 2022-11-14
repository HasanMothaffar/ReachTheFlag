using ReachTheFlag.Game;
using ReachTheFlag.Logic;

ReachTheFlagGame game = new ReachTheFlagGame();
GameSolver solver;

GameSolver GetSolveStrategyFromUserInput()
{
    Console.WriteLine("Choose solving strategy:");
    Console.WriteLine("1: User input");
    Console.WriteLine("2: DFS");
    Console.WriteLine("3: BFS");
    Console.WriteLine("-----------");

    char pressedKey = Console.ReadKey(true).KeyChar;
    string type = pressedKey.ToString();

    solver = SolverFactory.GetSolverForGame(game, type);

    return solver;
}

void DisplayTimeStatistics(System.Diagnostics.Stopwatch watch)
{
    var elapsedTimeInSeconds = watch.Elapsed.TotalSeconds;
    Console.WriteLine("Elapsed time: {0}", elapsedTimeInSeconds);
}

void Main()
{
    // Make sure to show the board to users first
    game.PrintBoard();
    solver = GetSolveStrategyFromUserInput();

    var watch = solver.SolveAndGetElapsedTime();
    DisplayTimeStatistics(watch);
}

Main();