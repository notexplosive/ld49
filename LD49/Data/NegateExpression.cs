namespace LD49.Data
{
    public class NegateExpression : UnaryExpression
    {
        private NegateExpression(MathExpression inner) :
            base(inner, "-")
        {
        }

        public static MathExpression Create(MathExpression value)
        {
            if (value is Zero)
            {
                return Zero.Instance;
            }
            
            return new NegateExpression(value);
        }

        public override int UnderlyingValue => -(this.inner.UnderlyingValue);
    }
}
