using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace LD49.Components
{
    /// <summary>
    /// Totall repurposed as the "hover feedback" class, tooltips are no longer a thing
    /// lmao 420 blaze it pptphpthptphpthpt
    /// </summary>
    public class TooltipProvider : BaseComponent
    {
        private readonly string tooltipText;
        private Hoverable hoverable;
        private bool hasSetup = false;
        private readonly BoundingRect boundingRect;

        public TooltipProvider(Actor actor, string tooltipText) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.tooltipText = tooltipText;
        }

        public override void Update(float dt)
        {
            if (!this.hasSetup)
            {
                this.hoverable = this.actor.GetComponent<Hoverable>();
                if (this.hoverable != null)
                {
                    this.hoverable.OnHoverStart += OnHovered;
                    this.hoverable.OnHoverEnd += OnUnhovered;
                }

                this.hasSetup = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.hoverable != null && this.hoverable.IsHovered && (GameRunner.DragHand == null || !GameRunner.DragHand.IsHolding))
            {
                spriteBatch.FillRectangle(this.boundingRect.Rect, new Color(Color.Orange, 0.15f), transform.Depth + 20);
            }
        }

        private void OnHovered()
        {
            // Reckoning.tooltipTextRenderer.Text = this.tooltipText;
        }

        private void OnUnhovered()
        {
        }

        public override void OnDeleteFinished()
        {
            if (this.hoverable != null)
            {
                this.hoverable.OnHoverStart -= OnHovered;
                this.hoverable.OnHoverEnd -= OnUnhovered;
            }
        }
    }
}
