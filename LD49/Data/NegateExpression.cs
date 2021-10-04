namespace LD49.Data
{
    public class NegateExpression : UnaryExpression
    {
        private NegateExpression(MathExpression inner) :
            base(inner, "-")
        {
        }

        public override int UnderlyingValue => -this.inner.UnderlyingValue;

        public static MathExpression Create(MathExpression expression)
        {
            if (expression is Zero)
            {
                // (-0) -> 0
                return Zero.Instance;
            }

            if (expression is NegateExpression givenNegate)
            {
                // (-(-X)) -> X
                return givenNegate.inner;
            }

            if (expression is MultiplyMathExpression multiplyMathExpression)
            {
                // -(X * Y) -> (-1) * X * Y
                return MathOperator.Multiply(expression, NegateExpression.Create(One.Instance));
            }

            if (expression is AddMathExpression addMathExpression)
            {
                // -(X + Y) -> (-X) + (-Y)
                return AddMathExpression.Distribute(NegateExpression.Create(One.Instance), addMathExpression);
            }

            return new NegateExpression(expression);
        }
    }
}
