using System;

namespace LD49.Data
{
    public abstract class MathExpression
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
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(MathExpression left, MathExpression right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(MathExpression left, MathExpression right)
        {
            return !object.Equals(left, right);
        }

        public abstract MathExpression Multiply(MathExpression i);
        public abstract MathExpression Add(MathExpression i);
        public abstract MathExpression Subtract(MathExpression i);
        public abstract MathExpression DivideBy(MathExpression i);

        public static implicit operator MathExpression(int i)
        {
            return new ConstantMathExpression(i);
        }
    }
}
