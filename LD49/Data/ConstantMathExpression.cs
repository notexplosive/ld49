namespace LD49.Data
{
    public class ConstantMathExpression : MathExpression
    {
        private int value;

        public ConstantMathExpression(int i)
        {
            this.value = i;
        }

        public override MathExpression Multiply(MathExpression i)
        {
            if (i is ConstantMathExpression otherConstant)
            {
                this.value *= otherConstant.value;
            }

            return this;
        }

        public override MathExpression Add(MathExpression i)
        {
            if (i is ConstantMathExpression otherConstant)
            {
                this.value += otherConstant.value;
            }

            return this;
        }

        public override MathExpression Subtract(MathExpression i)
        {
            if (i is ConstantMathExpression otherConstant)
            {
                this.value -= otherConstant.value;
            }

            return this;
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            if (i is ConstantMathExpression otherConstant && this.value % otherConstant.value == 0)
            {
                this.value /= otherConstant.value;
                return this;
            }

            return new FractionalMathExpression(new ConstantMathExpression(this.value), i);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
