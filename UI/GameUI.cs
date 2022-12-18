using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;

namespace ReachTheFlag.UI
{
    public abstract class GameUI
    {
        public abstract void Run();
        public abstract void DisplayAvailableStrategies();
        public abstract void DisplayBoard(GameBoard board);
        public abstract void DisplayBoardInPosition(GameBoard board, int x, int y);
        public abstract void DisplayGameStatistics(GameStatistics statistics);

        public abstract void DisplayLevelsToChooseFrom();

    }
}
