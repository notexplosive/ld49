using System;

namespace LD49.Data
{
    public class AddMathExpression : TransitiveExpression
    {
        public AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
            : base(leftAddend, '+', rightAddend)
        {
        }

        public override MathExpression Multiply(MathExpression i)
        {
            // X * (A + B) -> AX + BX
            return new AddMathExpression(this.content[0].Multiply(i), this.content[1].Multiply(i));
        }

        public override MathExpression Add(MathExpression i)
        {
            // X + (A + B)
            return new AddMathExpression(this, i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            // (A + B) / X -> (A/X + B/X)
            return new AddMathExpression(
                new FractionalMathExpression(this.content[0], i),
                new FractionalMathExpression(this.content[1], i));
        }

        public override int UnderlyingValue
        {
            get
            {
                var total = 0;
                foreach (var n in this.content)
                {
                    total += n.UnderlyingValue;
                }

                return total;
            }
        }
    }
}
