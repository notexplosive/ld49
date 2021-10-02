namespace LD49.Data
{
    public class FractionalMathExpression : PairExpression
    {
        private FractionalMathExpression(MathExpression numerator, MathExpression denominator)
            : base(numerator, denominator, '/')
        {
        }

        public MathExpression Numerator => this.left;
        public MathExpression Denominator => this.right;

        public override int UnderlyingValue => Denominator.UnderlyingValue / Numerator.UnderlyingValue;

        public static MathExpression Create(MathExpression numerator, MathExpression denominator)
        {
            return FractionalMathExpression.Simplify(new FractionalMathExpression(numerator, denominator));
        }

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
            if (fraction.Denominator is One)
            {
                return fraction.Numerator;
            }

            if (fraction.Denominator is Zero)
            {
                return Infinity.Instance;
            }

            if (fraction.Numerator == fraction.Denominator)
            {
                return One.Instance;
            }

            return fraction;
        }
    }
}
