using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.ThirdParty;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public class OverlayTrigger : BaseComponent
    {
        private readonly DragHand dragHand;
        private readonly Vector2 offPosition;
        private readonly Vector2 onPosition;
        private readonly Hoverable targetHoverable;
        private readonly TweenChain tween = new TweenChain();
        private bool isOn;

        public OverlayTrigger(Actor actor, DragHand dragHand, Hoverable targetHoverable, Vector2 onPosition) :
            base(actor)
        {
            this.dragHand = dragHand;
            this.targetHoverable = targetHoverable;
            this.onPosition = onPosition;
            this.offPosition = transform.Position;
        }

        public override void Update(float dt)
        {
            if (this.targetHoverable.IsSoftHovered && this.dragHand.IsHolding)
            {
                BecomeOn();
            }
            else
            {
                BecomeOff();
            }

            this.tween.Update(dt);
        }

        public void BecomeOn()
        {
            if (!this.isOn)
            {
                TweenTo(this.onPosition);
            }

            this.isOn = true;
        }

        public void BecomeOff()
        {
            if (this.isOn)
            {
                TweenTo(this.offPosition);
            }
            
            this.isOn = false;
        }

        private void TweenTo(Vector2 targetPosition)
        {
            var position =
                new TweenAccessors<Vector2>(() => transform.Position, value => transform.Position = value);
            this.tween.Clear();
            this.tween.AppendVectorTween(targetPosition, 0.25f, EaseFuncs.CubicEaseOut, position);
        }
    }
}
