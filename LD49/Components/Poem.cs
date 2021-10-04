using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class Poem : ILevel
    {
        public static Poem Introduction = new Poem(
            new Page("this is Zero", Zero.Instance),
            new Page("this is One", One.Instance),
            new Page("they are stable", Zero.Instance),
            new Page("they are safe", One.Instance),
            new Page("they are the bedrock on which all other Numbers stand",
                MathOperator.Add(One.Instance, MathOperator.Add(One.Instance, One.Instance))),
            new Page("your task: turn this sequence of Ones into Zero",
                MathOperator.Add(One.Instance, MathOperator.Add(One.Instance, One.Instance))));

        public static Poem IntroducePrimes = new Poem(
            new Page("these are Primes",
                AddMathExpression.CreateMany(Prime.Three, Prime.Seven, Prime.Thirteen, Prime.TwentyNine)),
            new Page("some people think they're important",
                AddMathExpression.CreateMany(Prime.Eleven, Prime.FiftyThree, Prime.FortyOne, Prime.ThirtySeven,
                    Prime.FiftyNine)),
            new Page("i think they're clutter", Zero.Instance),
            new Page("push them all to one side so we can isolate the X", NamedVariable.X)
        );

        public static Poem IntroduceNegative = new Poem(
            new Page("this is a Negative", MathOperator.Negate(Prime.Thirteen)),
            new Page("every Number has a Negative, except Zero", Zero.Instance),
            new Page("a Negative plus its counterpart is Zero", Zero.Instance),
            new Page("you might have heard of a thing called 'subtracting'",
                MathOperator.Negate(
                    MathOperator.Add(Prime.Two, MathOperator.Add(Prime.Eleven, Prime.Three)))),
            new Page("subtracting is just adding a negative", MathOperator.Negate(Prime.Two)),
            new Page("i have devised a machine to help you use them, feel free to experiment",
                MathOperator.Negate(MathOperator.Add(Prime.NinetySeven, Prime.FiftyNine))));

        public static Poem IntroduceReciprocal = new Poem(
            new Page("this is a Reciprocal", MathOperator.Inverse(Prime.Thirteen)),
            new Page("every Number has a Reciprocal, except One", One.Instance),
            new Page("a Reciprocal times its counterpart is One", One.Instance),
            new Page("the reciprocal of Zero is dangerous", Infinity.Instance),
            new Page("you might have heard of a thing called 'dividing'",
                MathOperator.Inverse(
                    MathOperator.Add(Prime.Two, MathOperator.Add(Prime.Eleven, Prime.Three)))),
            new Page("dividing is just multiplying by a Reciprocal", MathOperator.Inverse(Prime.Two)),
            new Page("i have modified the machine to use Reciprocals, feel free to experiment",
                MathOperator.Inverse(MathOperator.Add(Prime.NinetySeven, Prime.FiftyNine))));

        public static Poem IntroduceInfinity = new Poem(
            new Page("this is Infinity", Infinity.Instance),
            new Page("it is unstable", Infinity.Instance),
            new Page("it is dangerous", Infinity.Instance),
            new Page("it is the Antizero", Infinity.Instance),
            new Page("once born, Infinity can only be destroyed by Zero", Zero.Instance),
            new Page("but the Zero will wash away everything, so perhaps it is best to start over", NamedVariable.X)
        );

        public static Poem Credits = new Poem(
            new Page("thank you for playing", Prime.SeventyNine),
            new Page("more games like this at NotExplosive.net", Prime.NinetySeven),
            new Page("please wishlist 'Three in a Rogue' on steam", Prime.FiftyThree)
        );

        private readonly Page[] pages;

        public Poem(params Page[] pages)
        {
            this.pages = pages;
        }

        public static void BuildPageScene(Poem poem, int pageIndex)
        {
            Reckoning.gameScene.DeleteAllActors();

            var page = poem.GetPage(pageIndex);
            var nextPage = poem.GetPage(pageIndex + 1);
            var prevPage = poem.GetPage(pageIndex - 1);

            var gameScreen = Reckoning.gameScene.AddActor("GameScreen");
            new BoundingRect(gameScreen, Point.Zero);
            new BoundingRectToViewportSize(gameScreen);
            var gameLayout = new LayoutGroup(gameScreen, Orientation.Vertical);
            gameLayout.SetMarginSize(new Point(100, 50));

            gameLayout.AddBothStretchedElement("picture",
                pictureActor => { new ExpressionRenderer(pictureActor, false, page.expression); });

            gameLayout.AddHorizontallyStretchedElement("text", 200, textActor =>
            {
                new BoundedTextRenderer(textActor, page.verse,
                    MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center, Overflow.Ignore);
            });

            gameLayout.AddHorizontallyStretchedElement("button ribbon", 100, buttonRibbonActor =>
            {
                var ribbon = new LayoutGroup(buttonRibbonActor, Orientation.Horizontal);

                ribbon.AddBothStretchedElement("prev", prevButton =>
                {
                    if (prevPage != null)
                    {
                        new NavigationArrowRenderer(prevButton, ControlPanel.NavigationImage.Previous);
                        Poem.SetupButton(prevButton, () => { Poem.BuildPageScene(poem, pageIndex - 1); });
                    }
                });

                ribbon.HorizontallyStretchedSpacer();

                ribbon.AddBothStretchedElement("next", nextButton =>
                {
                    if (nextPage != null)
                    {
                        new NavigationArrowRenderer(nextButton, ControlPanel.NavigationImage.Next);
                        Poem.SetupButton(nextButton, () => { Poem.BuildPageScene(poem, pageIndex + 1); });
                    }
                    else
                    {
                        new ExpressionRenderer(nextButton, false, Zero.Instance);
                        Poem.SetupButton(nextButton, () => { Reckoning.LoadNextLevel(); });
                    }
                });
            });
        }

        private static void SetupButton(Actor button, Action action)
        {
            new BoundingRectBorder(button, NumberRenderer.Colors[5]);
            new Hoverable(button);
            new TooltipProvider(button, "button");

            new Clickable(button).OnClick += button => action();
        }

        private Page GetPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= this.pages.Length)
            {
                return null;
            }

            return this.pages[pageIndex];
        }

        public class Page
        {
            public readonly MathExpression expression;
            public readonly string verse;

            public Page(string verse, MathExpression expression)
            {
                this.verse = verse;
                this.expression = expression;
            }
        }
    }
}
