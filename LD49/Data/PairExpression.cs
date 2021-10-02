using System.Collections.Generic;

namespace LD49.Data
{
    public abstract class PairExpression : MathExpression
    {
        protected readonly MathExpression left;
        protected readonly MathExpression right;
        private readonly char symbol;

        protected PairExpression(MathExpression left, MathExpression right, char symbol)
        {
            if (left is Prime p1 && right is Prime p2)
            {
                var list = new List<Prime> {p1, p2};
                list.Sort();
                this.left = list[0];
                this.right = list[1];
            }
            else
            {
                this.left = left;
                this.right = right;
            }
            
            
            this.symbol = symbol;
        }

        public override string ToString()
        {
            return $"({this.left} {symbol} {this.right})";
        }
    }
}
