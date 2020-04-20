﻿using System;

namespace NPacMan.Game
{
    public class PacMan
    {
        internal PacMan(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }

        public PacMan WithNewX(int newX) => new PacMan(newX, Y, Direction);
        public PacMan WithNewY(int newY) => new PacMan(X, newY, Direction);
        public PacMan WithNewDirection(Direction newDirection) => new PacMan(X, Y, newDirection);

        internal PacMan Move()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return new PacMan(X, Y - 1, Direction);

                case Direction.Down:
                    return new PacMan(X, Y + 1, Direction);

                case Direction.Left:
                    return new PacMan(X - 1, Y, Direction);

                case Direction.Right:
                    return new PacMan(X + 1, Y, Direction);

                default:
                    throw new NotImplementedException();
            }
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}