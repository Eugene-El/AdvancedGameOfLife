using System;
using System.Collections.Generic;

namespace GameOfLifeLib
{
    public record FieldUpdateConfiguration
    {
        public static FieldUpdateConfiguration DefaulConfiguration
        {
            get
            {
                return new FieldUpdateConfiguration(
                    new int[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } },
                    new List<Range> { new Range(0, 1), new Range(4, 8) },
                    new List<Range> { new Range(3, 3) }
                );
            }
        }

        public int[,] NeighborsCalculationMatrix { get; protected set; }
        public List<Range> DieCellRanges { get; protected set; }
        public List<Range> BornCellRanges { get; protected set; }

        public FieldUpdateConfiguration(int[,] neighborsCalculationMatrix, List<Range> dieCellRange, List<Range> bornCellRange)
        {
            if (neighborsCalculationMatrix.GetLength(0) % 2 == 0 || neighborsCalculationMatrix.GetLength(1) == 0)
                throw new Exception("Unacceptable matrix was used for game world");

            NeighborsCalculationMatrix = neighborsCalculationMatrix;
            DieCellRanges = dieCellRange;
            BornCellRanges = bornCellRange;
        }

    }
}
