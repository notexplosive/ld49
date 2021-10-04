using LD49.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class LevelTransition : BaseComponent
    {
        private readonly TweenAccessors<Vector2> cameraPos;
        private readonly TweenChain tween;

        public LevelTransition(Actor actor, bool isEndOfLevel) : base(actor)
        {
            this.tween = new TweenChain();

            this.cameraPos = new TweenAccessors<Vector2>(this.actor.scene.camera.UnscaledPosition);

            var offset = actor.scene.camera.UnscaledViewportSize.Y + 50;

            if (isEndOfLevel)
            {
                this.tween.AppendWaitTween(1);
            }

            this.tween.AppendVectorTween(
                new Vector2(0, actor.scene.camera.UnscaledPosition.Y + offset), 2f, EaseFuncs.CubicEaseOut,
                this.cameraPos);

            if (isEndOfLevel)
            {
                this.tween.AppendCallback(() =>
                {
                    actor.scene.camera.UnscaledPosition = new Vector2(0, -offset);

                    // temp
                    Reckoning.BuildLevel(actor.scene, new Allowances(), Prime.Seven, Prime.Three);
                    // /temp

                    new LevelTransition(actor.scene.AddActor("LevelStart"), false);
                });
            }
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);

            this.actor.scene.camera.UnscaledPosition = this.cameraPos.CurrentValue;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
