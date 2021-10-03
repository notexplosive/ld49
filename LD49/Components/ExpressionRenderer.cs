using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class ExpressionRenderer : BaseComponent
    {
        public ExpressionRenderer(Actor actor, TransitiveExpression expression) : base(actor)
        {
            var transitiveType = expression is AddMathExpression ? TransitiveExpression.SubType.Add : TransitiveExpression.SubType.Multiply;

            var layout = new LayoutGroup(this.actor,
                transitiveType == TransitiveExpression.SubType.Add ? Orientation.Horizontal : Orientation.Vertical);

            var content = expression.GetContents();
            var i = 0;

            foreach (var subexpression in content)
            {
                layout.AddBothStretchedElement("item", child =>
                {
                    if (subexpression is Prime prime)
                    {
                        new PrimeRenderer(child, prime);
                    }

                    if (subexpression is TransitiveExpression transitiveExpression)
                    {
                        new ExpressionRenderer(child, transitiveExpression);
                    }
                });

                if (i < content.Length - 1)
                {
                    layout.AddBothStretchedElement("symbol", child => { new OperatorRenderer(child, transitiveType); });
                }

                i++;
            }
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
