using System;
using System.Collections.Generic;

namespace LD49.Data
{
    public class Prime : Number
    {
        private static readonly Dictionary<int, Prime> All = new Dictionary<int, Prime>();
        public static Prime Two = new Prime(2);
        public static Prime Three = new Prime(3);
        public static Prime Five = new Prime(5);
        public static Prime Seven = new Prime(7);
        public static Prime Eleven = new Prime(11);
        public static Prime Thirteen = new Prime(13);

        private Prime(int value) : base(value)
        {
            Prime.All.Add(this.value, this);
        }

        public override MathExpression Multiply(MathExpression i)
        {
            if (i is One)
            {
                return this;
            }

            if (i is Zero)
            {
                return Zero.Instance;
            }

            return new MultiplyMathExpression(this, i);
        }

        public static bool IsPrime(int val)
        {
            return Prime.All.ContainsKey(val);
        }

        public override MathExpression Add(MathExpression i)
        {
            if (i is Prime otherPrime)
            {
                var sum = otherPrime.value + this.value;
                if (Prime.IsPrime(sum))
                {
                    return Prime.All[sum];
                }
            }

            return new AddMathExpression(this, i);
        }

        public override MathExpression Subtract(MathExpression i)
        {
            if (i is Prime otherPrime)
            {
                if (i == this)
                {
                    return Zero.Instance;
                }
            }
            
            return new SubtractMathExpression(this, i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            if (i == this)
            {
                return One.Instance;
            }
            
            return new FractionalMathExpression(this, i);
        }
    }
}
