using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class DragNumber : BaseComponent
    {
        private readonly Draggable draggable;
        private readonly ExpressionRenderer renderer;

        public DragNumber(Actor actor) : base(actor)
        {
            this.renderer = RequireComponent<ExpressionRenderer>();

            new Clickable(actor);
            this.draggable = new Draggable(actor);

            this.draggable.DragStart += DragStart;
            this.draggable.DragEnd += DragEnd;
        }

        public override void OnDeleteFinished()
        {
            this.draggable.DragStart -= DragStart;
            this.draggable.DragEnd -= DragEnd;
        }

        private void DragStart(Vector2 mousePos, Vector2 delta)
        {
            Reckoning.DragHand.PickUp(this.renderer);
            Reckoning.DragHand.actor.Visible = true;
        }

        private void DragEnd(Vector2 mousePos, Vector2 delta)
        {
            Reckoning.DragHand.actor.Visible = false;
            Reckoning.DragHand.Drop(mousePos);
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
