namespace LD49.Data
{
    public class ConstantMathExpression : IMathExpression
    {
        private readonly int value;

        public ConstantMathExpression(int i)
        {
            this.value = i;
        }
    }
}
