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
            return AttemptToSimplify(new FractionalMathExpression(this.numerator.Multiply(i), this.denominator));
        }

        public override MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            throw new NotImplementedException();
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return AttemptToSimplify(new FractionalMathExpression(this.numerator,this.denominator.Multiply(i)));
        }

        private static MathExpression AttemptToSimplify(FractionalMathExpression fraction)
        {
            if (fraction.numerator is ConstantMathExpression && fraction.denominator is ConstantMathExpression)
            {
                var attemptResult = fraction.numerator.DivideBy(fraction.denominator);
                if (attemptResult is ConstantMathExpression)
                {
                    return attemptResult;
                }
            }
            
            return fraction;
        }

        public override string ToString()
        {
            return $"({this.numerator} / {this.denominator})";
        }
    }
}
