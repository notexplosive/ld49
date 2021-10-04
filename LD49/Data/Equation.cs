namespace LD49.Data
{
    public class Equation
    {
        public readonly MathExpression left;
        public readonly MathExpression right;
        public readonly bool isJustExpression;

        public Equation(MathExpression left, MathExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Equation(MathExpression only) : this(only, only)
        {
            this.isJustExpression = true;
        }
    }
}
