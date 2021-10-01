namespace LD49.Data
{
    public abstract class MathExpression
    {
        public abstract MathExpression Multiply(MathExpression i);
        public abstract MathExpression Add(MathExpression i);
        public abstract MathExpression Subtract(MathExpression i);
        public abstract MathExpression DivideBy(MathExpression i);
        
        public static implicit operator MathExpression(int i) => new ConstantMathExpression(i);
    }
}
