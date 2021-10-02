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
            if (expression is InverseExpression givenInverse)
            {
                // Reverse the inverse if we're already an inverse
                return givenInverse.inner;
            }

            return new InverseExpression(expression);
        }
    }
}
