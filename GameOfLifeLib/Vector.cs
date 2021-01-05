namespace GameOfLifeLib
{
    public record Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y) => (X, Y) = (x, y);
    }
}
