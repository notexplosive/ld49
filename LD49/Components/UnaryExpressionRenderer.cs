﻿using LD49.Data;
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

            var layout = new LayoutGroup(this.actor,
                isNegate ? Orientation.Horizontal : Orientation.Vertical);

            var negateColor = Color.Cyan;

            if (isHoverable)
            {
                new Hoverable(this.actor);
                new TooltipProvider(this.actor, expression.ToString());
            }

            if (isNegate)
            {
                layout.AddVerticallyStretchedElement("first", 32,
                    firstActor => { new OperatorRenderer(firstActor, MathOperator.Name.Minus, negateColor, false); });
            }
            else
            {
                layout.AddBothStretchedElement("first",
                    firstActor =>
                    {
                        new NumberRenderer(firstActor, One.Instance, false);
                        new OperatorRenderer(firstActor, MathOperator.Name.FractionBar, Color.Gray, false);
                    });
            }

            layout.AddBothStretchedElement("second",
                secondActor =>
                {
                    if (isNegate)
                    {
                        new BoundingRectBorder(secondActor, negateColor);
                    }

                    new ExpressionRenderer(secondActor, isHoverable, expression.GetInnerValue(), isNegate ? 2 : 1);
                });
        }
    }
}
