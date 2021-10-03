using System;
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

        public static DragHand DragHand { private set; get; }

        protected override void OnGameLoad()
        {
            SceneLayers.BackgroundColor = Color.Black;
            var gameScene = SceneLayers.AddNewScene();

            ExpressionRenderer mainExpressionRenderer = null;
            ExpressionRenderer storageExpressionRenderer = null;
            Hoverable expressionHoverable = null;

            var hand = gameScene.AddActor("Hand");
            hand.transform.Depth = 100; // very close to front
            new BoundingRect(hand, new Point(200, 200)).SetOffsetToCenter();

            Reckoning.DragHand = new DragHand(hand);

            // Build Main Expression
            {
                var gameScreen = gameScene.AddActor("GameScreen");
                new BoundingRect(gameScreen, Point.Zero);
                new BoundingRectToViewportSize(gameScreen);
                new LayoutGroup(gameScreen, Orientation.Vertical)
                    .SetMarginSize(new Point(100, 50))
                    .AddBothStretchedElement("main expression", expressionActor =>
                    {
                        expressionHoverable = new Hoverable(expressionActor);
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
                                                    MathOperator.Multiply(
                                                        MathOperator.Add(NamedVariable.Z, Prime.Thirteen),
                                                        NamedVariable.X))),
                                            Zero.Instance
                                        ))),
                                One.Instance));

                        mainExpressionRenderer.OnExpressionChange += () => { Reckoning.tooltipTextRenderer.Text = ""; };
                    })
                    .AddHorizontallyStretchedElement("tooltip", 60,
                        controlsRoot =>
                        {
                            new BoundingRectBorder(controlsRoot, Color.Orange);
                            Reckoning.tooltipTextRenderer = new BoundedTextRenderer(controlsRoot, "...",
                                MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                                HorizontalAlignment.Center,
                                VerticalAlignment.Center, Overflow.Ignore);
                        })
                    .AddHorizontallyStretchedElement("controlsRoot", 200,
                        controlsRoot =>
                        {
                            new BoundingRectBorder(controlsRoot, Color.Orange);
                            var layout = new LayoutGroup(controlsRoot, Orientation.Horizontal);

                            layout.AddBothStretchedElement("inventory", inventoryActor =>
                            {
                                new LayoutGroup(inventoryActor, Orientation.Horizontal).SetPaddingBetweenElements(5);
                                new ControlPanel(inventoryActor, mainExpressionRenderer);
                            });

                            layout.AddVerticallyStretchedElement("storage", 300,
                                storageActor =>
                                {
                                    storageExpressionRenderer =
                                        new ExpressionRenderer(storageActor, true, Zero.Instance);
                                });
                        })
                    ;
            }

            // Build overlay panel
            {
                var overlayPanelActor = gameScene.AddActor("OverlayPanel");
                overlayPanelActor.transform.Depth = 200; // close to front but behind cursor
                var boundingRect = new BoundingRect(overlayPanelActor,
                    new Point(gameScene.camera.UnscaledViewportSize.X, gameScene.camera.UnscaledViewportSize.Y / 2));
                overlayPanelActor.transform.Position -= new Vector2(0, boundingRect.Size.Y);

                new Hoverable(overlayPanelActor); // prevents hovers on things below it

                void SetupOverlayButton(Actor buttonActor, MathOperator.Name operatorName, Action<MathExpression> onDrop)
                {
                    new BoundingRectFill(buttonActor, new Color(Color.Orange, 0.25f));
                    new OperatorRenderer(buttonActor, operatorName, Color.White, false);
                    new Hoverable(buttonActor);
                    new DropSite(buttonActor, Reckoning.DragHand, onDrop);
                }

                new LayoutGroup(overlayPanelActor, Orientation.Vertical)
                    .VerticallyStretchedSpacer()
                    .AddBothStretchedElement("OverlayContent", overlayContentActor =>
                    {
                        new BoundingRectFill(overlayContentActor, new Color(Color.Orange, 0.25f));
                        new LayoutGroup(overlayContentActor, Orientation.Horizontal)
                            .SetMarginSize(new Point(32, 32))
                            .HorizontallyStretchedSpacer()
                            .AddBothStretchedElement("Add Button",
                                addButton =>
                                {
                                    SetupOverlayButton(addButton, MathOperator.Name.Plus, expression =>
                                    {
                                        mainExpressionRenderer.Expression =
                                            MathOperator.Add(mainExpressionRenderer.Expression, expression);
                                    });
                                })
                            .HorizontallyStretchedSpacer()
                            .AddBothStretchedElement("Multiply Button",
                                multiplyButton =>
                                {
                                    SetupOverlayButton(multiplyButton, MathOperator.Name.Times, expression =>
                                    {
                                        mainExpressionRenderer.Expression =
                                            MathOperator.Multiply(mainExpressionRenderer.Expression, expression);
                                    });
                                })
                            .HorizontallyStretchedSpacer()
                            ;
                    })
                    ;

                new OverlayTrigger(overlayPanelActor, Reckoning.DragHand, expressionHoverable, Vector2.Zero);
            }
        }
    }
}
