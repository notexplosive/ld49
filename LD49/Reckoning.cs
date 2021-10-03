using LD49.Components;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49
{
    public class Reckoning : MachinaGame
    {
        public Reckoning(string[] args) : base("The Reckoning", args, new Point(1600, 900), new Point(1600, 900),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        protected override void OnGameLoad()
        {
            SceneLayers.BackgroundColor = Color.Black;
            var gameScene = SceneLayers.AddNewScene();

            var numberSize = 200;

            var layoutRoot = gameScene.AddActor("LayoutRoot");
            new BoundingRect(layoutRoot, Point.Zero);
            new BoundingRectToViewportSize(layoutRoot);
            var rows = new LayoutGroup(layoutRoot, Orientation.Vertical);

            LayoutGroup CreateRow()
            {
                LayoutGroup group = null;
                rows.AddHorizontallyStretchedElement("Row", numberSize,
                    actor => { group = new LayoutGroup(actor, Orientation.Horizontal); });
                return group;
            }

            var row = CreateRow();
            MachinaGame.Print(row.actor.GetComponent<BoundingRect>().Width,
                row.actor.GetComponent<BoundingRect>().Height);
            var column = 0;

            foreach (var prime in Prime.All.Values)
            {
                row.AddVerticallyStretchedElement("Item", numberSize,
                    actor => { new NumberRenderer(actor, prime); });

                column++;

                if (column > 7)
                {
                    row = CreateRow();
                    column = 0;
                }
            }
        }
    }
}
