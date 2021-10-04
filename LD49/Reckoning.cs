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
        private static int currentLevel;

        private static Scene gameScene;
        // public static BoundedTextRenderer tooltipTextRenderer;

        public Reckoning(string[] args) : base("The Reckoning", args, new Point(1600, 900), new Point(1600, 900),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        public static DragHand DragHand { private set; get; }

        protected override void OnGameLoad()
        {
            SceneLayers.BackgroundColor = Color.Black;
            Reckoning.gameScene = SceneLayers.AddNewScene();

            var level = Level.All[0];
            Reckoning.BuildLevel(level.allowances, level.startingExpression,level.endingExpression);
        }

        public static void BuildLevel(Allowances allowances, MathExpression startingExpression,
            MathExpression winningExpression)
        {
            Reckoning.gameScene.DeleteAllActors();

            ExpressionRenderer mainExpressionRenderer = null;
            ExpressionRenderer storageExpressionRenderer = null;
            Hoverable expressionHoverable = null;

            var hand = Reckoning.gameScene.AddActor("Hand");
            hand.transform.Depth = 100; // very close to front
            new BoundingRect(hand, new Point(200, 200)).SetOffsetToCenter();

            Reckoning.DragHand = new DragHand(hand);

            // Build Main Expression
            {
                var gameScreen = Reckoning.gameScene.AddActor("GameScreen");
                new BoundingRect(gameScreen, Point.Zero);
                new BoundingRectToViewportSize(gameScreen);
                new LayoutGroup(gameScreen, Orientation.Vertical)
                    .SetMarginSize(new Point(100, 50))
                    .AddBothStretchedElement("main expression", expressionActor =>
                    {
                        expressionHoverable = new Hoverable(expressionActor);
                        mainExpressionRenderer = new ExpressionRenderer(expressionActor, true, startingExpression);

                        mainExpressionRenderer.OnExpressionChange += newExpression =>
                        {
                            if (newExpression == winningExpression)
                            {
                                new LevelTransition(gameScreen, true);
                            }
                        };
                    })
                    /*
                    .AddHorizontallyStretchedElement("tooltip", 60,
                        controlsRoot =>
                        {
                            new BoundingRectBorder(controlsRoot, Color.Orange);
                            Reckoning.tooltipTextRenderer = new BoundedTextRenderer(controlsRoot, "...",
                                MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                                HorizontalAlignment.Center,
                                VerticalAlignment.Center, Overflow.Ignore);
                        })
                        */
                    .AddHorizontallyStretchedElement("controlsRoot", 300, controlsRoot =>
                    {
                        var controlsLayout = new LayoutGroup(controlsRoot, Orientation.Horizontal);

                        if (allowances.AllowStorage)
                        {
                            controlsLayout.AddVerticallyStretchedElement("storage", 300, storageActorWrapper =>
                            {
                                new BoundingRectBorder(storageActorWrapper, NumberRenderer.Colors[1]);
                                new LayoutGroup(storageActorWrapper, Orientation.Vertical)
                                    .SetMarginSize(new Point(10, 10))
                                    .AddHorizontallyStretchedElement("button ribbon", 60, buttonRibbonActor =>
                                    {
                                        var storageTopRibbonLayout =
                                            new LayoutGroup(buttonRibbonActor, Orientation.Horizontal);

                                        if (allowances.allowAddingTo_Storage)
                                        {
                                            storageTopRibbonLayout.AddBothStretchedElement("add", buttonActor =>
                                            {
                                                new BoundingRectBorder(buttonActor, NumberRenderer.Colors[2]);
                                                Reckoning.SetupDropSite(buttonActor, MathOperator.Name.Plus,
                                                    expression =>
                                                    {
                                                        storageExpressionRenderer.Expression =
                                                            MathOperator.Add(storageExpressionRenderer.Expression,
                                                                expression);
                                                    });
                                            });
                                        }

                                        if (allowances.allowMultiplyingTo_Storage)
                                        {
                                            storageTopRibbonLayout.AddBothStretchedElement("multiply", buttonActor =>
                                            {
                                                new BoundingRectBorder(buttonActor, NumberRenderer.Colors[2]);
                                                Reckoning.SetupDropSite(buttonActor, MathOperator.Name.Times,
                                                    expression =>
                                                    {
                                                        storageExpressionRenderer.Expression =
                                                            MathOperator.Multiply(
                                                                storageExpressionRenderer.Expression,
                                                                expression);
                                                    });
                                            });
                                        }
                                    })
                                    .AddBothStretchedElement("storage inner", storageActor =>
                                    {
                                        var dragNumber = new DragNumber(storageActor, Zero.Instance);
                                        storageExpressionRenderer = dragNumber.expressionRenderer;
                                    })
                                    .AddHorizontallyStretchedElement("lower button ribbon", 60,
                                        lowerButtonRibbonActor =>
                                        {
                                            var lowerRibbon = new LayoutGroup(lowerButtonRibbonActor,
                                                Orientation.Horizontal);

                                            if (allowances.allowNegating_Storage)
                                            {
                                                lowerRibbon.AddBothStretchedElement("negate", buttonActor =>
                                                {
                                                    new BoundingRectBorder(buttonActor, NumberRenderer.Colors[3]);
                                                    new BoundedTextRenderer(buttonActor, "Recipr.",
                                                        MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                                                        HorizontalAlignment.Center, VerticalAlignment.Center,
                                                        Overflow.Ignore);
                                                    new UIButton(buttonActor, () =>
                                                    {
                                                        storageExpressionRenderer.Expression =
                                                            MathOperator.Negate(
                                                                storageExpressionRenderer.Expression);
                                                    });
                                                });
                                            }

                                            if (allowances.allowInverting_Storage)
                                            {
                                                lowerRibbon.AddBothStretchedElement("inverse", buttonActor =>
                                                {
                                                    new BoundingRectBorder(buttonActor, NumberRenderer.Colors[3]);
                                                    new BoundedTextRenderer(buttonActor, "Inverse",
                                                        MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                                                        HorizontalAlignment.Center, VerticalAlignment.Center,
                                                        Overflow.Ignore);
                                                    new UIButton(buttonActor, () =>
                                                    {
                                                        storageExpressionRenderer.Expression =
                                                            MathOperator.Inverse(
                                                                storageExpressionRenderer.Expression);
                                                    });
                                                });
                                            }
                                        });
                            });
                        }

                        controlsLayout.PixelSpacer(32);

                        controlsLayout.AddBothStretchedElement("numbers", inventoryActor =>
                        {
                            new BoundingRectBorder(inventoryActor, NumberRenderer.Colors[0]);
                            new LayoutGroup(inventoryActor, Orientation.Horizontal)
                                .SetMarginSize(new Point(50, 50))
                                .SetPaddingBetweenElements(5);

                            new BoundedTextRenderer(inventoryActor, "Codex (Drag from here)",
                                MachinaGame.Assets.GetSpriteFont("DefaultFont"), NumberRenderer.Colors[4],
                                HorizontalAlignment.Center);

                            new ControlPanel(inventoryActor, mainExpressionRenderer, allowances);
                        });
                    });
            }

            // Build overlay panel
            {
                var overlayPanelActor = Reckoning.gameScene.AddActor("OverlayPanel");
                overlayPanelActor.transform.Depth = 200; // close to front but behind cursor
                var boundingRect = new BoundingRect(overlayPanelActor,
                    new Point(Reckoning.gameScene.camera.UnscaledViewportSize.X,
                        Reckoning.gameScene.camera.UnscaledViewportSize.Y / 2));
                overlayPanelActor.transform.Position -= new Vector2(0, boundingRect.Size.Y);

                new LayoutGroup(overlayPanelActor, Orientation.Vertical)
                    .VerticallyStretchedSpacer()
                    .AddBothStretchedElement("OverlayContent", overlayContentActor =>
                    {
                        new Hoverable(overlayPanelActor); // prevents hovers on things below it
                        new BoundingRectFill(overlayContentActor, new Color(Color.Orange, 0.25f));
                        var overlayButtons = new LayoutGroup(overlayContentActor, Orientation.Horizontal);
                        overlayButtons.SetMarginSize(new Point(32, 32));
                        overlayButtons.SetPaddingBetweenElements(100);
                        overlayButtons.HorizontallyStretchedSpacer();

                        if (allowances.allowAddingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Add Button", button =>
                            {
                                Reckoning.SetupOverlayButton(button, MathOperator.Name.Plus, Color.Orange, expression =>
                                {
                                    mainExpressionRenderer.Expression =
                                        MathOperator.Add(mainExpressionRenderer.Expression, expression);
                                });
                            });
                        }

                        if (allowances.allowSubtractingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Subtract Button", button =>
                            {
                                Reckoning.SetupOverlayButton(button, MathOperator.Name.Minus, Color.Orange,
                                    expression =>
                                    {
                                        mainExpressionRenderer.Expression =
                                            MathOperator.Subtract(mainExpressionRenderer.Expression, expression);
                                    });
                            });
                        }

                        if (allowances.allowMultiplyingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Multiply Button", button =>
                            {
                                Reckoning.SetupOverlayButton(button, MathOperator.Name.Times, Color.Orange,
                                    expression =>
                                    {
                                        mainExpressionRenderer.Expression =
                                            MathOperator.Multiply(mainExpressionRenderer.Expression, expression);
                                    });
                            });
                        }

                        if (allowances.allowDividingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Divide Button", button =>
                            {
                                Reckoning.SetupOverlayButton(button, MathOperator.Name.Divide, Color.Orange,
                                    expression =>
                                    {
                                        mainExpressionRenderer.Expression =
                                            MathOperator.Divide(mainExpressionRenderer.Expression, expression);
                                    });
                            });
                        }

                        overlayButtons.HorizontallyStretchedSpacer();
                    });

                new OverlayTrigger(overlayPanelActor, Reckoning.DragHand, expressionHoverable,
                    Vector2.Zero);
            }
        }

        private static void SetupOverlayButton(Actor buttonActor, MathOperator.Name operatorName, Color color,
            Action<MathExpression> onDrop)
        {
            new BoundingRectFill(buttonActor, new Color(color, 0.25f));
            new OperatorRenderer(buttonActor, operatorName, Color.White, false);
            new Hoverable(buttonActor);
            new DropSite(buttonActor, Reckoning.DragHand, onDrop);
        }

        private static void SetupDropSite(Actor buttonActor, MathOperator.Name operatorName,
            Action<MathExpression> onDrop)
        {
            new OperatorRenderer(buttonActor, operatorName, Color.White, false);
            new Hoverable(buttonActor);
            new DropSite(buttonActor, Reckoning.DragHand, onDrop);
            new TooltipProvider(buttonActor, "DropSite");
        }

        public static void LoadNextLevel()
        {
            Reckoning.currentLevel++;

            var level = Level.All[Reckoning.currentLevel];
            Reckoning.BuildLevel(level.allowances, level.startingExpression, level.endingExpression);
        }
    }
}
