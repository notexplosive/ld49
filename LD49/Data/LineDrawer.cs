using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace LD49.Data
{
    public class LineDrawer
    {
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, Depth depth,
            float thickness)
        {
            spriteBatch.DrawLine(start, end, color, thickness, depth);
            spriteBatch.DrawCircle(new CircleF(start, thickness / 2f), (int) thickness, color, thickness / 2f, depth);
            spriteBatch.DrawCircle(new CircleF(end, thickness / 2f), (int) thickness, color, thickness / 2f, depth);
        }
    }
}
