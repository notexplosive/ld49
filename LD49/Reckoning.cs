using LD49.Components;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49
{
    public class Reckoning : MachinaGame
    {
        public static BoundedTextRenderer tooltipTextRenderer;

        public Reckoning(string[] args) : base("The Reckoning", args, new Point(1600, 900), new Point(1600, 900),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        protected override void OnGameLoad()
        {
            SceneLayers.BackgroundColor = Color.Black;
            var gameScene = SceneLayers.AddNewScene();

            var parent = gameScene.AddActor("LayoutRoot");
            ExpressionRenderer mainExpressionRenderer = null;
            new BoundingRect(parent, Point.Zero);
            new BoundingRectToViewportSize(parent);
            new LayoutGroup(parent, Orientation.Vertical)
                .SetMarginSize(new Point(100, 50))
                .AddBothStretchedElement("main expression", expressionActor =>
                {
                    mainExpressionRenderer = new ExpressionRenderer(expressionActor, true,
                        MathOperator.Multiply(
                            MathOperator.Add(
                                Prime.Seven,
                                MathOperator.Multiply(
                                    MathOperator.Add(
                                        Prime.Seventeen,
                                        Prime.FiftyNine),
                                    MathOperator.Add(
                                        MathOperator.Inverse(
                                            MathOperator.Negate(
                                                MathOperator.Multiply(MathOperator.Add(NamedVariable.Z, Prime.Thirteen),
                                                    NamedVariable.X))),
                                        Zero.Instance
                                    ))),
                            One.Instance));

                    mainExpressionRenderer.OnExpressionChange += () =>
                    {
                        Reckoning.tooltipTextRenderer.Text = "";
                    };
                })
                .AddHorizontallyStretchedElement("tooltip", 60,
                    controlsRoot =>
                    {
                        new BoundingRectBorder(controlsRoot, Color.Orange);
                        Reckoning.tooltipTextRenderer = new BoundedTextRenderer(controlsRoot, "...",
                            MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White, HorizontalAlignment.Center,
                            VerticalAlignment.Center, Overflow.Ignore);
                    })
                .AddHorizontallyStretchedElement("controls", 200,
                    controlsRoot =>
                    {
                        new BoundingRectBorder(controlsRoot, Color.Orange);
                        var layout = new LayoutGroup(controlsRoot, Orientation.Horizontal);
                        new ControlPanel(controlsRoot, mainExpressionRenderer);
                    })
                ;
        }
    }
}
