using ReachTheFlag.Exceptions;

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

        public override void OnPlayerLeave()
        {
            throw new CellImpossibleToReachException();
        }

        public override bool CanBeVisited()
        {
            return false;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void OnPlayerEnter()
        {
            throw new CellImpossibleToReachException();
        }
    }
}
