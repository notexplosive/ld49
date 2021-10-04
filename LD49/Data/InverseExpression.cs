namespace LD49.Data
{
    public class InverseExpression : UnaryExpression
    {
        private InverseExpression(MathExpression denominator)
            : base(denominator, "1 / ")
        {
        }

        public override int UnderlyingValue => this.inner.UnderlyingValue;

        public static MathExpression Create(MathExpression expression)
        {
            if (expression is Zero)
            {
                return Infinity.Instance;
            }

            if (expression is InverseExpression givenInverse)
            {
                // (1 / (1 / X)) -> X
                return givenInverse.inner;
            }

            if (expression is One)
            {
                // (1 / 1) -> 1
                return expression;
            }

            if (expression is MultiplyMathExpression multiplyMathExpression)
            {
                // (1 / (X * Y)) -> (1 / X) * (1 / Y)
                return MultiplyMathExpression.InverseEach(multiplyMathExpression);
            }

            return new InverseExpression(expression);
        }
    }
}
