using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LD49.Data
{
    public abstract class TransitiveExpression : MathExpression
    {
        public enum SubType
        {
            Add,
            Multiply
        }
        
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

        public override int UnderlyingValue
        {
            get
            {
                var total = 0;
                foreach (var n in this.content)
                {
                    total = UnderlyingFunction(total, n.UnderlyingValue);
                }

                return total;
            }
        }

        protected abstract int UnderlyingFunction(int prevValue, int currentValue);

        public override string ToString()
        {
            return $"({string.Join($" {this.symbol} ", this.content)})";
        }

        protected static List<MathExpression> FilterOppositeExpressions(MathExpression[] allExpressions,
            SpecialNumber eraser, Func<MathExpression, MathExpression> invertFunction)
        {
            for (var i = 0; i < allExpressions.Length; i++)
            {
                for (var j = 0; j < allExpressions.Length; j++)
                {
                    if (i != j)
                    {
                        var left = allExpressions[i];
                        var right = allExpressions[j];

                        if (left == invertFunction(right) || right == invertFunction(left))
                        {
                            allExpressions[i] = eraser;
                            allExpressions[j] = eraser;
                        }
                    }
                }
            }

            var result = new List<MathExpression>();

            foreach (var exp in allExpressions)
            {
                if (exp != eraser)
                {
                    result.Add(exp);
                }
            }

            return result;
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

        public MathExpression[] GetContents()
        {
            return this.content.ToArray();
        }

        public T GetFirstExpression<T>() where T : TransitiveExpression
        {
            foreach (var expression in this.content)
            {
                if (expression is T result)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
