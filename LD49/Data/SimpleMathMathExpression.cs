namespace LD49.Data
{
    public class SimpleMathMathExpression : IMathExpression
    {
        private readonly IMathExpression denominator = new ConstantMathExpression(1);
        private int addend;
        private int factor = 1;

        public IMathExpression Add(int i)
        {
            this.addend += i;
            return this;
        }

        public IMathExpression DivideBy(int i)
        {
            this.denominator.Multiply(i);
            return this;
        }

        public IMathExpression Multiply(int i)
        {
            this.factor *= i;
            return this;
        }

        public IMathExpression Subtract(int i)
        {
            this.addend -= i;
            return this;
        }

        public override string ToString()
        {
            var fractionalSegment = "";

            if (this.denominator.ToString() != "1")
            {
                fractionalSegment = " / " + this.denominator;
            }

            return this.addend * this.factor + fractionalSegment;
        }
    }
}
