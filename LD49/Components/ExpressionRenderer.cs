using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class ExpressionRenderer : BaseComponent
    {
        public ExpressionRenderer(Actor actor, MathExpression expression) : base(actor)
        {
            if (expression is Number number)
            {
                new NumberRenderer(actor, number);
            }

            if (expression is TransitiveExpression transitiveExpression)
            {
                new TransitiveExpressionRenderer(actor, transitiveExpression);
            }

            if (expression is NamedVariable variable)
            {
                new NamedVariableRenderer(actor, variable);
            }

            if (expression is InverseExpression inverseExpression)
            {
                new UnaryExpressionRenderer(actor, inverseExpression);
            }

            if (expression is NegateExpression negateExpression)
            {
                new UnaryExpressionRenderer(actor, negateExpression);
            }
        }
    }
}
