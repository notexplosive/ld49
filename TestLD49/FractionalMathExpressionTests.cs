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
            new FractionalMathExpression(One.Instance, Prime.Three).ToString().Should().Be("(1 / 3)");
        }
        
        [Fact]
        public void multiply_fraction()
        {
            new FractionalMathExpression(One.Instance, Prime.Three).Multiply(Prime.Five).ToString().Should().Be("(5 / 3)");
        }

        [Fact]
        public void multiply_fraction_to_simple()
        {
            new FractionalMathExpression(One.Instance, Prime.Three).Multiply(Prime.Three).ToString().Should().Be("1");
        }
        
        [Fact]
        public void simplify_to_just_numerator()
        {
            new FractionalMathExpression(Prime.Seven, Prime.Three).Multiply(Prime.Three).ToString().Should().Be("7");
        }

        [Fact]
        public void add_to_fraction()
        {
            new FractionalMathExpression(Prime.Seven, Prime.Three).Add(Prime.Thirteen).ToString().Should().Be("((7 / 3) + 13)");
        }
    }
}
