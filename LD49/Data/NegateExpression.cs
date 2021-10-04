namespace LD49.Data
{
    public class NegateExpression : UnaryExpression
    {
        private NegateExpression(MathExpression inner) :
            base(inner, "-")
        {
        }

        public override int UnderlyingValue => -this.inner.UnderlyingValue;

        public static MathExpression Create(MathExpression value)
        {
            if (value is Zero)
            {
                return Zero.Instance;
            }

            if (value is NegateExpression givenNegate)
            {
                // (-(-X)) -> X
                return givenNegate.inner;
            }

            if (value is MultiplyMathExpression multiplyMathExpression)
            {
                // -(X * Y) -> (-1) * X * Y
                return MathOperator.Multiply(value, NegateExpression.Create(One.Instance));
            }

            if (value is AddMathExpression addMathExpression)
            {
                // -(X + Y) -> (-X) + (-Y)
                return AddMathExpression.Distribute(NegateExpression.Create(One.Instance), addMathExpression);
            }

            return new NegateExpression(value);
        }
    }
}
