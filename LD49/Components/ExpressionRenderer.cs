using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;

namespace LD49.Components
{
    public class ExpressionRenderer : BaseComponent
    {
        public readonly BoundingRect boundingRect;
        private readonly int expressionDepth;
        private readonly bool isHoverable;
        private MathExpression expressionImpl;
        private Actor mainChild;

        public ExpressionRenderer(Actor actor, bool isHoverable, MathExpression expression, int expressionDepth = 0) :
            base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.isHoverable = isHoverable;
            this.expressionDepth = expressionDepth;
            Expression = expression;

            this.boundingRect.onSizeChange += size => { this.mainChild.GetComponent<BoundingRect>().SetSize(size); };
        }

        public MathExpression Expression
        {
            get => this.expressionImpl;
            set
            {
                Clear();
                BuildExpression(value);
                this.expressionImpl = value;
                OnExpressionChange?.Invoke();
            }
        }

        public event Action OnExpressionChange;

        private void Clear()
        {
            if (this.mainChild != null)
            {
                this.mainChild.Destroy();
            }

            this.mainChild = transform.AddActorAsChild("MainChild");

            new BoundingRect(this.mainChild, this.boundingRect.Size, this.boundingRect.Offset);
        }

        private void BuildExpression(MathExpression expression)
        {
            if (expression is Number number)
            {
                new NumberRenderer(this.mainChild, number, this.isHoverable);
            }
            else if (expression is TransitiveExpression transitiveExpression)
            {
                new TransitiveExpressionRenderer(this.mainChild, this.isHoverable,
                    transitiveExpression, this, this.expressionDepth);
            }
            else if (expression is NamedVariable variable)
            {
                new NamedVariableRenderer(this.mainChild, variable);
            }
            else if (expression is InverseExpression inverseExpression)
            {
                new UnaryExpressionRenderer(this.mainChild, inverseExpression, this.isHoverable);
            }
            else if (expression is NegateExpression negateExpression)
            {
                new UnaryExpressionRenderer(this.mainChild, negateExpression, this.isHoverable);
            }
        }
    }
}
