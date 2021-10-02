namespace LD49.Data
{
    public abstract class UnaryExpression : MathExpression
    {
        protected readonly MathExpression inner;
        private readonly char symbol;

        protected UnaryExpression(MathExpression inner, char symbol)
        {
            this.inner = inner;
            this.symbol = symbol;
        }

        public override string ToString()
        {
            return $"({this.symbol}{this.inner})";
        }
    }
}
