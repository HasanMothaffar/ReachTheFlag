using System;

namespace ReachTheFlag.Cells
{
    public class CellFactory
    {
        public static BoardCell GetCell(int x, int y, string type)
        {
            switch (type)
            {
                case CellTypes.Flag:
                    {
                        return new FlagCell(x, y);
                    }

                case CellTypes.Gap:
                    {
                        return new GapCell(x, y);
                    }

                case CellTypes.InfiniteStep:
                    {
                        return new InfiniteStepCell(x, y);
                    }

                case CellTypes.Player:
                    {
                        BoardCell cell = new NStepCell(x, y, 1)
                        {
                            IsPlayerVisiting = true
                        };

                        return cell;
                    }


                default:
                    {
                        try
                        {
                            int numberOfAllowedSteps = Int32.Parse(type);
                            return new NStepCell(x, y, numberOfAllowedSteps);
                        }

                        catch
                        {
                            return new NStepCell(x, y, 1);
                        }
                    }
            }
        }
    }

    public static class CellTypes
    {
        public const string Flag = "f";
        public const string Gap = "g";
        public const string InfiniteStep = "m";
        public const string Player = "p";
    }
}
