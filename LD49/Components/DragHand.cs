using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class DragHand : BaseComponent
    {
        private readonly ExpressionRenderer expressionRenderer;

        public DragHand(Actor actor) : base(actor)
        {
            this.actor.Visible = false;
            this.expressionRenderer = new ExpressionRenderer(this.actor, false, Zero.Instance);
        }

        public MathExpression Expression
        {
            get => this.expressionRenderer.Expression;
            set => this.expressionRenderer.Expression = value;
        }

        public override void Update(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void OnMouseUpdate(Vector2 currentPosition, Vector2 positionDelta, Vector2 rawDelta)
        {
            transform.Position = currentPosition;
        }
    }
}
