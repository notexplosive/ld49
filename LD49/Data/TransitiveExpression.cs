using System.Collections.Generic;
using System.Collections.Immutable;

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

        protected TransitiveExpression(char symbol, TransitiveExpression leftSet, TransitiveExpression rightSet) :
            this(symbol)
        {
            var bothSetsContent = new List<MathExpression>();
            bothSetsContent.AddRange(leftSet.content);
            bothSetsContent.AddRange(rightSet.content);
            bothSetsContent.Sort();

            this.content = bothSetsContent.ToImmutableArray();
        }

        protected TransitiveExpression(char symbol, TransitiveExpression leftSet, Prime rightPrime) : this(symbol)
        {
            var bothSetsContent = new List<MathExpression>();
            bothSetsContent.AddRange(leftSet.content);
            bothSetsContent.Add(rightPrime);
            bothSetsContent.Sort();

            this.content = bothSetsContent.ToImmutableArray();
        }

        protected TransitiveExpression(char symbol, Prime leftPrime, TransitiveExpression rightSet) :
            this(symbol)
        {
            var bothSetsContent = new List<MathExpression>();
            bothSetsContent.Add(leftPrime);
            bothSetsContent.AddRange(rightSet.content);
            bothSetsContent.Sort();

            this.content = bothSetsContent.ToImmutableArray();
        }

        public override string ToString()
        {
            return $"({string.Join($" {this.symbol} ", this.content)})";
        }
    }
}
