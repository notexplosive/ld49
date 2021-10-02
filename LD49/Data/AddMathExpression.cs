using System.Collections.Generic;

namespace LD49.Data
{
    public class AddMathExpression : TransitiveExpression
    {
        private AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
            : base('+', leftAddend, rightAddend)
        {
        }

        private AddMathExpression(AddMathExpression leftSet, AddMathExpression rightSet) : base('+', leftSet, rightSet)
        {
        }

        private AddMathExpression(AddMathExpression leftSet, Prime rightPrime) : base('+', leftSet, rightPrime)
        {
        }

        private AddMathExpression(Prime leftPrime, AddMathExpression rightSet) : base('+', leftPrime, rightSet)
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
            if (left is AddMathExpression add1 && right is AddMathExpression add2)
            {
                return AddMathExpression.Simplify(new AddMathExpression(add1, add2));
            }

            if (left is AddMathExpression leftAdd && right is Prime rightPrime)
            {
                return AddMathExpression.Simplify(new AddMathExpression(leftAdd, rightPrime));
            }

            if (left is Prime leftPrime && right is AddMathExpression rightAdd)
            {
                return AddMathExpression.Simplify(new AddMathExpression(leftPrime, rightAdd));
            }

            return AddMathExpression.Simplify(new AddMathExpression(left, right));
        }

        private static MathExpression Simplify(AddMathExpression expression)
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
                return new AddMathExpression(finalExpressions.ToArray());
            }

            if (finalExpressions.Count == 0)
            {
                return Zero.Instance;
            }
            else
            {
                return finalExpressions[0];
            }
        }

        public override MathExpression Multiply(MathExpression i)
        {
            // X * (A + B) -> AX + BX
            return new AddMathExpression(this.content[0].Multiply(i), this.content[1].Multiply(i));
        }

        public override MathExpression Add(MathExpression i)
        {
            return base.Add(i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            // (A + B) / X -> (A/X + B/X)
            return new AddMathExpression(
                new FractionalMathExpression(this.content[0], i),
                new FractionalMathExpression(this.content[1], i));
        }
    }
}
