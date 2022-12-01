using ReachTheFlag;
using ReachTheFlag.ExtensionMethods;
using ReachTheFlag.Game;
using ReachTheFlag.Logic;

SolverStrategy GetSolveStrategyFromUserInput()
{    
    Console.WriteLine("Choose solving strategy:");
    foreach (SolverStrategy solverType in Enum.GetValues(typeof(SolverStrategy)))
    {
        Console.WriteLine($"{(int)solverType}: {solverType}");
    }

    Console.WriteLine("-----------");

    char pressedKey = Console.ReadKey(true).KeyChar;
    string type = pressedKey.ToString();

    Console.Clear();


    if (Enum.TryParse(type, out SolverStrategy solverStrategy))
    {
        Console.WriteLine($"Chosen strategy: {solverStrategy.DisplayName()}");
        return solverStrategy;
    }

    else
    {
        Console.WriteLine("Unknown key was input: Falling back to user input strategy.");
        return SolverStrategy.UserInput;
    }
}

void Main()
{
    ReachTheFlagGame game = new ReachTheFlagGame("D:\\my-projects\\VersionTest\\ConsoleApp1\\Maps\\map.txt");

    while (true)
    {
        SolverStrategy strategy = GetSolveStrategyFromUserInput();
        game.SolveAndPrintSolutionStatistics(strategy);

        Console.WriteLine("Press r to restart the game, or any other key to quit.");

        char pressedKey = Console.ReadKey(true).KeyChar;
        string pressedKeyInLowercase = pressedKey.ToString().ToLower();

        if (pressedKeyInLowercase == "r")
        {
            Console.Clear();
            game.Restart();
        }

        else
        {
            break;
        }
    }
}

void TestGamePerformance()
{
    GamePerformanceTest testSuite = new GamePerformanceTest();
    testSuite.CalculateAndDisplayAverageRuntimes(2);
    Console.WriteLine();
    //t.CalculateAndDisplayAverageRuntimes(10);
}

Main();
//TestAlgorithmRuntimes();