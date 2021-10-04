using System;
using LD49.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace LD49.Components
{
    public class DropSite : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly DragHand dragHand;
        private readonly Hoverable hoverable;
        private readonly Action<MathExpression> onDroppedInner;
        private readonly TweenAccessors<float> opacity;
        private readonly TweenChain tween = new TweenChain();
        private TweenAccessors<float> borderScale;

        public DropSite(Actor actor, DragHand dragHand, Action<MathExpression> onDroppedInner) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.hoverable = RequireComponent<Hoverable>();
            this.dragHand = dragHand;
            this.onDroppedInner = onDroppedInner;

            this.borderScale = new TweenAccessors<float>(0f);
            this.opacity = new TweenAccessors<float>(0f);

            this.dragHand.Dropped += OnDropped;
            this.dragHand.PickedUp += OnSomethingPickedUp;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.RectF;
            rect.Inflate(60f * this.borderScale.CurrentValue, 20f * this.borderScale.CurrentValue);

            var color = new Color(NumberRenderer.Colors[2], this.opacity.CurrentValue);

            LineDrawer.DrawLine(spriteBatch, rect.TopLeft, rect.TopRight, color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch, rect.TopLeft, rect.BottomLeft, color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch, rect.BottomLeft, rect.BottomRight, color, transform.Depth, 5f);
            LineDrawer.DrawLine(spriteBatch, rect.TopRight, rect.BottomRight, color, transform.Depth, 5f);
            
            if (this.hoverable.IsHovered && this.dragHand.IsHolding)
            {
                spriteBatch.FillRectangle( this.boundingRect.Rect, new Color(NumberRenderer.Colors[3],0.5f), new Depth(300));
            }
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
        }

        private void OnDropped(MathExpression expression)
        {
            if (this.hoverable.IsHovered)
            {
                this.onDroppedInner?.Invoke(expression);
            }

            this.tween.Clear();
            var multi = this.tween.AppendMulticastTween();
            multi.AddChannel().AppendFloatTween(1f, 0.25f, EaseFuncs.CubicEaseOut, this.borderScale);
            multi.AddChannel().AppendFloatTween(0f, 0.25f, EaseFuncs.CubicEaseOut, this.opacity);
        }

        public override void OnDeleteFinished()
        {
            this.dragHand.Dropped -= OnDropped;
            this.dragHand.PickedUp -= OnSomethingPickedUp;
        }

        private void OnSomethingPickedUp()
        {
            this.tween.Clear();
            this.borderScale.CurrentValue = 1f;
            var multi = this.tween.AppendMulticastTween();
            multi.AddChannel().AppendFloatTween(0f, 0.25f, EaseFuncs.CubicEaseOut, this.borderScale);
            multi.AddChannel().AppendFloatTween(1f, 0.25f, EaseFuncs.CubicEaseOut, this.opacity);
        }
    }
}
