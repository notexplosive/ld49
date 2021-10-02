namespace LD49.Data
{
    public class MultiplyMathExpression : TransitiveExpression
    {
        public MultiplyMathExpression(MathExpression rightFactor, MathExpression leftFactor)
            : base('*', leftFactor, rightFactor)
        {
        }

        public override int UnderlyingValue
        {
            get
            {
                var total = 0;
                foreach (var n in this.content)
                {
                    total *= n.UnderlyingValue;
                }

                return total;
            }
        }

        public override MathExpression Multiply(MathExpression i)
        {
            if (i is Prime)
            {
                if (this.content[0] is Prime)
                {
                    return new MultiplyMathExpression(this.content[0].Multiply(i), this.content[1]);
                }

                if (this.content[1] is Prime)
                {
                    return new MultiplyMathExpression(this.content[0], this.content[1].Multiply(i));
                }
            }

            return new MultiplyMathExpression(this, i);
        }
    }
}
