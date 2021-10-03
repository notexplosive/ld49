using Machina.Components;
using Machina.Engine;

namespace LD49.Components
{
    public class TooltipProvider : BaseComponent
    {
        private readonly string tooltipText;
        private Hoverable hoverable;
        private bool hasSetup = false;

        public TooltipProvider(Actor actor, string tooltipText) : base(actor)
        {
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
