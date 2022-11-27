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

    SolverStrategy solverStrategy;

    try
    {
        Enum.TryParse(type, out SolverStrategy solverType);
        solverStrategy = solverType;
    }

    catch
    {
        Console.WriteLine("Unknown key was input: Falling back to user input strategy.");
        solverStrategy = SolverStrategy.UserInput;
    }

    return solverStrategy;
}

void Main()
{
    ReachTheFlagGame game = new ReachTheFlagGame("D:\\my-projects\\VersionTest\\ConsoleApp1\\Maps\\map.txt");

    while (true)
    {
        SolverStrategy strategy = GetSolveStrategyFromUserInput();
        game.Solve(strategy);

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

Main();