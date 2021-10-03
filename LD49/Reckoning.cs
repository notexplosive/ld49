using System;
using LD49.Content;
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
            MachinaGame.Print(row.actor.GetComponent<BoundingRect>().Width,
                row.actor.GetComponent<BoundingRect>().Height);
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

            AddItemToRow(actor =>
            {
                new ExpressionRenderer(actor,
                    (TransitiveExpression)
                    MathOperator.Add(
                        MathOperator.Add(
                            Prime.Seven,
                            MathOperator.Multiply(
                                MathOperator.Add(
                                    Prime.Seventeen, 
                                    Prime.FiftyNine), 
                                MathOperator.Add(
                                    Prime.Three,
                                    Prime.Thirteen
                                    ))),
                        Prime.Thirteen));
            });
        }
    }
}
