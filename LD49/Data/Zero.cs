namespace LD49.Data
{
    public class Number : MathExpression
    {
        protected readonly int value;

        protected Number(int value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }

    public abstract class SpecialNumber : Number
    {
        protected SpecialNumber(int value) : base(value)
        {
        }
    }

    public class One : SpecialNumber
    {
        public static readonly MathExpression Instance = new One();

        private One() : base(1)
        {
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return i;
        }
    }

    public class Infinity : SpecialNumber
    {
        public static readonly MathExpression Instance = new Infinity();

        private Infinity() : base(int.MaxValue)
        {
        }
    }

    public class Zero : SpecialNumber
    {
        public static readonly MathExpression Instance = new Zero();

        private Zero() : base(0)
        {
        }

        public override MathExpression Add(MathExpression i)
        {
            return i;
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return this;
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return this;
        }
    }
}
