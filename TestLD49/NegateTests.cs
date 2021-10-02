using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class NegateTests
    {
        [Fact]
        public void negate_to_get_negative()
        {
            NegateExpression.Create(Prime.Seven).ToString().Should().Be("(-7)");
        }

        [Fact]
        public void negate_zero_is_zero()
        {
            NegateExpression.Create(Zero.Instance).Should().Be(Zero.Instance);
        }
    }
}
