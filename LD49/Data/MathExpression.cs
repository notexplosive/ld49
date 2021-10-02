using System;

namespace LD49.Data
{
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
