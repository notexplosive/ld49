using System;

namespace LD49.Data
{
    public abstract class MathExpression : IComparable<MathExpression>
    {
        public abstract int UnderlyingValue { get; }

        public int CompareTo(MathExpression other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
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
            return ReferenceEquals(this, obj) || obj is MathExpression other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(MathExpression left, MathExpression right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MathExpression left, MathExpression right)
        {
            return !Equals(left, right);
        }

        public virtual MathExpression Multiply(MathExpression i)
        {
            if (i is One)
            {
                return this;
            }

            if (i is Zero)
            {
                return Zero.Instance;
            }

            return MultiplyMathExpression.Create(this, i);
        }

        public static MathExpression Negate(MathExpression i)
        {
            return new NegateExpression(i);
        }

        public static MathExpression Inverse(MathExpression i)
        {
            return InverseExpression.Create(i);
        }

        public MathExpression Add(MathExpression i)
        {
            return AddMathExpression.Create(this, i);
        }

        public MathExpression Subtract(MathExpression i)
        {
            if (i is Zero)
            {
                return this;
            }

            return Add(MathExpression.Negate(i));
        }

        public virtual MathExpression DivideBy(MathExpression i)
        {
            return MultiplyMathExpression.Create(this, InverseExpression.Create(i));
        }
    }
}
