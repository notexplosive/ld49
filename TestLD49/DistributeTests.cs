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
                .Be("((-X) + (-11) + (Y * ((-X) + (-11))) + (7 * ((-X) + (-11))))");
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

        [Fact]
        public void negate_a_multiplication_should_multiply_by_negative_one()
        {
            MathOperator.Negate(MathOperator.Multiply(NamedVariable.X, NamedVariable.Y)).Should().Be(
                MathOperator.Multiply(MathOperator.Multiply(NamedVariable.X, NamedVariable.Y),
                    MathOperator.Negate(One.Instance)));
        }

        [Fact]
        public void multiply_by_negative_one_twice_to_be_positive()
        {
            MathOperator.Multiply(
                    MathOperator.Negate(One.Instance),
                    MathOperator.Multiply(
                        MathOperator.Negate(One.Instance),
                        MathOperator.Multiply(
                            NamedVariable.X,
                            NamedVariable.Y)))
                .Should().Be(MathOperator.Multiply(NamedVariable.X, NamedVariable.Y));
        }

        [Fact]
        public void negate_distributes_across_an_add()
        {
            MathOperator.Negate(MathOperator.Add(NamedVariable.X, NamedVariable.Y))
                .Should().Be(
                    MathOperator.Add(MathOperator.Negate(NamedVariable.X), MathOperator.Negate(NamedVariable.Y))
                );
        }

        [Fact]
        public void inverse_distributes_across_multiplication()
        {
            MathOperator.Inverse(MathOperator.Multiply(NamedVariable.X, NamedVariable.Y))
                .Should().Be(
                    MathOperator.Multiply(MathOperator.Inverse(NamedVariable.X),MathOperator.Inverse(NamedVariable.Y))
                );
        }

        [Fact]
        public void inverse_four_times_cancels_out()
        {
            MathOperator.Inverse(
                MathOperator.Inverse(
                    MathOperator.Inverse(
                        MathOperator.Inverse(NamedVariable.X
                        )))).Should().Be(NamedVariable.X);
        }
        
        [Fact]
        public void inverse_and_negate_are_order_agnostic()
        {
            MathOperator.Inverse(
                MathOperator.Negate(
                    NamedVariable.X
                        )).Should().Be(
                MathOperator.Negate(
                    MathOperator.Inverse(
                NamedVariable.X)));
            
            MathOperator.Negate(
                MathOperator.Inverse(
                    NamedVariable.X
                )).Should().Be(
                MathOperator.Inverse(
                    MathOperator.Negate(
                        NamedVariable.X)));
        }
    }
}
