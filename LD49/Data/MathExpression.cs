namespace LD49.Data
{
    public class MathExpression
    {
        private int addend;
        private int denominator = 1;

        public override string ToString()
        {
            var fractionalSegment = "";

            if (this.denominator != 1)
            {
                fractionalSegment = " / " + this.denominator;
            }
            
            return this.addend.ToString() + fractionalSegment;
        }

        public MathExpression Add(int i)
        {
            this.addend += i;
            return this;
        }

        public MathExpression DivideBy(int i)
        {
            this.denominator *= i;
            return this;
        }
    }
}
