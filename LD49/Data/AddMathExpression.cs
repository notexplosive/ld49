using System.Collections.Generic;
using System.Linq;

namespace LD49.Data
{
    public class AddMathExpression : TransitiveExpression
    {
        private AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
            : base('+', leftAddend, rightAddend)
        {
        }

        private AddMathExpression(params MathExpression[] expressions) : base('+', expressions)
        {
        }

        public override int UnderlyingValue
        {
            get
            {
                var total = 0;
                foreach (var n in this.content)
                {
                    total += n.UnderlyingValue;
                }

                return total;
            }
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

                        if (left == MathExpression.Negate(right) || right == MathExpression.Negate(left))
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

        public override MathExpression DivideBy(MathExpression i)
        {
            // (A + B) / X -> (A* 1/X) + (B * 1/X)
            return new AddMathExpression(
                MultiplyMathExpression.Create(InverseExpression.Create(this.content[0]), i),
                MultiplyMathExpression.Create(InverseExpression.Create(this.content[1]), i)
            );
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
