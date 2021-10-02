using FluentAssertions;
using LD49.Data;
using Xunit;

namespace TestLD49
{
    public class PrimeTests
    {
        [Fact]
        public void add_two_primes_becomes_expression()
        {
            Prime.Eleven.Add(Prime.Five).Should().Be(new AddMathExpression(Prime.Five,Prime.Eleven));
        }

        [Fact]
        public void add_two_primes_becomes_prime()
        {
            Prime.Eleven.Add(Prime.Two).Should().Be(Prime.Thirteen);
        }
    }
}
