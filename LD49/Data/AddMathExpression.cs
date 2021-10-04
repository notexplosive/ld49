using System.Linq;

namespace LD49.Data
{
    public class AddMathExpression : TransitiveExpression
    {
        private AddMathExpression(params MathExpression[] expressions) : base('+', expressions)
        {
        }

        protected override int UnderlyingFunction(int prevValue, int currentValue)
        {
            return prevValue + currentValue;
        }

        public static MathExpression CreateMany(params MathExpression[] many)
        {
            var builder = new Builder();
            foreach (var item in many)
            {
                builder.Add(item);
            }

            return AddMathExpression.Simplify(builder.Build());
        }

        public static MathExpression Create(MathExpression left, MathExpression right)
        {
            // X + (A + B) -> X + A + B
            return AddMathExpression.Simplify(new Builder().Add(left).Add(right).Build());
        }

        public static MathExpression Distribute(MathExpression expression, AddMathExpression addExpression)
        {
            MathExpression result = Zero.Instance;
            foreach (var addItem in addExpression.content)
            {
                result = MathOperator.Add(result, MathOperator.Multiply(expression, addItem));
            }

            return result;
        }

        private static MathExpression Simplify(AddMathExpression expression)
        {
            // Cancel out negates
            var finalExpressions =
                TransitiveExpression.FilterOppositeExpressions(expression.content.ToArray(), Zero.Instance,
                    MathOperator.Negate);

            if (finalExpressions.Contains(Infinity.Instance))
            {
                return Infinity.Instance;
            }
            
            if (finalExpressions.Count > 1)
            {
                var builder = new Builder();
                foreach (var item in finalExpressions.ToArray())
                {
                    builder.Add(item);
                }

                return builder.Build();
            }

            if (finalExpressions.Count == 0)
            {
                return Zero.Instance;
            }

            return finalExpressions[0];
        }

        private class Builder : TransitiveBuilder<Builder, AddMathExpression>
        {
            public override AddMathExpression Build()
            {
                return new AddMathExpression(this.content.ToArray());
            }
        }
    }
}
