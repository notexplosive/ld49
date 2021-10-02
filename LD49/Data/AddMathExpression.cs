using System.Collections.Generic;

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
            var allExpressions = new List<MathExpression>();

            foreach (var item in expression.content)
            {
                allExpressions.Add(item);
            }

            // Cancel out negates
            for (var i = 0; i < allExpressions.Count; i++)
            {
                for (var j = 0; j < allExpressions.Count; j++)
                {
                    if (i != j)
                    {
                        var left = allExpressions[i];
                        var right = allExpressions[j];

                        if (left == MathOperator.Negate(right) || right == MathOperator.Negate(left))
                        {
                            allExpressions[i] = Zero.Instance;
                            allExpressions[j] = Zero.Instance;
                        }
                    }
                }
            }

            var finalExpressions = new List<MathExpression>();

            foreach (var exp in allExpressions)
            {
                if (exp != Zero.Instance)
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
                return Zero.Instance;
            }

            return finalExpressions[0];
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
