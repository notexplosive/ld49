using System;

namespace LD49.Data
{
    public class FractionalMathExpression : MathExpression
    {
        private readonly MathExpression denominator;
        private readonly MathExpression numerator;

        public FractionalMathExpression(MathExpression numerator, MathExpression denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return this.numerator.Multiply(i);
        }

        public override MathExpression Add(MathExpression i)
        {
            return new SimpleMathMathExpression().Add(this).Add(i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            return new SimpleMathMathExpression().Add(this).Subtract(i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this.numerator,this.denominator.Multiply(i));
        }

        public override string ToString()
        {
            return this.numerator + " / " + this.denominator;
        }
    }
}
