using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class TransitiveExpressionRenderer : BaseComponent
    {
        public TransitiveExpressionRenderer(Actor actor, bool isHoverable, TransitiveExpression expression,
            ExpressionRenderer parentExpressionRenderer, int expressionDepth) :
            base(actor)
        {
            var transitiveType = expression is AddMathExpression
                ? TransitiveExpression.SubType.Add
                : TransitiveExpression.SubType.Multiply;

            var layout = new LayoutGroup(this.actor,
                expressionDepth % 2 == 0 ? Orientation.Horizontal : Orientation.Vertical);

            if (isHoverable)
            {
                new Hoverable(this.actor);
                new TooltipProvider(this.actor, expression.ToString());
            }

            var content = expression.GetContents();
            var i = 0;

            foreach (var subexpression in content)
            {
                layout.AddBothStretchedElement("item",
                    child => { new ExpressionRenderer(child, isHoverable, subexpression, expressionDepth + 1); });

                if (i < content.Length - 1 && transitiveType == TransitiveExpression.SubType.Add)
                {
                    layout.AddBothStretchedElement("symbol",
                        child =>
                        {
                            var operatorLayout = new LayoutGroup(child, Orientation.Vertical);

                            // Distribute button
                            if (expressionDepth == 0 && expression is MultiplyMathExpression multiplyExpression &&
                                multiplyExpression.CanDistribute())
                            {
                                operatorLayout.VerticallyStretchedSpacer();
                                if (GameRunner.CurrentLevel.allowances.allowDistribute)
                                {
                                    operatorLayout.AddHorizontallyStretchedElement("DistributeButton", 50,
                                        buttonActor =>
                                        {
                                            new BoundingRectBorder(buttonActor, Color.Orange);
                                            new BoundedTextRenderer(buttonActor, "Distribute",
                                                MachinaGame.Assets.GetSpriteFont("DefaultFont"), Color.White,
                                                HorizontalAlignment.Center, VerticalAlignment.Center, Overflow.Ignore);
                                            new Hoverable(buttonActor);
                                            var clickable = new Clickable(buttonActor);

                                            void OnClick(MouseButton button)
                                            {
                                                if (button == MouseButton.Left)
                                                {
                                                    var addExpression =
                                                        expression.GetFirstExpression<AddMathExpression>();
                                                    var everythingElse =
                                                        multiplyExpression.GetExpressionExcept(addExpression);
                                                    parentExpressionRenderer.Expression =
                                                        AddMathExpression.Distribute(everythingElse, addExpression);
                                                }
                                            }

                                            clickable.OnClick += OnClick;

                                            new CallbackOnDestroy(buttonActor, () => { clickable.OnClick -= OnClick; });
                                        });
                                }
                            }

                            new OperatorRenderer(child,
                                transitiveType == TransitiveExpression.SubType.Add
                                    ? MathOperator.Name.Plus
                                    : MathOperator.Name.Times,
                                Color.Gray, false);
                        });
                }

                i++;
            }
        }
    }
}
