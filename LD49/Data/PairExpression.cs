using System.Linq;

namespace LD49.Data
{
    public abstract class PairExpression : MathExpression
    {
        protected readonly MathExpression left;
        protected readonly MathExpression right;
        private readonly char symbol;

        protected PairExpression(MathExpression left, MathExpression right, char symbol)
        {
            this.left = left;
            this.right = right;

            this.symbol = symbol;
        }

        public override string ToString()
        {
            return $"({this.left} {symbol} {this.right})";
        }
    }
}
