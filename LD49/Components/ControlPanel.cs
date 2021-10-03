using System;
using System.Linq;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class ControlPanel : BaseComponent
    {
        private readonly LayoutGroup layout;
        private readonly ExpressionRenderer mainExpressionRenderer;

        public ControlPanel(Actor actor, ExpressionRenderer mainExpressionRenderer) : base(actor)
        {
            this.layout = RequireComponent<LayoutGroup>();
            this.mainExpressionRenderer = mainExpressionRenderer;
            LoadPrimesPage(0);
        }

        private void LoadPrimesPage(int pageNumber)
        {
            ClearPage();
            var primes = Prime.All.Values;
            var pageLength = 7;

            if (pageNumber != 0)
            {
                BuildNavigationButton(() => LoadPrimesPage(pageNumber - 1));
            }
            else
            {
                this.layout.HorizontallyStretchedSpacer();
            }

            var isAtEnd = false;

            for (var i = pageNumber * pageLength; i < (pageNumber + 1) * pageLength; i++)
            {
                this.layout.AddBothStretchedElement("primeButton",
                    primeButtonActor =>
                    {
                        if (primes.Count > i)
                        {
                            var prime = primes.ElementAt(i);

                            SetupPrimeButton(primeButtonActor, prime,
                                () => { LoadOperatorPage(prime); });
                        }
                        else
                        {
                            isAtEnd = true;
                        }
                    });
            }

            if (!isAtEnd)
            {
                BuildNavigationButton(() => LoadPrimesPage(pageNumber + 1));
            }
            else
            {
                this.layout.HorizontallyStretchedSpacer();
            }
        }

        private void LoadOperatorPage(MathExpression prime)
        {
            ClearPage();
            this.layout.HorizontallyStretchedSpacer();

            this.layout.AddBothStretchedElement("Back", buttonActor =>
            {
                new ExpressionRenderer(buttonActor, false, prime);
                SetupExecuteButton(buttonActor, () => LoadPrimesPage(0));
            });
            this.layout.AddBothStretchedElement("Add",
                buttonActor =>
                {
                    new OperatorRenderer(buttonActor, MathOperator.Name.Plus, Color.White, false);
                    SetupExecuteButton(buttonActor,
                        () =>
                        {
                            this.mainExpressionRenderer.Expression =
                                MathOperator.Add(this.mainExpressionRenderer.Expression, prime);
                        });
                });
            this.layout.AddBothStretchedElement("Subtract",
                buttonActor =>
                {
                    new OperatorRenderer(buttonActor, MathOperator.Name.Minus, Color.White, false);
                    SetupExecuteButton(buttonActor,
                        () =>
                        {
                            this.mainExpressionRenderer.Expression =
                                MathOperator.Subtract(this.mainExpressionRenderer.Expression, prime);
                        });
                });
            this.layout.AddBothStretchedElement("Multiply",
                buttonActor =>
                {
                    new OperatorRenderer(buttonActor, MathOperator.Name.Times, Color.White, false);
                    SetupExecuteButton(buttonActor,
                        () =>
                        {
                            this.mainExpressionRenderer.Expression =
                                MathOperator.Multiply(this.mainExpressionRenderer.Expression, prime);
                        });
                });
            this.layout.AddBothStretchedElement("Divide",
                buttonActor =>
                {
                    new LayoutGroup(buttonActor, Orientation.Vertical)
                        .AddBothStretchedElement("numerator", numeratorActor =>
                        {
                            new NumberRenderer(numeratorActor, Zero.Instance, false);
                            new OperatorRenderer(numeratorActor, MathOperator.Name.Divide, Color.White, false);
                        })
                        .AddBothStretchedElement("denominator",
                            denominatorActor => { new NumberRenderer(denominatorActor, Zero.Instance, false); });
                    SetupExecuteButton(buttonActor,
                        () =>
                        {
                            this.mainExpressionRenderer.Expression =
                                MathOperator.Divide(this.mainExpressionRenderer.Expression, prime);
                        });
                });
            this.layout.AddBothStretchedElement("Combine",
                buttonActor =>
                {
                    new NumberRenderer(buttonActor, Infinity.Instance, false);

                    SetupExecuteButton(buttonActor,
                        () => { });
                });

            this.layout.HorizontallyStretchedSpacer();
        }

        private void SetupExecuteButton(Actor buttonActor, Action callback)
        {
            new Hoverable(buttonActor);
            var clickable = new Clickable(buttonActor);

            void ClickAction(MouseButton button)
            {
                callback();
                LoadPrimesPage(0);
            }

            clickable.OnClick += ClickAction;
            new CallbackOnDestroy(buttonActor, () => clickable.OnClick -= ClickAction);
        }

        private void SetupPrimeButton(Actor primeButtonActor, MathExpression prime, Action callback)
        {
            new Hoverable(primeButtonActor);
            new ExpressionRenderer(primeButtonActor, false, prime);
            var clickable = new Clickable(primeButtonActor);

            void ClickAction(MouseButton button)
            {
                callback();
            }

            clickable.OnClick += ClickAction;
            new CallbackOnDestroy(primeButtonActor, () => clickable.OnClick -= ClickAction);
        }

        private void BuildNavigationButton(Action callback)
        {
            this.layout.AddBothStretchedElement("navigationButton", navigationButtonActor =>
            {
                new Hoverable(navigationButtonActor);
                var clickable = new Clickable(navigationButtonActor);

                void ClickAction(MouseButton button)
                {
                    callback();
                }

                clickable.OnClick += ClickAction;
                new CallbackOnDestroy(navigationButtonActor, () => clickable.OnClick -= ClickAction);
            });
        }

        private void ClearPage()
        {
            foreach (var element in this.layout.GetAllElements())
            {
                element.actor.Destroy();
            }
        }
    }
}
