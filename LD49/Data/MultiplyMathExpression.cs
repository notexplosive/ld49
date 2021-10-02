namespace LD49.Data
{
    public class MultiplyMathExpression : MathExpression
    {
        private readonly MathExpression leftFactor;
        private readonly MathExpression rightFactor;

        public MultiplyMathExpression(MathExpression rightFactor, MathExpression leftFactor)
        {
            this.rightFactor = rightFactor;
            this.leftFactor = leftFactor;
        }

        public override MathExpression Multiply(MathExpression i)
        {
            if (i is Prime)
            {
                if (this.leftFactor is Prime)
                {
                    return new MultiplyMathExpression(this.leftFactor.Multiply(i), this.rightFactor);
                }

                if (this.rightFactor is Prime)
                {
                    return new MultiplyMathExpression(this.leftFactor, this.rightFactor.Multiply(i));
                }
            }

            return new MultiplyMathExpression(this, i);
        }

        public override MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            return new SubtractMathExpression(this, i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }
    }
}
