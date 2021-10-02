using System;
using System.Collections.Generic;
using System.Linq;

namespace LD49.Data
{
    public class AddMathExpression : TransitiveExpression
    {
        private AddMathExpression(params MathExpression[] expressions) : base('+', expressions)
        {
        }

        protected override int UnderlyingFunction(int prevValue, int currentValue)
        {
            return prevValue + currentValue;
        }

        public static MathExpression Create(MathExpression left, MathExpression right)
        {
            // X + (A + B) -> X + A + B
            return AddMathExpression.Simplify(new Builder().Add(left).Add(right).Build());
        }

        private static MathExpression Simplify(AddMathExpression expression)
        {
            // Cancel out negates
            var finalExpressions =
                AddMathExpression.FilterExpressions(expression.content.ToArray(), Zero.Instance, MathOperator.Negate);

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
                return Zero.Instance;
            }

            return finalExpressions[0];
        }

        private static List<MathExpression> FilterExpressions(MathExpression[] allExpressions,
            SpecialNumber eraseNumber, Func<MathExpression, MathExpression> invertFunction)
        {
            for (var i = 0; i < allExpressions.Length; i++)
            {
                for (var j = 0; j < allExpressions.Length; j++)
                {
                    if (i != j)
                    {
                        var left = allExpressions[i];
                        var right = allExpressions[j];

                        if (left == invertFunction(right) || right == invertFunction(left))
                        {
                            allExpressions[i] = eraseNumber;
                            allExpressions[j] = eraseNumber;
                        }
                    }
                }
            }

            var result = new List<MathExpression>();

            foreach (var exp in allExpressions)
            {
                if (exp != eraseNumber)
                {
                    result.Add(exp);
                }
            }

            return result;
        }

        private class Builder : TransitiveBuilder<Builder, AddMathExpression>
        {
            public override AddMathExpression Build()
            {
                return new AddMathExpression(this.content.ToArray());
            }
        }
    }
}
