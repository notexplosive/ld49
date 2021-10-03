using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class DropSite : BaseComponent
    {
        private readonly DragHand dragHand;
        private readonly Hoverable hoverable;
        private readonly Action<MathExpression> onDroppedInner;

        public DropSite(Actor actor, DragHand dragHand, Action<MathExpression> onDroppedInner) : base(actor)
        {
            this.hoverable = RequireComponent<Hoverable>();
            this.dragHand = dragHand;
            this.onDroppedInner = onDroppedInner;

            this.dragHand.Dropped += OnDropped;
        }

        private void OnDropped(MathExpression expression)
        {
            if (this.hoverable.IsHovered)
            {
                this.onDroppedInner?.Invoke(expression);
            }
        }

        public override void OnDeleteFinished()
        {
            this.dragHand.Dropped -= OnDropped;
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
