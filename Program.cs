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
    string mapFilePath = "D:\\my-projects\\VersionTest\\ConsoleApp1\\map.txt";
    ReachTheFlagGame game = new ReachTheFlagGame(mapFilePath);
    SolverStrategy strategy = GetSolveStrategyFromUserInput();

    game.Solve(strategy);
}

Main();