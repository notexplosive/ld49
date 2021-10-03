using LD49.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class NamedVariableRenderer : ReckonRenderer
    {
        private readonly NamedVariable variable;

        public NamedVariableRenderer(Actor actor, NamedVariable variable) : base(actor)
        {
            this.variable = variable;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var center = this.boundingRect.Rect.Center.ToVector2();
            var legLength = ShortSide / 2;
            var color = Color.Green;
            var thickness = 10f;

            if (this.variable == NamedVariable.X)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, 1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);
            }
            else if (this.variable == NamedVariable.Y)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(0, 1) * legLength, color,
                    transform.Depth, thickness);
            }
            else if (this.variable == NamedVariable.Z)
            {
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center + Normal(1, 1) * legLength, center + Normal(-1, 1) * legLength, color,
                    transform.Depth, thickness);
                
                LineDrawer.DrawLine(spriteBatch, center, center + Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
                LineDrawer.DrawLine(spriteBatch, center + Normal(-1, -1) * legLength, center + Normal(1, -1) * legLength, color,
                    transform.Depth, thickness);
            }
        }
    }
}
