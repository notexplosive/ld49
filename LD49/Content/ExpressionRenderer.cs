using LD49.Components;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Content
{
    public class ExpressionRenderer : BaseComponent
    {
        public ExpressionRenderer(Actor actor, TransitiveExpression expression) : base(actor)
        {
            var layout = new LayoutGroup(this.actor, expression is AddMathExpression ? Orientation.Horizontal : Orientation.Vertical);

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
                    layout.AddBothStretchedElement("symbol", child => { new BoundingRectFill(child, Color.Orange); });
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
