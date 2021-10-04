using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class NamedVariableRenderer : BaseComponent
    {
        private readonly NamedVariable variable;
        private readonly ReckonRenderer reckonRenderer;

        public NamedVariableRenderer(Actor actor, NamedVariable variable, bool isHoverable) : base(actor)
        {
            this.reckonRenderer = new ReckonRenderer(actor, RequireComponent<BoundingRect>());
            this.variable = variable;

            if (isHoverable)
            {
                new Hoverable(this.actor);
                new TooltipProvider(this.actor, variable.ToString());
            }
        }

        public override void Update(float dt)
        {
            this.reckonRenderer.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var center = this.reckonRenderer.boundingRect.Rect.Center.ToVector2();
            var legLength = this.reckonRenderer.ShortSide / 2;
            var color = Color.Green;
            var thickness = 10f;

            if (this.variable == NamedVariable.X)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, 1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);
            }
            else if (this.variable == NamedVariable.Y)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(0, 1) * legLength, color,
                    transform.Depth, thickness);
            }
            else if (this.variable == NamedVariable.Z)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center + this.reckonRenderer.Normal(1, 1) * legLength, center + this.reckonRenderer.Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);

                LineDrawer.DrawLine(spriteBatch, center, center + this.reckonRenderer.Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center + this.reckonRenderer.Normal(-1, -1) * legLength, center + this.reckonRenderer.Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
            }
        }
    }
}
