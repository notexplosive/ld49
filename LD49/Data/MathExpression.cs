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

        public MathExpression Multiply(MathExpression i)
        {
            return new MultiplyMathExpression(this, i);
        }

        public MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public MathExpression Subtract(MathExpression i)
        {
            return new SubtractMathExpression(this, i);
        }

        public MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }

        public static implicit operator MathExpression(int i)
        {
            return new ConstantMathExpression(i);
        }
    }
}
