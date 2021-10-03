using LD49.Data;
using Machina.Engine;
using Machina.Components;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class BoundingRectBorder : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly Color color;

        public BoundingRectBorder(Actor actor, Color color) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.RectF;
            rect.Inflate(-5, -5);

            LineDrawer.DrawLine(spriteBatch,rect.TopLeft,rect.TopRight, this.color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch,rect.TopLeft,rect.BottomLeft, this.color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch,rect.BottomLeft,rect.BottomRight, this.color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch,rect.TopRight,rect.BottomRight, this.color, transform.Depth, 5f);
        }
    }
}
