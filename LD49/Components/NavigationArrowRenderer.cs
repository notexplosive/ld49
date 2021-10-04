using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class NavigationArrowRenderer : BaseComponent
    {
        private readonly ControlPanel.NavigationImage image;
        private readonly ReckonRenderer reckon;

        public NavigationArrowRenderer(Actor actor, ControlPanel.NavigationImage image) : base(actor)
        {
            this.reckon = new ReckonRenderer(actor, RequireComponent<BoundingRect>());
            this.image = image;
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.reckon.boundingRect.Rect;
            var center = rect.Center;

            var rightTip = new Vector2(center.X + rect.Width / 4f, center.Y);
            var leftTip = new Vector2(center.X - rect.Width / 4f, center.Y);

            if (this.image != ControlPanel.NavigationImage.None)
            {
                LineDrawer.DrawLine(spriteBatch, leftTip, rightTip, Color.Gray, transform.Depth, 5f);

                if (this.image == ControlPanel.NavigationImage.Next)
                {
                    LineDrawer.DrawLine(spriteBatch, rightTip, rightTip + new Vector2(-20, 20), Color.Gray,
                        transform.Depth, 5f);
                    LineDrawer.DrawLine(spriteBatch, rightTip, rightTip + new Vector2(-20, -20), Color.Gray,
                        transform.Depth, 5f);
                }
                else if (this.image == ControlPanel.NavigationImage.Previous)
                {
                    LineDrawer.DrawLine(spriteBatch, leftTip, leftTip + new Vector2(20, 20), Color.Gray,
                        transform.Depth, 5f);
                    LineDrawer.DrawLine(spriteBatch, leftTip, leftTip + new Vector2(20, -20), Color.Gray,
                        transform.Depth, 5f);
                }
            }
        }
    }
}
