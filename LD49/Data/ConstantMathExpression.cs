namespace LD49.Data
{
    public class ConstantMathExpression : IMathExpression
    {
        private int value;

        public ConstantMathExpression(int i)
        {
            this.value = i;
        }
        
        public IMathExpression Multiply(int i)
        {
            this.value *= i;
            return this;
        }

        public IMathExpression Add(int i)
        {
            this.value += i;
            return this;
        }

        public IMathExpression Subtract(int i)
        {
            this.value -= i;
            return this;
        }

        public IMathExpression DivideBy(int i)
        {
            this.value /= i;
            return this;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
