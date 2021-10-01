using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class ExpressionTests
    {
        [Fact]
        public void empty_expression_is_zero()
        {
            new MathExpression().ToString().Should().Be("0");
        }
        
        [Fact]
        public void add_one()
        {
            new MathExpression().Add(1).ToString().Should().Be("1");
        }

        [Fact]
        public void add_one_then_divide_by_three()
        {
            new MathExpression().Add(1).DivideBy(3).ToString().Should().Be("1 / 3");
        }
    }
}
