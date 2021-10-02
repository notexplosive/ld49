namespace LD49.Data
{
    public class FractionalMathExpression : PairExpression
    {
        public FractionalMathExpression(MathExpression numerator, MathExpression denominator)
            : base(numerator, denominator, '/' , false)
        {
        }

        private MathExpression Numerator => this.left;
        private MathExpression Denominator => this.right;

        public override MathExpression Multiply(MathExpression i)
        {
            return new FractionalMathExpression(Numerator.Multiply(i), Denominator);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(Numerator, Denominator.Multiply(i));
        }
    }
}
