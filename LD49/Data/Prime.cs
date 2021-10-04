using System.Collections.Generic;

namespace LD49.Data
{
    public class Prime : Number
    {
        public static readonly Dictionary<int, Prime> All = new Dictionary<int, Prime>();
        public static readonly Dictionary<Prime, int> IndexLookup = new Dictionary<Prime, int>();

        public static readonly Prime Two = new Prime(2);
        public static readonly Prime Three = new Prime(3);
        public static readonly Prime Five = new Prime(5);
        public static readonly Prime Seven = new Prime(7);
        public static readonly Prime Eleven = new Prime(11);
        public static readonly Prime Thirteen = new Prime(13);
        public static readonly Prime Seventeen = new Prime(17);
        public static readonly Prime Nineteen = new Prime(19);
        public static readonly Prime TwentyThree = new Prime(23);
        public static readonly Prime TwentyNine = new Prime(29);
        public static readonly Prime ThirtyOne = Prime.Create(31);

        public static readonly Prime[] LittlePrimes =
        {
            Prime.Two,
            Prime.Three,
            Prime.Five,
            Prime.Seven,
            Prime.Eleven,
            Prime.Thirteen,
            Prime.Seventeen,
            Prime.Nineteen,
            Prime.TwentyThree,
            Prime.TwentyNine,
            Prime.ThirtyOne
        };

        private Prime(int value) : base(value)
        {
            Prime.IndexLookup.Add(this, Prime.All.Count);
            Prime.All.Add(this.value, this);
        }

        public static Prime Create(int value)
        {
            return new Prime(value);
        }
    }

    public static class BigPrime
    {
        public static readonly Prime ThirtySeven = Prime.Create(37);
        public static readonly Prime FortyOne = Prime.Create(41);
        public static readonly Prime FortyThree = Prime.Create(43);
        public static readonly Prime FortySeven = Prime.Create(47);
        public static readonly Prime FiftyThree = Prime.Create(53);
        public static readonly Prime FiftyNine = Prime.Create(59);
        public static readonly Prime SixtyOne = Prime.Create(61);
        public static readonly Prime SixtySeven = Prime.Create(67);
        public static readonly Prime SeventyOne = Prime.Create(71);
        public static readonly Prime SeventyThree = Prime.Create(73);
        public static readonly Prime SeventyNine = Prime.Create(79);
        public static readonly Prime EightyThree = Prime.Create(83);
        public static readonly Prime EightyNine = Prime.Create(89);
        public static readonly Prime NinetySeven = Prime.Create(97);
    }
}
