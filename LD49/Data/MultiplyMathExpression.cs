using System.Collections.Generic;

namespace LD49.Data
{
    public class MultiplyMathExpression : TransitiveExpression
    {
        private MultiplyMathExpression(MathExpression rightFactor, MathExpression leftFactor)
            : base('*', leftFactor, rightFactor)
        {
        }

        private MultiplyMathExpression(params MathExpression[] expressions) : base('*', expressions)
        {
        }

        public override int UnderlyingValue
        {
            get
            {
                var total = 0;
                foreach (var n in this.content)
                {
                    total *= n.UnderlyingValue;
                }

                return total;
            }
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

            var allExpressions = new List<MathExpression>();
            
            foreach (var item in expression.content)
            {
                allExpressions.Add(item);
            }

            for (var i = 0; i < allExpressions.Count; i++)
            {
                for (var j = 0; j < allExpressions.Count; j++)
                {
                    if (i != j)
                    {
                        var left = allExpressions[i];
                        var right = allExpressions[j];

                        if (left == MathOperator.Inverse(right) || right == MathOperator.Inverse(left))
                        {
                            allExpressions[i] = One.Instance;
                            allExpressions[j] = One.Instance;
                        }
                    }
                }
            }

            var finalExpressions = new List<MathExpression>();

            foreach (var exp in allExpressions)
            {
                if (exp != One.Instance)
                {
                    finalExpressions.Add(exp);
                }
            }

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

        private class Builder : TransitiveBuilder<Builder, MultiplyMathExpression>
        {
            public override MultiplyMathExpression Build()
            {
                return new MultiplyMathExpression(this.content.ToArray());
            }
        }
    }
}
