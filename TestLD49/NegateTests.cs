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
            new NegateExpression(Prime.Seven).ToString().Should().Be("(-7)");
        }
    }
}
