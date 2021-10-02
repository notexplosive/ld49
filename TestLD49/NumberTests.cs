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
            One.Instance.Add(Prime.Three).Add(Prime.Two).ToString().Should().Be("(1 + 2 + 3)");
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
            Prime.Three.Multiply(Prime.Five).Multiply(Prime.Two).ToString().Should().Be("(2 * 3 * 5)");
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

        [Fact]
        public void sum_two_primes_to_expression_not_prime()
        {
            Prime.Two.Add(Prime.Three).ToString().Should().Be("(2 + 3)");
        }

        [Fact]
        public void sum_three_primes_to_expression_not_prime()
        {
            Prime.Three.Add(Prime.Seven).Add(Prime.Three).ToString().Should().Be("(3 + 3 + 7)");
        }
        
        [Fact]
        public void sum_three_primes_to_expression()
        {
            Prime.Three.Add(Prime.Three).Add(Prime.Three).ToString().Should().Be("(3 + 3 + 3)");
        }

        [Fact]
        public void product_three_numbers_to_express()
        {
            Prime.Five.Multiply(Prime.Two).Multiply(Prime.Three).ToString().Should().Be("(2 * 3 * 5)");
        }

        [Fact]
        public void get_inverse()
        {
            MathExpression.Inverse(Prime.Five).ToString().Should().Be("(1 / 5)");
        }

        [Fact]
        public void prime_times_inverse_is_one()
        {
            Prime.Five.Multiply(MathExpression.Inverse(Prime.Five)).Should().Be(One.Instance);
        }
        
        [Fact]
        public void prime_plus_negate_is_zero()
        {
            Prime.Five.Add(MathExpression.Negate(Prime.Five)).Should().Be(Zero.Instance);
        }

        [Fact]
        public void expression_times_one_is_expression()
        {
            Prime.Five.DivideBy(Prime.Seven).Multiply(One.Instance).ToString().Should().Be("(5 / 7)");
        }

        [Fact]
        public void complex_expression_with_inverse_cancels()
        {
            Prime.Two.Multiply(Prime.Three).Multiply(Prime.Thirteen).Multiply(MathExpression.Inverse(Prime.Two)).ToString().Should().Be("(3 * 13)");
        }
    }
}
