using FluentAssertions;
using LD49.Data;
using Microsoft.VisualBasic.CompilerServices;
using Xunit;

namespace TestLD49
{
    public class VariableTests
    {
        [Fact]
        public void named_variable_comes_first_in_expression()
        {
            MathOperator
                .Add(MathOperator.Add(Prime.Eleven, MathOperator.Add(NamedVariable.X, Prime.Seven)),
                    MathOperator.Add(Prime.Thirteen, Prime.Three)).ToString().Should().Be("(X + 3 + 7 + 11 + 13)");
        }

        [Fact]
        public void x_over_x_is_one()
        {
            MathOperator.Divide(NamedVariable.X, NamedVariable.X).Should().Be(One.Instance);
        }
        
        [Fact]
        public void x_times_inverse_x_is_x()
        {
            MathOperator.Multiply(NamedVariable.X, MathOperator.Inverse(NamedVariable.X)).Should().Be(One.Instance);
        }

        [Fact]
        public void x_minus_x_is_zero()
        {
            MathOperator.Subtract(NamedVariable.X, NamedVariable.X).Should().Be(Zero.Instance);
        }
    }
}
