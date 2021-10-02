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

        public override int UnderlyingValue => this.value;
    }

    public abstract class SpecialNumber : Number
    {
        protected SpecialNumber(int value) : base(value)
        {
        }
    }

    public class One : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new One();

        private One() : base(1)
        {
        }
    }

    public class Infinity : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new Infinity();

        private Infinity() : base(int.MaxValue)
        {
        }
    }

    public class Zero : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new Zero();

        private Zero() : base(0)
        {
        }
    }
}
