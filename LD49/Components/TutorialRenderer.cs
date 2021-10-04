using LD49.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD49.Components
{
    public class TutorialRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly TweenChain tween = new TweenChain();
        private TweenAccessors<float> borderScale;
        private TweenAccessors<Vector2> offset;
        private TweenAccessors<float> opacity;

        public TutorialRenderer(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();

            this.offset = new TweenAccessors<Vector2>(Vector2.Zero);
            this.borderScale = new TweenAccessors<float>(0f);
            this.opacity = new TweenAccessors<float>(0f);

            this.tween.AppendWaitTween(3);
            this.tween.AppendCallback(() =>
            {
                this.offset.CurrentValue = Vector2.Zero;
                this.borderScale.CurrentValue = 1f;
                this.opacity.CurrentValue = 1f;
            });
            this.tween.AppendFloatTween(0f, 0.25f, EaseFuncs.EaseOutBack, this.borderScale);
            this.tween.AppendVectorTween(new Vector2(0, -300), 0.5f, EaseFuncs.EaseOutBack, this.offset);
            this.tween.AppendFloatTween(0f, 0.5f, EaseFuncs.CubicEaseOut, this.opacity);
            this.tween.AppendWaitTween(1);

            // this.tween.AppendCallback(() => this.tween.Refresh());
        }

        public override void Update(float dt)
        {
            if (!Reckoning.DragHand.IsHolding)
            {
                this.tween.Refresh();
            }

            this.tween.Update(dt);

            if (this.tween.IsDone())
            {
                this.tween.Refresh();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Reckoning.DragHand.IsHolding)
            {
                var rect = this.boundingRect.RectF;
                rect.Offset(this.offset.CurrentValue);
                rect.Inflate(20f * this.borderScale.CurrentValue, 20f * this.borderScale.CurrentValue);

                var color = new Color(NumberRenderer.Colors[2], this.opacity.CurrentValue);

                LineDrawer.DrawLine(spriteBatch, rect.TopLeft, rect.TopRight, color, transform.Depth, 5f);
                LineDrawer.DrawLine(spriteBatch, rect.TopLeft, rect.BottomLeft, color, transform.Depth, 5f);
                LineDrawer.DrawLine(spriteBatch, rect.BottomLeft, rect.BottomRight, color, transform.Depth, 5f);
                LineDrawer.DrawLine(spriteBatch, rect.TopRight, rect.BottomRight, color, transform.Depth, 5f);
            }
        }
    }
}
