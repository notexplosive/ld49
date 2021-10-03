using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class OperatorRenderer : BaseComponent
    {
        private readonly Color color;
        private readonly ReckonRenderer reckonRenderer;
        private readonly MathOperator.Name symbol;

        public OperatorRenderer(Actor actor, MathOperator.Name symbol, Color color, bool isHoverable) : base(actor)
        {
            this.reckonRenderer = new ReckonRenderer(actor, RequireComponent<BoundingRect>());
            this.symbol = symbol;
            this.color = color;

            if (isHoverable)
            {
                new Hoverable(this.actor);
            }
        }

        public override void Update(float dt)
        {
            this.reckonRenderer.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var legLength = this.reckonRenderer.ShortSide / 4f;
            var center = this.reckonRenderer.boundingRect.Rect.Center.ToVector2();

            if (this.symbol == MathOperator.Name.Times || this.symbol == MathOperator.Name.Plus)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(legLength, 0), this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(-legLength, 0), this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, -legLength), this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, legLength), this.color,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Times)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, 1) * legLength,
                    this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, -1) * legLength,
                    this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, -1) * legLength,
                    this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, 1) * legLength,
                    this.color,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Divide)
            {
                // Underscore underneath the number
                var longLegLength = this.reckonRenderer.ShortSide / 2;
                LineDrawer.DrawLine(spriteBatch, center + new Vector2(longLegLength, longLegLength),
                    center + new Vector2(-longLegLength, longLegLength), this.color,
                    transform.Depth, 8f);
            }

            if (this.symbol == MathOperator.Name.Minus)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, 0) * legLength,
                    this.color,
                    transform.Depth, 8f);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, 0) * legLength,
                    this.color,
                    transform.Depth, 8f);
            }
        }
    }
}
