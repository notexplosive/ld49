using System;

namespace LD49.Data
{
    public class AddMathExpression : MathExpression
    {
        private readonly MathExpression leftAddend;
        private readonly MathExpression rightAddend;

        public AddMathExpression(MathExpression leftAddend, MathExpression rightAddend)
        {
            this.leftAddend = leftAddend;
            this.rightAddend = rightAddend;
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
