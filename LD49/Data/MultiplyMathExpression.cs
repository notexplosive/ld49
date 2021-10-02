namespace LD49.Data
{
    public class MultiplyMathExpression : PairExpression
    {
        public MultiplyMathExpression(MathExpression rightFactor, MathExpression leftFactor)
            : base(leftFactor,rightFactor, '*', true)
        {
        }

        public override MathExpression Multiply(MathExpression i)
        {
            if (i is Prime)
            {
                if (this.left is Prime)
                {
                    return new MultiplyMathExpression(this.left.Multiply(i), this.right);
                }

                if (this.right is Prime)
                {
                    return new MultiplyMathExpression(this.left, this.right.Multiply(i));
                }
            }

            return new MultiplyMathExpression(this, i);
        }
    }
}
