using System.Collections.Generic;
using System.Linq;

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
            if (left is MultiplyMathExpression mul1 && right is MultiplyMathExpression mul2)
            {
                return MultiplyMathExpression.Simplify(new Builder().Add(mul1).Add(mul2).Build());
            }

            if (left is MultiplyMathExpression leftMul && right is Prime rightPrime)
            {
                return MultiplyMathExpression.Simplify(new Builder().Add(leftMul).Add(rightPrime).Build());
            }

            if (left is Prime leftPrime && right is MultiplyMathExpression rightMul)
            {
                return MultiplyMathExpression.Simplify(new Builder().Add(leftPrime).Add(rightMul).Build());
            }

            // (1 / X) * (1 / Y) -> 1 / (X * Y)
            if (left is InverseExpression leftInverse && right is InverseExpression rightInverse)
            {
                return InverseExpression.Create(
                    MultiplyMathExpression.Create(
                        MathExpression.Inverse(leftInverse),
                        MathExpression.Inverse(rightInverse)));
            }

            return MultiplyMathExpression.Simplify(new MultiplyMathExpression(left, right));
        }

        private static MathExpression Simplify(MultiplyMathExpression expression)
        {
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

                        if (left == MathExpression.Inverse(right) || right == MathExpression.Inverse(left))
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
                return new MultiplyMathExpression(finalExpressions.ToArray());
            }

            if (finalExpressions.Count == 0)
            {
                return One.Instance;
            }

            return finalExpressions[0];
        }

        private class Builder : TransitiveBuilder<Builder, MultiplyMathExpression>
        {
            public MultiplyMathExpression Build()
            {
                return new MultiplyMathExpression(this.content.ToArray());
            }
        }
    }
}
