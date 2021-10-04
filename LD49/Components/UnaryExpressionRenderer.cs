using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class UnaryExpressionRenderer : BaseComponent
    {
        private readonly UnaryExpression expression;

        public UnaryExpressionRenderer(Actor actor, UnaryExpression expression, bool isHoverable) : base(actor)
        {
            this.expression = expression;

            var isNegate = expression is NegateExpression;

            var layout = new LayoutGroup(this.actor,Orientation.Horizontal);

            layout.SetMarginSize(new Point(8, 8));
            var color = isNegate ? Color.Cyan : Color.OrangeRed;

            if (isHoverable)
            {
                new Hoverable(this.actor);
                new TooltipProvider(this.actor, expression.ToString());
            }

            layout.AddBothStretchedElement("first",
                firstActor =>
                {
                    new BoundingRectBorder(firstActor, color);
                    new ExpressionRenderer(firstActor, false, expression.GetInnerValue());
                });
        }
    }
}
