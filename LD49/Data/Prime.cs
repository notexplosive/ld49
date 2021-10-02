using System;
using System.Collections.Generic;

namespace LD49.Data
{
    public class Prime : MathExpression, IComparable<Prime>
    {
        private static readonly Dictionary<int, Prime> All = new Dictionary<int, Prime>();
        public static Prime Zero = new Prime(0); // Zero is a prime, FIGHT ME
        public static Prime One = new Prime(1); // One is also a prime, FIGHT ME AGAIN
        public static Prime Two = new Prime(2);
        public static Prime Three = new Prime(3);
        public static Prime Five = new Prime(5);
        public static Prime Seven = new Prime(7);
        public static Prime Eleven = new Prime(11);
        public static Prime Thirteen = new Prime(13);
        private readonly int value;

        private Prime(int i)
        {
            this.value = i;
            Prime.All.Add(this.value, this);
        }

        public int CompareTo(Prime other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (object.ReferenceEquals(null, other))
            {
                return 1;
            }

            return this.value.CompareTo(other.value);
        }

        public override MathExpression Multiply(MathExpression i)
        {
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
            return new SubtractMathExpression(this, i);
        }

        public override MathExpression DivideBy(MathExpression i)
        {
            return new FractionalMathExpression(this, i);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
