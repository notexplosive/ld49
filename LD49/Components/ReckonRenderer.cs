using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class ReckonRenderer
    {
        public enum RotationLevel
        {
            Normal,
            Horizontal,
            Vertical,
            Both
        }

        private readonly Actor actor;
        public readonly BoundingRect boundingRect;
        private float totalTime;

        public ReckonRenderer(Actor actor, BoundingRect boundingRect)
        {
            this.boundingRect = boundingRect;
            this.actor = actor;
        }

        public int ShortSide => Math.Min(this.boundingRect.Rect.Height, this.boundingRect.Rect.Width);
        public int Radius => ShortSide / 2;

        public void DrawRing(SpriteBatch spriteBatch, float radius, int sides, Color color, float animationFactor,
            RotationLevel rotationLevel)
        {
            var time = this.totalTime * animationFactor;
            var fullRadians = MathF.PI * 2;
            var sideIncrement = fullRadians / sides;
            var center = this.boundingRect.Rect.Center.ToVector2();

            for (var i = 0; i < sides; i++)
            {
                var horizontalRadius = rotationLevel == RotationLevel.Horizontal || rotationLevel == RotationLevel.Both
                    ? radius * MathF.Sin(time * 3)
                    : radius;
                var verticalRadius = rotationLevel == RotationLevel.Vertical || rotationLevel == RotationLevel.Both
                    ? radius * MathF.Cos(time * 3)
                    : radius;

                var angle = sideIncrement * i + time;

                var edgePoint1 = center
                                 + new Vector2(
                                     MathF.Cos(angle - sideIncrement) * horizontalRadius,
                                     MathF.Sin(angle - sideIncrement) * verticalRadius);
                var edgePoint2 = center
                                 + new Vector2(
                                     MathF.Cos(angle) * horizontalRadius,
                                     MathF.Sin(angle) * verticalRadius);

                LineDrawer.DrawLine(spriteBatch, edgePoint1, edgePoint2, color, this.actor.transform.Depth, 10f);
            }
        }

        public void Update(float dt)
        {
            this.totalTime += dt;
        }

        public Vector2 Normal(int x, int y)
        {
            var vector = new Vector2(x, y);
            vector.Normalize();
            return vector;
        }
    }
}
