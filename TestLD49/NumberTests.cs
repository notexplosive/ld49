using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class NumberTests
    {
        [Fact]
        public void divide_into_fraction()
        {
            Prime.Eleven.DivideBy(Prime.Three).ToString().Should().Be("(11 / 3)");
        }

        [Fact]
        public void empty_expression_is_zero()
        {
            Zero.Instance.ToString().Should().Be("0");
        }

        [Fact]
        public void add_one()
        {
            Zero.Instance.Add(One.Instance).ToString().Should().Be("1");
        }

        [Fact]
        public void add_multiple()
        {
            One.Instance.Add(Prime.Three).Add(Prime.Two).ToString().Should().Be("(2 + (1 + 3))");
        }

        [Fact]
        public void add_one_then_divide_by_three()
        {
            One.Instance.DivideBy(Prime.Three).ToString().Should().Be("(1 / 3)");
        }

        [Fact]
        public void add_one_then_divide_multiple()
        {
            One.Instance.DivideBy(Prime.Three).DivideBy(Prime.Three).ToString().Should().Be("(1 / (3 * 3))");
        }

        [Fact]
        public void multiply_by_three()
        {
            One.Instance.Multiply(Prime.Three).ToString().Should().Be("3");
        }

        [Fact]
        public void multiply_by_several_things()
        {
            Prime.Three.Multiply(Prime.Five).Multiply(Prime.Two).ToString().Should().Be("((2 * 3) * 5)");
        }

        [Fact]
        public void add_and_subtract_cancel_out()
        {
            Prime.Five.Subtract(Prime.Five).ToString().Should().Be("0");
        }

        [Fact]
        public void multiply_and_divide_cancel_out()
        {
            Prime.Thirteen.DivideBy(Prime.Thirteen).Should().Be(One.Instance);
        }

        [Fact]
        public void multiply_by_one_does_nothing()
        {
            One.Instance.Multiply(Prime.Thirteen).Should().Be(Prime.Thirteen);
        }
    }
}
