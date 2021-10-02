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
            new FractionalMathExpression(1, 3).ToString().Should().Be("1 / 3");
        }
        
        [Fact]
        public void multiply_fraction()
        {
            new FractionalMathExpression(1, 3).Multiply(5).ToString().Should().Be("5 / 3");
        }

        [Fact]
        public void multiply_fraction_to_simple()
        {
            new FractionalMathExpression(1, 3).Multiply(3).ToString().Should().Be("1");
        }
        
        [Fact]
        public void simplify_to_just_numerator()
        {
            new FractionalMathExpression(7, 3).Multiply(3).ToString().Should().Be("7");
        }

        [Fact]
        public void add_to_fraction()
        {
            new FractionalMathExpression(7, 3).Add(10).ToString().Should().Be("7 / 3 + 10");
        }
    }
}
