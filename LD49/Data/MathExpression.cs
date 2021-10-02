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
            return new MultiplyMathExpression(this, i);
        }

        public virtual MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public virtual MathExpression Subtract(MathExpression i)
        {
            return new SubtractMathExpression(this, i);
        }

        public virtual MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }

        public static implicit operator MathExpression(int i)
        {
            return new ConstantMathExpression(i);
        }
    }
}
