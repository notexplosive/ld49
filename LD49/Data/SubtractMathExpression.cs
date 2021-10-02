using System;

namespace LD49.Data
{
    public class SubtractMathExpression : MathExpression
    {
        private readonly MathExpression minuend;
        private readonly MathExpression subtrahend;

        public SubtractMathExpression(MathExpression minuend, MathExpression subtrahend)
        {
            this.minuend = minuend;
            this.subtrahend = subtrahend;
        }
        
        public override MathExpression Multiply(MathExpression i)
        {
            return new MultiplyMathExpression(this.minuend.Multiply(i), this.subtrahend.Multiply(i));
        }

        public override MathExpression Add(MathExpression i)
        {
            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            return new SubtractMathExpression(this, i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }
    }
}
