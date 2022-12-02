namespace ReachTheFlag.Cells
{
    public class CellFactory
    {
        public static BoardCell GetCell(int x, int y, string type, int allowedNumberOfSteps = 0, int weight = 1)
        {
            switch (type)
            {
                case CellTypes.Flag:
                    return new FlagCell(x, y, weight);
                case CellTypes.Gap:
                    return GapCell.GetInstance();

                case CellTypes.Player:
                    {
                        BoardCell cell = new NStepCell(x, y, allowedNumberOfSteps, weight);
                        cell.OnPlayerEnter();
                        return cell;
                    }
                case CellTypes.NStep:
                    return new NStepCell(x, y, allowedNumberOfSteps, weight);
                default:
                    return new NStepCell(x, y, weight);
            }
        }
    }

    public static class CellTypes
    {
        public const string Flag = "f";
        public const string Gap = "g";
        public const string Player = "p";
        public const string NStep = "n";
    }
}
