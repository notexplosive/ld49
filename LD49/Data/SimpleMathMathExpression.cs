using System.Reflection.Metadata;

namespace LD49.Data
{
    public class SimpleMathMathExpression : IMathExpression
    {
        private int addend;
        private IMathExpression denominator = new ConstantMathExpression(1);
        private int factor = 1;

        public override string ToString()
        {
            var fractionalSegment = "";

            if (this.denominator != 1)
            {
                fractionalSegment = " / " + this.denominator;
            }

            return this.addend * factor + fractionalSegment;
        }

        public SimpleMathMathExpression Add(int i)
        {
            this.addend += i;
            return this;
        }

        public SimpleMathMathExpression DivideBy(int i)
        {
            this.denominator *= i;
            return this;
        }

        public SimpleMathMathExpression Multiply(int i)
        {
            this.factor *= i;
            return this;
        }

        public SimpleMathMathExpression Subtract(int i)
        {
            this.addend -= i;
            return this;
        }
    }
}
