namespace LD49.Data
{
    public class Number : MathExpression
    {
        protected readonly int value;

        protected Number(int value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public override int UnderlyingValue => this.value;
    }
}
