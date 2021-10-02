namespace LD49.Data
{
    public class FractionalMathExpression : PairExpression
    {
        public FractionalMathExpression(MathExpression numerator, MathExpression denominator)
            : base(numerator, denominator, '/')
        {
        }

        private MathExpression Numerator => this.left;
        private MathExpression Denominator => this.right;

        public override MathExpression Multiply(MathExpression i)
        {
            return FractionalMathExpression.AttemptToSimplify(
                new FractionalMathExpression(Numerator.Multiply(i), Denominator));
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return FractionalMathExpression.AttemptToSimplify(
                new FractionalMathExpression(Numerator, Denominator.Multiply(i)));
        }

        private static MathExpression AttemptToSimplify(FractionalMathExpression fraction)
        {
            if (fraction.Numerator is ConstantMathExpression && fraction.Denominator is ConstantMathExpression)
            {
                var attemptResult = fraction.Numerator.DivideBy(fraction.Denominator);
                if (attemptResult is ConstantMathExpression)
                {
                    return attemptResult;
                }
            }

            return fraction;
        }
    }
}
