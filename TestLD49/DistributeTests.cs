using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class DistributeTests
    {
        [Fact]
        public void simple_distribute()
        {
            var thingToDistribute = NamedVariable.X;
            var addExpression = MathOperator.Add(NamedVariable.Y, One.Instance);

            AddMathExpression.Distribute(thingToDistribute, addExpression as AddMathExpression).ToString().Should()
                .Be("(X + (X * Y))");
        }

        [Fact]
        public void complex_distribute()
        {
            var thingToDistribute =
                MathOperator.Negate(
                    MathOperator.Add(
                        NamedVariable.X,
                        Prime.Eleven));
            var addExpression =
                MathOperator.Add(
                    MathOperator.Add(
                        NamedVariable.Y,
                        One.Instance),
                    Prime.Seven);

            AddMathExpression.Distribute(thingToDistribute, addExpression as AddMathExpression).ToString().Should()
                .Be("((Y * (-(X + 11))) + (7 * (-(X + 11))) + (-(X + 11)))");
        }

        [Fact]
        public void foil_happens_naturally()
        {
            var thingToDistribute =
                MathOperator.Add(
                    NamedVariable.X,
                    NamedVariable.Y);
            var addExpression =
                MathOperator.Add(
                    NamedVariable.Z,
                    Prime.Seven);

            AddMathExpression.Distribute(thingToDistribute, addExpression as AddMathExpression).ToString().Should()
                .Be("((Z * (X + Y)) + ((X + Y) * 7))");
        }
    }
}
