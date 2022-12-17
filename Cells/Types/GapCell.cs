using ReachTheFlag.Exceptions;
using ReachTheFlag.Game;

namespace ReachTheFlag.Cells
{
    class GapCell : BoardCell
    {
        private static GapCell _instance;

        // (-1, -1) for X and Y because you shouldn't access them: This is a Gap cell.
        private GapCell() : base(-1, -1, 0)
        {
            Symbol = CellPrintSymbols.Gap;
            Color = ConsoleColor.Blue;
        }

        // Gap cells don't have state, therefore one instance suffices.
        public static GapCell GetInstance()
        {
            if (_instance is null)
            {
                _instance = new GapCell();
            }

            return _instance;
        }

        public override void OnPlayerLeave(MoveDirection direction)
        {
            throw new CellImpossibleToReachException();
        }

        public override bool CanBeVisited() => false;
        public override bool IsValid() => true;

        public override void OnPlayerEnter()
        {
            throw new CellImpossibleToReachException();
        }

        public override BoardCell Clone()
        {
            if (_instance is null)
            {
                _instance = new GapCell();
            }

            return _instance;
        }
    }
}
