using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class DragNumber : BaseComponent
    {
        private readonly Draggable draggable;
        public readonly ExpressionRenderer expressionRenderer;
        private readonly ExpressionRenderer renderer;

        public DragNumber(Actor actor, MathExpression expression) : base(actor)
        {
            this.expressionRenderer = new ExpressionRenderer(actor, false, expression);
            new Hoverable(actor);
            new TooltipProvider(actor, expression.ToString());

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
            GameRunner.DragHand.PickUp(this.renderer);
            GameRunner.DragHand.actor.Visible = true;
        }

        private void DragEnd(Vector2 mousePos, Vector2 delta)
        {
            GameRunner.DragHand.actor.Visible = false;
            GameRunner.DragHand.Drop(mousePos);
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
