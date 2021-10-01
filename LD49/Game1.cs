using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49
{
    public class Game1 : MachinaGame
    {
        public Game1(string[] args) : base("LD49", args, new Point(1600, 900), new Point(1600, 900),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        protected override void OnGameLoad()
        {
            // Hello world!
        }
    }
}
