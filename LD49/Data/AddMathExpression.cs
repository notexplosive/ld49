using System;

namespace LD49.Data
{
    public class AddMathExpression : TwoPrimeExpression
    {
        public AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
            : base(leftAddend, rightAddend, '+')
        {
        }

        public override MathExpression Multiply(MathExpression i)
        {
            // X * (A + B) -> AX + BX
            return new AddMathExpression(this.left.Multiply(i), this.right.Multiply(i));
        }

        public override MathExpression Add(MathExpression i)
        {
            // X + (A + B)
            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            throw new NotImplementedException();
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            // (A + B) / X -> (A/X + B/X)
            return new AddMathExpression(
                new FractionalMathExpression(this.left, i),
                new FractionalMathExpression(this.right, i));
        }
    }
}
