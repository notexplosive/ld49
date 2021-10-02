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
            return FractionalMathExpression.Simplify(new FractionalMathExpression(Numerator.Multiply(i), Denominator));
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return FractionalMathExpression.Simplify(new FractionalMathExpression(Numerator, Denominator.Multiply(i)));
        }

        private static MathExpression Simplify(FractionalMathExpression fraction)
        {
            if (fraction.Numerator == fraction.Denominator)
            {
                return One.Instance;
            }

            return fraction;
        }

        public override int UnderlyingValue => this.Denominator.UnderlyingValue / this.Numerator.UnderlyingValue;
    }
}
