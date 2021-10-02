namespace LD49.Data
{
    public static class MathOperator
    {
        public static MathExpression Multiply(MathExpression left, MathExpression right)
        {
            return MultiplyMathExpression.Create(left, right);
        }

        public static MathExpression Negate(MathExpression value)
        {
            return NegateExpression.Create(value);
        }

        public static MathExpression Inverse(MathExpression value)
        {
            return InverseExpression.Create(value);
        }

        public static MathExpression Add(MathExpression left, MathExpression right)
        {
            return AddMathExpression.Create(left, right);
        }

        public static MathExpression Subtract(MathExpression left, MathExpression right)
        {
            return MathOperator.Add(left, MathOperator.Negate(right));
        }

        public static MathExpression Divide(MathExpression left, MathExpression right)
        {
            return MultiplyMathExpression.Create(left, MathOperator.Inverse(right));
        }
    }
}
