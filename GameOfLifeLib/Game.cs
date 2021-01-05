using System;

namespace GameOfLifeLib
{
    public class Game
    {
        protected int[,] previousFieldState;
        protected int[,] currentFieldState;
        protected GameConfiguration.GameFieldUpdate gameFieldUpdateMethod;

        public Vector Size { get; protected set; }
        public int Width => Size.X;
        public int Height => Size.Y;


        public Game(): this(GameConfiguration.DefaulConfiguration) { }
        public Game(GameConfiguration gameConfiguration)
        {
            Size = gameConfiguration.Size;
            gameFieldUpdateMethod = gameConfiguration.GameFieldUpdateMethod;
            previousFieldState = new int[Height, Width];
            currentFieldState = new int[Height, Width];
        }

        public void NextStep()
        {
            var fieldState = currentFieldState;
            currentFieldState = gameFieldUpdateMethod(previousFieldState, currentFieldState);
            previousFieldState = fieldState;
        }

        public int this[int y, int x]
        {
            get
            {
                AssertCoordinates(y, x);
                return currentFieldState[y, x];
            }
            set
            {
                AssertCoordinates(y, x);
                currentFieldState[y, x] = value;
            }
        }

        protected void AssertCoordinates(int y, int x)
        {
            if (y < 0 || y > Height)
                throw new ArgumentOutOfRangeException($"Specified Y ({y}) is out of range of game");
            if (x < 0 || x > Width)
                throw new ArgumentOutOfRangeException($"Specified X ({x}) is out of range of game");
        }
    }
}
