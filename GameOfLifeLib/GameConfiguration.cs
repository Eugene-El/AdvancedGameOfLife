using System.Linq;

namespace GameOfLifeLib
{
    public record GameConfiguration
    {
        protected const int deadCell = 0;
        protected const int aliveCell = 1;

        public static GameConfiguration DefaulConfiguration {
            get
            {
                return new GameConfiguration(new Vector(64, 64));
            }
        }

        public delegate int[,] GameFieldUpdate(int[,] previousFieldState, int[,] currentFieldState);

        protected static GameFieldUpdate GenerateFieldUpdateMethod(FieldUpdateConfiguration fieldUpdateConfiguration)
        {
            return (int[,] previousFieldState, int[,] currentFieldState) => {
                int height = currentFieldState.GetLength(0);
                int width = currentFieldState.GetLength(1);

                int calculationHeight = fieldUpdateConfiguration.NeighborsCalculationMatrix.GetLength(0);
                int calculationWidth = fieldUpdateConfiguration.NeighborsCalculationMatrix.GetLength(1);
                int calculationOffsetY = calculationHeight / -2;
                int calculationOffsetX = calculationWidth / -2;

                int[,] newFieldState = new int[height, width];

                int cellRate, cellX, cellY;
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        cellRate = 0;
                        for (int calculationY = 0; calculationY < calculationHeight; calculationY++)
                            for (int calculationX = 0; calculationX < calculationWidth; calculationX++)
                            {
                                cellY = (y + calculationOffsetY + calculationY + height) % height;
                                cellX = (x + calculationOffsetX + calculationX + width) % width;
                                if (currentFieldState[cellY, cellX] == aliveCell)
                                    cellRate += fieldUpdateConfiguration.NeighborsCalculationMatrix[calculationY, calculationX];
                            }
                        if (currentFieldState[y, x] == aliveCell)
                        {
                            newFieldState[y, x] = fieldUpdateConfiguration.DieCellRanges.Any(r =>
                                cellRate >= r.From && cellRate <= r.To) ? deadCell : aliveCell;
                        }
                        else
                        {
                            newFieldState[y, x] = fieldUpdateConfiguration.BornCellRanges.Any(r =>
                                cellRate >= r.From && cellRate <= r.To) ? aliveCell : deadCell;
                        }
                    }

                return newFieldState;
            };
        }


        public Vector Size { get; set; }
        public GameFieldUpdate GameFieldUpdateMethod { get; set; }


        public GameConfiguration(Vector size) :
            this(size, FieldUpdateConfiguration.DefaulConfiguration) { }

        public GameConfiguration(Vector size, FieldUpdateConfiguration fieldUpdateConfiguration) :
            this(size, GenerateFieldUpdateMethod(fieldUpdateConfiguration)) { }

        public GameConfiguration(Vector size, GameFieldUpdate gameFieldUpdateMethod)
        {
            Size = size;
            GameFieldUpdateMethod = gameFieldUpdateMethod;
        }

    }
}
