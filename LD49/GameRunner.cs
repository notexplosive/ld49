using System;
using LD49.Components;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD49
{
    public class GameRunner : MachinaGame
    {
        private static int currentLevelIndex;

        public static Scene gameScene;
        // public static BoundedTextRenderer tooltipTextRenderer;

        public GameRunner(string[] args) : base("Expression Compression", args, new Point(1600, 900), new Point(1600, 900),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        public static DragHand DragHand { set; get; }

        public static Level CurrentLevel => Level.All[GameRunner.currentLevelIndex] as Level;

        protected override void OnGameLoad()
        {
            SceneLayers.BackgroundColor = new Color(0.01f, 0.01f, 0.01f);
            var backgroundScene = SceneLayers.AddNewScene();

            var operatorNames = new[]
            {
                MathOperator.Name.Divide,
                MathOperator.Name.Plus,
                MathOperator.Name.Minus,
                MathOperator.Name.Times
            };

            for (var i = 0; i < 50; i++)
            {
                var actor = backgroundScene.AddActor("Orbitter");
                new BoundingRect(actor, new Point(60, 60));
                new OperatorRenderer(actor, operatorNames[MachinaGame.Random.Dirty.Next(operatorNames.Length)],
                    new Color(0.01f, 0.01f, 0.1f),
                    false);
                new Orbitter(actor, backgroundScene.camera.ViewportCenter,
                    MachinaGame.Random.Dirty.NextFloat() * 0.5f + 0.1f,
                    this.startingWindowSize);
            }

            GameRunner.gameScene = SceneLayers.AddNewScene();

            var otherScene = SceneLayers.AddNewScene();
            new AdHoc(otherScene.AddActor("debug")).onKey += (key, state, modifiers) =>
            {
                if (modifiers.ControlShift && key == Keys.Space && state == ButtonState.Pressed)
                {
                    GameRunner.LoadNextLevel();
                }
            };

            GameRunner.LoadLevel(0);
        }

        private static void LoadLevel(int i)
        {
            if (i >= Level.All.Length)
            {
                return;
            }

            var thing = Level.All[i];

            if (thing is Level level)
            {
                GameRunner.BuildLevel(level.allowances, level.startingEquation, level.endingExpression);
            }

            if (thing is Poem poem)
            {
                Poem.BuildPageScene(poem, 0);
            }
        }

        public static void BuildLevel(Allowances allowances, Equation startingEquation,
            MathExpression winningExpression)
        {
            GameRunner.gameScene.DeleteAllActors();

            ExpressionRenderer leftExpressionRenderer = null;
            ExpressionRenderer rightExpressionRenderer = null;
            ExpressionRenderer storageExpressionRenderer = null;

            void DoToBothSides(Func<MathExpression, MathExpression> func)
            {
                leftExpressionRenderer.Expression = func(leftExpressionRenderer.Expression);
                if (!startingEquation.isJustExpression)
                {
                    rightExpressionRenderer.Expression = func(rightExpressionRenderer.Expression);
                }

                if (startingEquation.isJustExpression)
                {
                    if (leftExpressionRenderer.Expression == winningExpression)
                    {
                        new LevelTransition(GameRunner.gameScene.AddActor("Level transition"), true);
                    }
                }
                else
                {
                    if (rightExpressionRenderer.Expression == winningExpression
                        && !GameRunner.IsInvalidState(leftExpressionRenderer.Expression, winningExpression)
                        || leftExpressionRenderer.Expression == winningExpression
                        && !GameRunner.IsInvalidState(rightExpressionRenderer.Expression, winningExpression))
                    {
                        new LevelTransition(GameRunner.gameScene.AddActor("Level transition"), true);
                    }
                }
            }

            var hand = GameRunner.gameScene.AddActor("Hand");
            hand.transform.Depth = 100; // very close to front
            new BoundingRect(hand, new Point(200, 200)).SetOffsetToCenter();

            GameRunner.DragHand = new DragHand(hand);

            // Build Main Expression
            {
                var gameScreen = GameRunner.gameScene.AddActor("GameScreen");
                new BoundingRect(gameScreen, Point.Zero);
                new BoundingRectToViewportSize(gameScreen);
                var gameLayout = new LayoutGroup(gameScreen, Orientation.Vertical);
                gameLayout.SetMarginSize(new Point(100, 50));

                gameLayout.AddElement("reset button", new Point(300, 100), button =>
                {
                    new BoundingRectBorder(button, NumberRenderer.Colors[6]);
                    new BoundedTextRenderer(button, "Reset",
                        MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                        HorizontalAlignment.Center,
                        VerticalAlignment.Center, Overflow.Ignore);
                    new Hoverable(button);
                    new TooltipProvider(button, "Reset");
                    new Clickable(button).OnClick += button => { GameRunner.ReloadLevel(); };
                });

                gameLayout.AddBothStretchedElement("equation", equationActor =>
                {
                    var equationLayout = new LayoutGroup(equationActor, Orientation.Horizontal);
                    equationLayout.AddBothStretchedElement("left expression", expressionActor =>
                    {
                        new Hoverable(expressionActor);
                        leftExpressionRenderer = new ExpressionRenderer(expressionActor, true, startingEquation.left);
                    });

                    equationLayout.AddVerticallyStretchedElement("equals sign", 40,
                        equalsSignActor =>
                        {
                            new OperatorRenderer(equalsSignActor, MathOperator.Name.Equals, Color.White, false);
                        });

                    if (!startingEquation.isJustExpression)
                    {
                        equationLayout.AddBothStretchedElement("right expression", expressionActor =>
                        {
                            new Hoverable(expressionActor);
                            rightExpressionRenderer =
                                new ExpressionRenderer(expressionActor, true, startingEquation.right);
                        });
                    }
                });

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
                gameLayout.AddHorizontallyStretchedElement("controlsRoot", 300, controlsRoot =>
                {
                    var controlsLayout = new LayoutGroup(controlsRoot, Orientation.Horizontal);

                    if (allowances.AllowStorage)
                    {
                        controlsLayout.AddVerticallyStretchedElement("storage", 300, storageActorWrapper =>
                        {
                            new BoundingRectBorder(storageActorWrapper, NumberRenderer.Colors[5]);
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
                                            new BoundingRectBorder(buttonActor, NumberRenderer.Colors[6]);
                                            GameRunner.SetupDropSite(buttonActor, MathOperator.Name.Plus,
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
                                            new BoundingRectBorder(buttonActor, NumberRenderer.Colors[6]);
                                            GameRunner.SetupDropSite(buttonActor, MathOperator.Name.Times,
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
                                    var dragNumber = new DragNumber(storageActor,
                                        allowances.storageStartValue_One ? One.Instance : Zero.Instance);
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
                                                new BoundingRectBorder(buttonActor, Color.Cyan);
                                                new BoundedTextRenderer(buttonActor, "Negate",
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
                                                new BoundingRectBorder(buttonActor, Color.OrangeRed);
                                                new BoundedTextRenderer(buttonActor, "Recipr.",
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

                        new ControlPanel(inventoryActor, allowances);
                    });
                });
            }

            // Build overlay panel
            {
                var overlayPanelActor = GameRunner.gameScene.AddActor("OverlayPanel");
                overlayPanelActor.transform.Depth = 200; // close to front but behind cursor
                var boundingRect = new BoundingRect(overlayPanelActor,
                    new Point(GameRunner.gameScene.camera.UnscaledViewportSize.X,
                        GameRunner.gameScene.camera.UnscaledViewportSize.Y / 4));
                overlayPanelActor.transform.Position -= new Vector2(0, boundingRect.Size.Y);

                new LayoutGroup(overlayPanelActor, Orientation.Vertical)
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
                            overlayButtons.AddBothStretchedElement("Add Button",
                                button =>
                                {
                                    GameRunner.SetupOverlayButton(button, MathOperator.Name.Plus, Color.Orange,
                                        expression =>
                                        {
                                            DoToBothSides(originalExpression =>
                                                MathOperator.Add(originalExpression, expression));
                                        });
                                });
                        }

                        if (allowances.allowSubtractingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Subtract Button", button =>
                            {
                                GameRunner.SetupOverlayButton(button, MathOperator.Name.Minus, Color.Orange,
                                    expression =>
                                    {
                                        DoToBothSides(originalExpression =>
                                            MathOperator.Subtract(originalExpression, expression));
                                    });
                            });
                        }

                        if (allowances.allowMultiplyingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Multiply Button", button =>
                            {
                                GameRunner.SetupOverlayButton(button, MathOperator.Name.Times, Color.Orange,
                                    expression =>
                                    {
                                        DoToBothSides(originalExpression =>
                                            MathOperator.Multiply(originalExpression, expression));
                                    });
                            });
                        }

                        if (allowances.allowDividingTo_Expression)
                        {
                            overlayButtons.AddBothStretchedElement("Divide Button", button =>
                            {
                                GameRunner.SetupOverlayButton(button, MathOperator.Name.Divide, Color.Orange,
                                    expression =>
                                    {
                                        DoToBothSides(originalExpression =>
                                            MathOperator.Divide(originalExpression, expression));
                                    });
                            });
                        }

                        overlayButtons.HorizontallyStretchedSpacer();
                    });

                new OverlayTrigger(overlayPanelActor, GameRunner.DragHand,
                    Vector2.Zero);
            }
        }

        private static bool IsInvalidState(MathExpression expression, MathExpression winningExpression)
        {
            // fuck it, we're using ToString()
            var isInTermsOfItself = expression.ToString().Contains(winningExpression.ToString());
            var isInTermsOfInfinity = expression.ToString().Contains(Infinity.Instance.ToString());

            return isInTermsOfInfinity || isInTermsOfItself;
        }

        private static void SetupOverlayButton(Actor buttonActor, MathOperator.Name operatorName, Color color,
            Action<MathExpression> onDrop)
        {
            new BoundingRectFill(buttonActor, new Color(color, 0.25f));
            new OperatorRenderer(buttonActor, operatorName, Color.White, false);
            new Hoverable(buttonActor);
            new DropSite(buttonActor, GameRunner.DragHand, onDrop);
        }

        private static void SetupDropSite(Actor buttonActor, MathOperator.Name operatorName,
            Action<MathExpression> onDrop)
        {
            new OperatorRenderer(buttonActor, operatorName, Color.White, false);
            new Hoverable(buttonActor);
            new DropSite(buttonActor, GameRunner.DragHand, onDrop);
            new TooltipProvider(buttonActor, "DropSite");
        }

        public static void LoadNextLevel()
        {
            GameRunner.currentLevelIndex++;
            GameRunner.LoadLevel(GameRunner.currentLevelIndex);
        }

        public static void ReloadLevel()
        {
            GameRunner.LoadLevel(GameRunner.currentLevelIndex);
        }
    }
}
