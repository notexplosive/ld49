using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class OperatorRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly TransitiveExpression.SubType subType;

        public OperatorRenderer(Actor actor, TransitiveExpression.SubType subType) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.subType = subType;
        }

        private int ShortSide => Math.Min(this.boundingRect.Rect.Height, this.boundingRect.Rect.Width);

        public override void Draw(SpriteBatch spriteBatch)
        {
            var legLength = ShortSide / 4f;
            var center = this.boundingRect.Rect.Center.ToVector2();

            LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(legLength, 0), Color.Gray,
                transform.Depth, 8f);
            LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(-legLength, 0), Color.Gray,
                transform.Depth, 8f);
            LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, -legLength), Color.Gray,
                transform.Depth, 8f);
            LineDrawer.DrawLine(spriteBatch, center, center + new Vector2(0, legLength), Color.Gray,
                transform.Depth, 8f);

            if (this.subType == TransitiveExpression.SubType.Multiply)
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
        }

        private Vector2 Normal(int x, int y)
        {
            var vector = new Vector2(x, y);
            vector.Normalize();
            return vector;
        }
    }
}
