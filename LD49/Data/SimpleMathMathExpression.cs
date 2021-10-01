namespace LD49.Data
{
    public class SimpleMathMathExpression : MathExpression
    {
        public override MathExpression Add(MathExpression i)
        {
            return this;
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return this;
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return this;
        }

        public override MathExpression Subtract(MathExpression i)
        {
            return this;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
