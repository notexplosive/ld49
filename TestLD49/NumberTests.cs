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
            MathOperator.Divide(Prime.Eleven, Prime.Three).ToString().Should().Be("((1 / 3) * 11)");
        }

        [Fact]
        public void empty_expression_is_zero()
        {
            Zero.Instance.ToString().Should().Be("0");
        }

        [Fact]
        public void add_one()
        {
            MathOperator.Add(Zero.Instance, One.Instance).ToString().Should().Be("1");
        }

        [Fact]
        public void add_multiple()
        {
            MathOperator.Add(MathOperator.Add(One.Instance, Prime.Three), Prime.Two).ToString().Should()
                .Be("(1 + 2 + 3)");
        }

        [Fact]
        public void add_one_then_divide_by_three()
        {
            MathOperator.Divide(One.Instance, Prime.Three).ToString().Should().Be("(1 / 3)");
        }

        [Fact]
        public void divide_multiple()
        {
            MathOperator.Divide(MathOperator.Divide(One.Instance, Prime.Three), Prime.Three).ToString().Should()
                .Be("((1 / 3) * (1 / 3))");
        }

        [Fact]
        public void multiply_by_three()
        {
            MathOperator.Multiply(One.Instance, Prime.Three).ToString().Should().Be("3");
        }

        [Fact]
        public void multiply_by_several_things()
        {
            MathOperator.Multiply(MathOperator.Multiply(Prime.Three, Prime.Five), Prime.Two).ToString().Should()
                .Be("(2 * 3 * 5)");
        }

        [Fact]
        public void add_and_subtract_cancel_out()
        {
            MathOperator.Subtract(Prime.Five, Prime.Five).ToString().Should().Be("0");
        }

        [Fact]
        public void multiply_and_divide_cancel_out()
        {
            MathOperator.Divide(Prime.Thirteen, Prime.Thirteen).Should().Be(One.Instance);
        }

        [Fact]
        public void multiply_by_one_does_nothing()
        {
            MathOperator.Multiply(One.Instance, Prime.Thirteen).Should().Be(Prime.Thirteen);
        }

        [Fact]
        public void sum_two_primes_to_expression_not_prime()
        {
            MathOperator.Add(Prime.Two, Prime.Three).ToString().Should().Be("(2 + 3)");
        }

        [Fact]
        public void sum_three_primes_to_expression_not_prime()
        {
            MathOperator.Add(MathOperator.Add(Prime.Three, Prime.Seven), Prime.Three).ToString().Should()
                .Be("(3 + 3 + 7)");
        }

        [Fact]
        public void sum_three_primes_to_expression()
        {
            MathOperator.Add(MathOperator.Add(Prime.Three, Prime.Three), Prime.Three).ToString().Should()
                .Be("(3 + 3 + 3)");
        }

        [Fact]
        public void product_three_numbers_to_express()
        {
            MathOperator.Multiply(MathOperator.Multiply(Prime.Five, Prime.Two), Prime.Three).ToString().Should()
                .Be("(2 * 3 * 5)");
        }

        [Fact]
        public void get_inverse()
        {
            MathOperator.Inverse(Prime.Five).ToString().Should().Be("(1 / 5)");
        }

        [Fact]
        public void prime_times_inverse_is_one()
        {
            MathOperator.Multiply(Prime.Five, MathOperator.Inverse(Prime.Five)).Should().Be(One.Instance);
        }

        [Fact]
        public void prime_plus_negate_is_zero()
        {
            MathOperator.Add(Prime.Five, MathOperator.Negate(Prime.Five)).Should().Be(Zero.Instance);
        }

        [Fact]
        public void expression_times_one_is_expression()
        {
            MathOperator.Multiply(MathOperator.Divide(Prime.Five, Prime.Seven), One.Instance).ToString().Should()
                .Be("(5 * (1 / 7))");
        }

        [Fact]
        public void complex_multiply_expression_with_inverse_cancels()
        {
            MathOperator.Multiply(
                MathOperator.Multiply(
                    Prime.Thirteen,
                    MathOperator.Multiply(Prime.Two, Prime.Three)),
                MathOperator.Inverse(Prime.Two)).ToString().Should().Be("(3 * 13)");
        }

        [Fact]
        public void simple_multiply_expression_with_inverse_cancels()
        {
            MathOperator.Multiply(MathOperator.Multiply(Prime.Three, Prime.Seven),
                    InverseExpression.Create(Prime.Three)).Should()
                .Be(Prime.Seven);
        }

        [Fact]
        public void prime_times_one_is_same()
        {
            MathOperator.Multiply(Prime.Seven, One.Instance).Should().Be(Prime.Seven);
        }

        [Fact]
        public void one_times_prime_is_same()
        {
            MathOperator.Multiply(One.Instance, Prime.Seven).Should().Be(Prime.Seven);
        }

        [Fact]
        public void prime_times_zero_is_zero()
        {
            MathOperator.Multiply(Prime.Seven, Zero.Instance).Should().Be(Zero.Instance);
        }

        [Fact]
        public void zero_times_prime_is_zero()
        {
            MathOperator.Multiply(Zero.Instance, Prime.Seven).Should().Be(Zero.Instance);
        }

        [Fact]
        public void one_cancels_with_negative_one()
        {
            MathOperator.Add(One.Instance, MathOperator.Negate(One.Instance)).Should().Be(Zero.Instance);
        }

        [Fact]
        public void negative_one_cancels_with_one()
        {
            MathOperator.Add(MathOperator.Negate(One.Instance), One.Instance).Should().Be(Zero.Instance);
        }

        [Fact]
        public void prime_minus_zero_is_prime()
        {
            MathOperator.Subtract(Prime.Seven, Zero.Instance).Should().Be(Prime.Seven);
        }

        [Fact]
        public void zero_minus_prime_is_negative_prime()
        {
            MathOperator.Subtract(Zero.Instance, Prime.Seven).Should().Be(MathOperator.Negate(Prime.Seven));
        }

        [Fact]
        public void prime_plus_one()
        {
            MathOperator.Add(Prime.Three, One.Instance).ToString().Should().Be("(1 + 3)");
        }

        [Fact]
        public void one_plus_prime()
        {
            MathOperator.Add(One.Instance, Prime.Three).ToString().Should().Be("(1 + 3)");
        }

        [Fact]
        public void three_plus_one_plus_one()
        {
            MathOperator.Add(One.Instance, MathOperator.Add(Prime.Three, One.Instance)).ToString().Should()
                .Be("(1 + 1 + 3)");
        }

        [Fact]
        public void lots_of_cancels_multiply()
        {
            MathOperator.Multiply(
                MathOperator.Multiply(
                    Prime.Three, Prime.Eleven),
                MathOperator.Multiply(
                    MathOperator.Multiply(
                        MathOperator.Inverse(Prime.Five), MathOperator.Inverse(Prime.Three)),
                    MathOperator.Multiply(
                        MathOperator.Multiply(MathOperator.Inverse(Prime.Eleven),
                            MathOperator.Multiply(
                                MathOperator.Multiply(Prime.Two, MathOperator.Inverse(Prime.Two)),
                                MathOperator.Inverse(Prime.Five))),
                        MathOperator.Multiply(Prime.Five, Prime.Five)))).Should().Be(One.Instance);
        }

        [Fact]
        public void lots_of_cancels_add()
        {
            MathOperator.Add(
                MathOperator.Add(
                    Prime.Three, Prime.Eleven),
                MathOperator.Add(
                    MathOperator.Add(
                        MathOperator.Negate(Prime.Five), MathOperator.Negate(Prime.Three)),
                    MathOperator.Add(
                        MathOperator.Add(MathOperator.Negate(Prime.Eleven),
                            MathOperator.Add(
                                MathOperator.Add(Prime.Two, MathOperator.Negate(Prime.Two)),
                                MathOperator.Negate(Prime.Five))),
                        MathOperator.Add(Prime.Five, Prime.Five)))).Should().Be(Zero.Instance);
        }

        [Fact]
        public void expression_over_expression_is_one()
        {
            MathOperator.Divide(
                MathOperator.Add(One.Instance, MathOperator.Add(Prime.Two, Prime.Three)),
                MathOperator.Add(Prime.Three, MathOperator.Add(Prime.Two, One.Instance))).Should().Be(One.Instance);
        }

        [Fact]
        public void multiply_expression_over_factor()
        {
            MathOperator.Divide(MathOperator.Multiply(Prime.Three, Prime.Two), Prime.Three).Should().Be(Prime.Two);
        }

        [Fact]
        public void prime_over_one_is_prime()
        {
            MathOperator.Divide(Prime.Seven, One.Instance).Should().Be(Prime.Seven);
        }

        [Fact]
        public void expressions_cancel_out_add()
        {
            MathOperator.Subtract(MathOperator.Add(Prime.Five, Prime.Three), MathOperator.Add(Prime.Three, Prime.Five))
                .ToString()
                .Should().Be("0");
        }

        [Fact]
        public void expressions_cancel_out_multiply()
        {
            MathOperator
                .Divide(MathOperator.Multiply(Prime.Five, Prime.Three), MathOperator.Multiply(Prime.Three, Prime.Five))
                .ToString().Should().Be("1");
        }
    }
}
