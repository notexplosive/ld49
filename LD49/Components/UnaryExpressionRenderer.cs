using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class UnaryExpressionRenderer : BaseComponent
    {
        private readonly UnaryExpression expression;

        public UnaryExpressionRenderer(Actor actor, UnaryExpression expression) : base(actor)
        {
            this.expression = expression;

            var isNegate = expression is NegateExpression;
            
            var layout = new LayoutGroup(this.actor,
                isNegate ? Orientation.Horizontal : Orientation.Vertical);
            
            layout.AddBothStretchedElement("first", firstActor =>
            {
                if (isNegate)
                {
                    new OperatorRenderer(firstActor,MathOperator.Name.Minus);
                }
                else
                {
                    new NumberRenderer(firstActor, One.Instance);
                    new OperatorRenderer(firstActor,MathOperator.Name.Divide);
                }
            });
            
            layout.AddBothStretchedElement("second", secondActor =>
            {
                new ExpressionRenderer(secondActor, expression.GetInnerValue());
            });
        }
    }
}
