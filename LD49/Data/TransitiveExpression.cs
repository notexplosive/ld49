using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Transactions;

namespace LD49.Data
{
    public abstract class TransitiveExpression : MathExpression
    {
        protected readonly ImmutableArray<MathExpression> content;
        private readonly char symbol;

        private TransitiveExpression(char symbol)
        {
            this.symbol = symbol;
        }

        protected TransitiveExpression(char symbol, params MathExpression[] expressions) : this(symbol)
        {
            var contentList = new List<MathExpression>();
            contentList.AddRange(expressions);
            contentList.Sort();

            this.content = contentList.ToImmutableArray();
        }

        public override string ToString()
        {
            return $"({string.Join($" {this.symbol} ", this.content)})";
        }

        public List<MathExpression> GetContent()
        {
            return this.content.ToList();
        }

        protected abstract class TransitiveBuilder<TBuilderType, TExpressionType> 
            where TBuilderType : class 
            where TExpressionType : TransitiveExpression
        {
            protected readonly List<MathExpression> content = new List<MathExpression>();

            public TBuilderType Add(MathExpression expression)
            {
                if (expression is TExpressionType transitiveExpression)
                {
                    AddMany(transitiveExpression.content.ToArray());
                }
                else
                {
                    this.content.Add(expression);
                }

                return this as TBuilderType;
            }

            private void AddMany(params MathExpression[] expressions)
            {
                foreach (var expression in expressions)
                {
                    Add(expression);
                }
            }

            public abstract TExpressionType Build();
        }
    }
}
