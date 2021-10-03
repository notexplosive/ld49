using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class TransitiveExpressionRenderer : BaseComponent
    {
        public TransitiveExpressionRenderer(Actor actor, TransitiveExpression expression) : base(actor)
        {
            var transitiveType = expression is AddMathExpression
                ? TransitiveExpression.SubType.Add
                : TransitiveExpression.SubType.Multiply;

            var layout = new LayoutGroup(this.actor,
                transitiveType == TransitiveExpression.SubType.Add ? Orientation.Horizontal : Orientation.Vertical);

            var content = expression.GetContents();
            var i = 0;

            foreach (var subexpression in content)
            {
                layout.AddBothStretchedElement("item", child => { new ExpressionRenderer(child, subexpression); });

                if (i < content.Length - 1)
                {
                    layout.AddBothStretchedElement("symbol",
                        child =>
                        {
                            new OperatorRenderer(child,
                                transitiveType == TransitiveExpression.SubType.Add
                                    ? MathOperator.Name.Plus
                                    : MathOperator.Name.Times);
                        });
                }

                i++;
            }
        }
    }
}
