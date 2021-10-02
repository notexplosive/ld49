using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class FractionalMathExpressionTests
    {
        [Fact]
        public void create_one_over_three()
        {
            InverseExpression.Create(Prime.Three).ToString().Should().Be("(1 / 3)");
        }

        [Fact]
        public void multiply_fraction()
        {
            MathOperator.Multiply(InverseExpression.Create(Prime.Three), Prime.Five).ToString().Should()
                .Be("((1 / 3) * 5)");
        }

        [Fact]
        public void multiply_fraction_to_simple()
        {
            MathOperator.Multiply(InverseExpression.Create(Prime.Three), Prime.Three).ToString().Should().Be("1");
        }

        [Fact]
        public void add_to_fraction()
        {
            MathOperator.Add(MultiplyMathExpression.Create(InverseExpression.Create(Prime.Seven), Prime.Three),
                    Prime.Thirteen)
                .ToString().Should().Be("((3 * (1 / 7)) + 13)");
        }

        [Fact]
        public void add_and_multiply_mixed()
        {
            MathOperator.Add(
                    MathOperator.Multiply(
                        MathOperator.Add(Prime.Eleven, Prime.Seven),
                        Prime.Three), Prime.Thirteen).ToString().Should()
                .Be("((3 * (7 + 11)) + 13)");
        }
    }
}
