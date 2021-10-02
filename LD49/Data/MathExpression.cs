using System;

namespace LD49.Data
{
    public abstract class MathExpression : IComparable<MathExpression>
    {
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

        public virtual MathExpression Multiply(MathExpression i)
        {
            if (i is One)
            {
                return this;
            }

            return new MultiplyMathExpression(this, i);
        }

        public virtual MathExpression Add(MathExpression i)
        {
            if (i is Zero)
            {
                return this;
            }

            return new AddMathExpression(this, i);
        }

        public virtual MathExpression Subtract(MathExpression i)
        {
            if (i is Zero)
            {
                return this;
            }

            return new SubtractMathExpression(this, i);
        }

        public virtual MathExpression DivideBy(MathExpression i)
        {
            if (i is One)
            {
                return this;
            }

            if (i is Zero)
            {
                return Infinity.Instance;
            }

            return new FractionalMathExpression(this, i);
        }
        
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

            return this.UnderlyingValue.CompareTo(other.UnderlyingValue);
        }
        
        public abstract int UnderlyingValue { get; }
    }
}
