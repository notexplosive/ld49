using System.Linq;

namespace LD49.Data
{
    public class MultiplyMathExpression : TransitiveExpression
    {
        private MultiplyMathExpression(params MathExpression[] expressions) : base('*', expressions)
        {
        }

        protected override int UnderlyingFunction(int prevValue, int currentValue)
        {
            return prevValue * currentValue;
        }

        public static MathExpression Create(MathExpression left, MathExpression right)
        {
            // X * (A * B) -> X * A * B
            return MultiplyMathExpression.Simplify(new Builder().Add(left).Add(right).Build());
        }

        private static MathExpression Simplify(MultiplyMathExpression expression)
        {
            if (expression.content.Contains(Zero.Instance))
            {
                // Anything times zero is zero
                return Zero.Instance;
            }

            var tempContent = expression.content.ToArray();

            // (-1) * X * Y -> -X * -Y
            for (var i = 0; i < tempContent.Length; i++)
            {
                if (tempContent[i] is NegateExpression negate && negate.GetInnerValue() == One.Instance)
                {
                    for (var j = 0; j < tempContent.Length; j++)
                    {
                        tempContent[j] = MathOperator.Negate(tempContent[j]);
                    }
                }
            }

            // Cancel out inverses
            var finalExpressions =
                TransitiveExpression.FilterOppositeExpressions(tempContent, One.Instance,
                    MathOperator.Inverse);

            if (finalExpressions.Count > 1)
            {
                var builder = new Builder();
                foreach (var item in finalExpressions.ToArray())
                {
                    builder.Add(item);
                }

                return builder.Build();
            }

            if (finalExpressions.Count == 0)
            {
                return One.Instance;
            }

            return finalExpressions[0];
        }

        public bool CanDistribute()
        {
            var numberOfAddExpressions = 0;
            foreach (var item in this.content)
            {
                if (item is AddMathExpression)
                {
                    numberOfAddExpressions++;
                }
            }

            return this.content.Length == 2 && numberOfAddExpressions > 0;
        }

        public MathExpression GetExpressionExcept(MathExpression expressionToRemove)
        {
            var builder = new Builder();
            var hasSkipped = false;

            foreach (var expression in this.content)
            {
                if (expression != expressionToRemove || hasSkipped)
                {
                    builder.Add(expression);
                }

                if (expression == expressionToRemove)
                {
                    hasSkipped = true;
                }
            }

            return MultiplyMathExpression.Simplify(builder.Build());
        }

        private class Builder : TransitiveBuilder<Builder, MultiplyMathExpression>
        {
            public override MultiplyMathExpression Build()
            {
                return new MultiplyMathExpression(this.content.ToArray());
            }
        }
    }
}
