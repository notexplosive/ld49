using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class TransitiveExpressionRenderer : BaseComponent
    {
        public TransitiveExpressionRenderer(Actor actor, TransitiveExpression expression, int expressionDepth) :
            base(actor)
        {
            var transitiveType = expression is AddMathExpression
                ? TransitiveExpression.SubType.Add
                : TransitiveExpression.SubType.Multiply;

            var layout = new LayoutGroup(this.actor,
                expressionDepth % 2 == 0 ? Orientation.Horizontal : Orientation.Vertical);

            var content = expression.GetContents();
            var i = 0;

            foreach (var subexpression in content)
            {
                layout.AddBothStretchedElement("item", child => { new ExpressionRenderer(child, subexpression, expressionDepth + 1); });

                if (i < content.Length - 1)
                {
                    layout.AddBothStretchedElement("symbol",
                        child =>
                        {
                            new OperatorRenderer(child,
                                transitiveType == TransitiveExpression.SubType.Add
                                    ? MathOperator.Name.Plus
                                    : MathOperator.Name.Times,
                                Color.Gray);
                        });
                }

                i++;
            }
        }
    }
}
