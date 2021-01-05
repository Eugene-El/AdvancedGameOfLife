namespace GameOfLifeLib
{
    public record Range
    {
        public int From { get; set; }
        public int To { get; set; }

        public Range(int from, int to) => (From, To) = (from, to);
    }
}
