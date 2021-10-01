namespace LD49.Data
{
    public interface IMathExpression
    {
        public IMathExpression Multiply(int i);
        public IMathExpression Add(int i);
        public IMathExpression Subtract(int i);
        public IMathExpression DivideBy(int i);
    }
}
