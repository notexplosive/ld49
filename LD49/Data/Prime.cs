﻿using System.Collections.Generic;

namespace LD49.Data
{
    public class Prime : Number
    {
        private static readonly Dictionary<int, Prime> All = new Dictionary<int, Prime>();
        public static readonly Prime Two = new Prime(2);
        public static readonly Prime Three = new Prime(3);
        public static readonly Prime Five = new Prime(5);
        public static readonly Prime Seven = new Prime(7);
        public static readonly Prime Eleven = new Prime(11);
        public static readonly Prime Thirteen = new Prime(13);

        private Prime(int value) : base(value)
        {
            Prime.All.Add(this.value, this);
        }
    }
}
