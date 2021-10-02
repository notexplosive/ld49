using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class UnaryTests
    {
        [Fact]
        public void negate_to_get_negative()
        {
            NegateExpression.Create(Prime.Seven).ToString().Should().Be("(-7)");
        }
        
        [Fact]
        public void inverse_to_get_one_over()
        {
            InverseExpression.Create(Prime.Seven).ToString().Should().Be("(1 / 7)");
        }

        [Fact]
        public void negate_zero_is_zero()
        {
            NegateExpression.Create(Zero.Instance).Should().Be(Zero.Instance);
        }

        [Fact]
        public void negate_twice()
        {
            MathOperator.Negate(MathOperator.Negate(Prime.Thirteen)).Should().Be(Prime.Thirteen);
        }
        
        [Fact]
        public void inverse_twice()
        {
            MathOperator.Inverse(MathOperator.Inverse(Prime.Thirteen)).Should().Be(Prime.Thirteen);
        }

        [Fact]
        public void inverse_zero_should_be_infinity()
        {
            MathOperator.Inverse(Zero.Instance).Should().Be(Infinity.Instance);
        }

        [Fact]
        public void inverse_one_is_one()
        {
            MathOperator.Inverse(One.Instance).Should().Be(One.Instance);
        }
    }
}
