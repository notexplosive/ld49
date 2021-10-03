using System;

namespace LD49
{
    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (var game = new Reckoning(args))
            {
                game.Run();
            }
        }
    }
}
