namespace LD49.Data
{
    public abstract class UnaryExpression : MathExpression
    {
        protected readonly MathExpression inner;
        private readonly string symbol;

        protected UnaryExpression(MathExpression inner, string symbol)
        {
            this.inner = inner;
            this.symbol = symbol;
        }

        public override string ToString()
        {
            return $"({this.symbol}{this.inner})";
        }

        public MathExpression GetInnerValue()
        {
            return this.inner;
        }
    }
}
