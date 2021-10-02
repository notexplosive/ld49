using System;

namespace LD49.Data
{
    public static class MathOperator
    {
        public static MathExpression Multiply(MathExpression left, MathExpression right)
        {
            return MultiplyMathExpression.Create(left, right);
        }

        public static MathExpression Negate(MathExpression value)
        {
            return new NegateExpression(value);
        }

        public static MathExpression Inverse(MathExpression value)
        {
            return InverseExpression.Create(value);
        }

        public static MathExpression Add(MathExpression left, MathExpression right)
        {
            return AddMathExpression.Create(left, right);
        }

        public static MathExpression Subtract(MathExpression left, MathExpression right)
        {
            return MathOperator.Add(left, MathOperator.Negate(right));
        }

        public static MathExpression Divide(MathExpression left, MathExpression right)
        {
            return MultiplyMathExpression.Create(left, MathOperator.Inverse(right));
        }
    }

    public abstract class MathExpression : IComparable<MathExpression>
    {
        public abstract int UnderlyingValue { get; }

        public int CompareTo(MathExpression other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (object.ReferenceEquals(null, other))
            {
                return 1;
            }

            return UnderlyingValue.CompareTo(other.UnderlyingValue);
        }

        protected bool Equals(MathExpression other)
        {
            return ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(this, obj) || obj is MathExpression other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(MathExpression left, MathExpression right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(MathExpression left, MathExpression right)
        {
            return !object.Equals(left, right);
        }
    }
}
