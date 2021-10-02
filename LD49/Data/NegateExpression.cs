namespace LD49.Data
{
    public class NegateExpression : UnaryExpression
    {
        public NegateExpression(MathExpression inner) :
            base(inner, "-")
        {
        }

        public override int UnderlyingValue => -(this.inner.UnderlyingValue);
    }
}
