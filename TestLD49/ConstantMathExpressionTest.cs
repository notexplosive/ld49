using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class ConstantMathExpressionTest
    {
        [Fact]
        public void divide_into_constant()
        {
            new ConstantMathExpression(10).DivideBy(2).ToString().Should().Be("5");
        }

        [Fact]
        public void divide_into_fraction()
        {
            new ConstantMathExpression(10).DivideBy(3).ToString().Should().Be("(10 / 3)");
        }

        [Fact]
        public void empty_expression_is_zero()
        {
            new ConstantMathExpression(0).ToString().Should().Be("0");
        }

        [Fact]
        public void add_one()
        {
            new ConstantMathExpression(0).Add(1).ToString().Should().Be("1");
        }

        [Fact]
        public void add_multiple()
        {
            new ConstantMathExpression(1).Add(3).Add(2).ToString().Should().Be("6");
        }

        [Fact]
        public void add_one_then_divide_by_three()
        {
            new ConstantMathExpression(1).DivideBy(3).ToString().Should().Be("(1 / 3)");
        }

        [Fact]
        public void add_one_then_divide_multiple()
        {
            new ConstantMathExpression(1).DivideBy(3).DivideBy(3).ToString().Should().Be("(1 / 9)");
        }

        [Fact]
        public void multiply_by_four()
        {
            new ConstantMathExpression(1).Multiply(4).ToString().Should().Be("4");
        }

        [Fact]
        public void multiply_by_several_things()
        {
            new ConstantMathExpression(1).Multiply(4).Multiply(5).Multiply(2).ToString().Should().Be("40");
        }

        [Fact]
        public void subtract_three()
        {
            new ConstantMathExpression(0).Subtract(3).ToString().Should().Be("-3");
        }

        [Fact]
        public void add_and_subtract_cancel_out()
        {
            new ConstantMathExpression(5).Subtract(5).ToString().Should().Be("0");
        }

        [Fact]
        public void multiply_and_divide_cancel_out()
        {
            new ConstantMathExpression(1).Multiply(13).DivideBy(13).ToString().Should().Be("1");
        }
    }
}
