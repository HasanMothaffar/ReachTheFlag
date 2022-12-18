using ReachTheFlag.Game;
using ReachTheFlag.UI;


AvailableGameUI ChooseGameUI()
{
    AvailableGameUI? chosenUI = null;

    while (chosenUI is null)
    {
        Console.WriteLine("Choose display style: ");
        Console.WriteLine("1. Terminal ");
        Console.WriteLine("2. GUI (Raylib) ");

        char pressedKey = Console.ReadKey(true).KeyChar;
        string type = pressedKey.ToString().ToLower();
        Console.Clear();

        if (Enum.TryParse(type, out AvailableGameUI ui) && Enum.IsDefined(ui))
        {
            chosenUI = ui;
        }
    }

    return (AvailableGameUI)chosenUI;
}

void Main()
{
    // For displaying emojis in windows temrinal correctly
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    ReachTheFlagGame game = new("D:\\my-projects\\VersionTest\\ConsoleApp1\\Maps\\");

    var ui = ChooseGameUI();
    game.SetUserInterface(ui);
    game.UserInterface.Run();
}

Main();

//void TestAlgorithmRuntimes()
//{
//    GamePerformanceTest testSuite = new GamePerformanceTest();
//    testSuite.CalculateAndDisplayAverageRuntimes(2);
//    Console.WriteLine();
//}

//TestAlgorithmRuntimes();
