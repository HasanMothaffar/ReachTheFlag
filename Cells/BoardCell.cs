﻿using ReachTheFlag.Utils;

namespace ReachTheFlag.Cells
{
    public abstract class BoardCell : ICloneable<BoardCell>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Weight;

        public bool IsFlag { get; protected set; }
        public string Symbol { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public bool IsVisited { get; private set; }
        public bool IsPlayerVisiting { get; private set; }

        public BoardCell(int x, int y, int weight = 1)
        {
            X = x;
            Y = y;
            Weight = weight;

            IsPlayerVisiting = false;
            IsVisited = false;
            IsFlag = false;

            Symbol = "default";
            Color = ConsoleColor.White;
        }

        public virtual void OnPlayerEnter()
        {
            IsVisited = true;
            IsPlayerVisiting = true;
        }

        public virtual void OnPlayerLeave()
        {
            IsPlayerVisiting = false;
        }

        public abstract bool CanBeVisited();
        public abstract bool IsValid();

        public virtual BoardCell Clone()
        {
            return (BoardCell)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
