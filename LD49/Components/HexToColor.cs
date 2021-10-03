using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LD49.Components
{
    public static class HexToColor
    {
        /// <summary>
        ///     Borrowed from stackoverflow
        ///     https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string str)
        {
            var hexIndex = new Dictionary<string, byte>();
            for (var i = 0; i <= 255; i++)
            {
                hexIndex.Add(i.ToString("X2"), (byte) i);
            }

            var hexRes = new List<byte>();
            for (var i = 0; i < str.Length; i += 2)
            {
                hexRes.Add(hexIndex[str.Substring(i, 2)]);
            }

            return hexRes.ToArray();
        }

        public static Color Convert(string hex)
        {
            var bytes = HexToColor.StringToByteArray(hex);
            return new Color(bytes[0], bytes[1], bytes[2]);
        }
    }
}
