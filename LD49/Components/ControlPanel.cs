using System;
using System.Collections.Generic;
using System.Linq;
using LD49.Data;
using Machina.Components;
using Machina.Engine;

namespace LD49.Components
{
    public class ControlPanel : BaseComponent
    {
        public enum NavigationImage
        {
            Next,
            Previous,
            None
        }

        private readonly List<MathExpression> allNumbers;
        private readonly LayoutGroup layout;
        private readonly Allowances allowances;

        public ControlPanel(Actor actor, Allowances allowances) : base(actor)
        {
            this.allNumbers = new List<MathExpression>();
            this.allNumbers.Add(Zero.Instance);
            this.allNumbers.Add(One.Instance);

            if (allowances.allowXNamedValue)
            {
                this.allNumbers.Add(NamedVariable.X);
            }

            if (allowances.allowAllPrimes)
            {
                this.allNumbers.AddRange(Prime.LittlePrimes);
            }

            this.layout = RequireComponent<LayoutGroup>();

            this.allowances = allowances;
            LoadNumberPage(0);
        }

        private void LoadNumberPage(int pageNumber)
        {
            ClearPage();
            var pageLength = 7;

            if (pageNumber != 0)
            {
                BuildNavigationButton("Previous Page", NavigationImage.Previous, () => LoadNumberPage(pageNumber - 1));
            }

            var isAtEnd = false;

            for (var i = pageNumber * pageLength; i < (pageNumber + 1) * pageLength; i++)
            {
                this.layout.AddBothStretchedElement("primeButton",
                    primeButtonActor =>
                    {
                        if (this.allNumbers.Count > i)
                        {
                            var prime = this.allNumbers.ElementAt(i);

                            SetupNumberButton(primeButtonActor, prime);
                        }
                    });
                
                if (this.allNumbers.Count-1 <= i)
                {
                    isAtEnd = true;
                }
            }

            if (!isAtEnd)
            {
                BuildNavigationButton("Next Page", NavigationImage.Next, () => LoadNumberPage(pageNumber + 1));
            }
        }

        private void SetupNumberButton(Actor numberActor, MathExpression number)
        {
            new DragNumber(numberActor, number);

            if (this.allowances.firstLevelTutorial && number is One)
            {
                new TutorialRenderer(numberActor);
            }
        }

        private void BuildNavigationButton(string tooltip, NavigationImage image, Action callback)
        {
            this.layout.AddBothStretchedElement("navigationButton", navigationButtonActor =>
            {
                new Hoverable(navigationButtonActor);
                new TooltipProvider(navigationButtonActor, tooltip);
                var clickable = new Clickable(navigationButtonActor);

                void ClickAction(MouseButton button)
                {
                    callback();
                }

                new NavigationArrowRenderer(navigationButtonActor, image);

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
