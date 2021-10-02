namespace LD49.Data
{
    public class SubtractMathExpression : PairExpression
    {
        public SubtractMathExpression(MathExpression minuend, MathExpression subtrahend)
            : base(minuend, subtrahend, '-', false)
        {
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return new MultiplyMathExpression(this.left.Multiply(i), this.left.Multiply(i));
        }
    }
}
