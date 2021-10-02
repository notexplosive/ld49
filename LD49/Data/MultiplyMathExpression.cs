﻿using System.Collections.Generic;

namespace LD49.Data
{
    public class MultiplyMathExpression : TransitiveExpression
    {
        private MultiplyMathExpression(MathExpression rightFactor, MathExpression leftFactor)
            : base('*', leftFactor, rightFactor)
        {
        }


        private MultiplyMathExpression(MultiplyMathExpression leftSet, MultiplyMathExpression rightSet) : base('*', leftSet, rightSet)
        {
        }

        private MultiplyMathExpression(MultiplyMathExpression leftSet, Prime rightPrime) : base('*', leftSet, rightPrime)
        {
        }

        private MultiplyMathExpression(Prime leftPrime, MultiplyMathExpression rightSet) : base('*', leftPrime, rightSet)
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
                return MultiplyMathExpression.Simplify(new MultiplyMathExpression(mul1, mul2));
            }

            if (left is MultiplyMathExpression leftMul && right is Prime rightPrime)
            {
                return MultiplyMathExpression.Simplify(new MultiplyMathExpression(leftMul, rightPrime));
            }

            if (left is Prime leftPrime && right is MultiplyMathExpression rightAdd)
            {
                return MultiplyMathExpression.Simplify(new MultiplyMathExpression(leftPrime, rightAdd));
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

                        if (left == MathExpression.Negate(right) || right == MathExpression.Negate(left))
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
            else
            {
                return finalExpressions[0];
            }
        }
    }
}
