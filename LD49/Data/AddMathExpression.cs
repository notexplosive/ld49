using System;
using System.Collections.Generic;

namespace LD49.Data
{
    public class AddMathExpression : MathExpression
    {
        private readonly MathExpression leftAddend;
        private readonly MathExpression rightAddend;

        public AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
        {
            if (leftAddend is Prime p1 && rightAddend is Prime p2)
            {
                var list = new List<Prime> {p1, p2};
                list.Sort();
                this.leftAddend = list[0];
                this.rightAddend = list[1];
            }
            else
            {
                this.leftAddend = leftAddend;
                this.rightAddend = rightAddend;
            }
        }

        public override MathExpression Multiply(MathExpression i)
        {
            return new AddMathExpression(this.leftAddend.Multiply(i), this.rightAddend.Multiply(i));
        }

        public override MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            throw new NotImplementedException();
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }

        public override string ToString()
        {
            return $"({this.leftAddend} + {this.rightAddend})";
        }
    }
}
