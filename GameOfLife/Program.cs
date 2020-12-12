using System;

namespace GameOfLife
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameOfLife())
                game.Run();
        }
    }
}
