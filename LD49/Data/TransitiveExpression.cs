using System.Collections.Generic;

namespace LD49.Data
{
    public abstract class TransitiveExpression : MathExpression
    {
        protected readonly List<MathExpression> content = new List<MathExpression>();
        private readonly char symbol;

        protected TransitiveExpression(MathExpression expression1, char symbol, MathExpression expression2,
            params MathExpression[] expressions)
        {
            this.symbol = symbol;
            this.content.Add(expression1);
            this.content.Add(expression2);
            this.content.AddRange(expressions);
            this.content.Sort();
        }

        public override string ToString()
        {
            return $"({string.Join($" {this.symbol} ",this.content)})";
        }
    }
}
