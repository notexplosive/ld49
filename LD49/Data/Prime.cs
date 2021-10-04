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
        public static readonly Prime ThirtyOne = new Prime(31);
        public static readonly Prime ThirtySeven = new Prime(37);
        public static readonly Prime FortyOne = new Prime(41);
        public static readonly Prime FortyThree = new Prime(43);
        public static readonly Prime FortySeven = new Prime(47);
        public static readonly Prime FiftyThree = new Prime(53);
        public static readonly Prime FiftyNine = new Prime(59);
        public static readonly Prime SixtyOne = new Prime(61);
        public static readonly Prime SixtySeven = new Prime(67);
        public static readonly Prime SeventyOne = new Prime(71);
        public static readonly Prime SeventyThree = new Prime(73);
        public static readonly Prime SeventyNine = new Prime(79);
        public static readonly Prime EightyThree = new Prime(83);
        public static readonly Prime EightyNine = new Prime(89);
        public static readonly Prime NinetySeven = new Prime(97);

        private Prime(int value) : base(value)
        {
            Prime.IndexLookup.Add(this, Prime.All.Count);
            Prime.All.Add(this.value, this);
        }
    }
}
