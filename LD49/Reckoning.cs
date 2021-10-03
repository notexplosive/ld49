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

            /*
            {
                var layoutRoot = gameScene.AddActor("LayoutRoot");
                new BoundingRect(layoutRoot, Point.Zero);
                new BoundingRectToViewportSize(layoutRoot);
                var rows = new LayoutGroup(layoutRoot, Orientation.Vertical);

                LayoutGroup CreateRow()
                {
                    LayoutGroup group = null;
                    rows.AddBothStretchedElement("Row",
                        actor => { group = new LayoutGroup(actor, Orientation.Horizontal); });
                    return group;
                }

                var row = CreateRow();
                var column = 0;

                void AddItemToRow(Action<Actor> callback)
                {
                    row.AddBothStretchedElement("Item", callback);

                    column++;

                    if (column > 7)
                    {
                        row = CreateRow();
                        column = 0;
                    }
                }

                AddItemToRow(actor => { });
            }
            */

            var parent = gameScene.AddActor("LayoutRoot");
            new BoundingRect(parent, Point.Zero);
            new BoundingRectToViewportSize(parent);

            new ExpressionRenderer(parent,
                (TransitiveExpression)
                MathOperator.Add(
                    MathOperator.Add(
                        Prime.Seven,
                        MathOperator.Multiply(
                            MathOperator.Add(
                                Prime.Seventeen,
                                Prime.FiftyNine),
                            MathOperator.Add(
                                NamedVariable.Z, 
                                Infinity.Instance
                            ))),
                    One.Instance));
        }
    }
}
