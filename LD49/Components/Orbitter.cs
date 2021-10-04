using System;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class Orbitter : BaseComponent
    {
        private readonly Vector2 center;
        private readonly float radius;
        private readonly Point radiusScale;
        private float time;
        private readonly float orbitSpeed;
        private readonly float timeDirection;

        public Orbitter(Actor actor, Vector2 center, float radius, Point radiusScale) : base(actor)
        {
            this.center = center;
            this.radius = radius;
            this.radiusScale = radiusScale;

            this.time = MathF.PI * 2 * MachinaGame.Random.Dirty.NextFloat();
            this.orbitSpeed = MachinaGame.Random.Dirty.NextFloat() / 4f;

            this.timeDirection = MachinaGame.Random.Dirty.NextBool() ? -1 : 1;
        }

        public override void Update(float dt)
        {
            this.time += (dt / 60f + this.orbitSpeed / 60f) * this.timeDirection;

            this.actor.transform.Position =
                this.center +
                new Vector2(MathF.Sin(this.time) * this.radius * this.radiusScale.X,
                    MathF.Cos(this.time) * this.radius * this.radiusScale.X);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
