using LD49.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class OperatorRenderer : ReckonRenderer
    {
        private readonly MathOperator.Name symbol;

        public OperatorRenderer(Actor actor, MathOperator.Name symbol) : base(actor)
        {
            this.symbol = symbol;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var legLength = ShortSide / 4f;
            var center = this.boundingRect.Rect.Center.ToVector2();

            if (this.symbol == MathOperator.Name.Times || this.symbol == MathOperator.Name.Plus)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(legLength, 0), Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(-legLength, 0), Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, -legLength), Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, legLength), Color.Gray,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Times)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, 1) * legLength, Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, -1) * legLength, Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, -1) * legLength, Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, 1) * legLength, Color.Gray,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Divide)
            {
                // Underscore underneath the number
                var longLegLength = ShortSide / 2;
                LineDrawer.DrawLine(spriteBatch, center + Normal(1, 1) * longLegLength,
                    center + Normal(-1, 1) * longLegLength, Color.Gray,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Minus)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, 0) * legLength, Color.Gray,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, 0) * legLength, Color.Gray,
                    transform.Depth, 8f);
            }
        }
    }
}
