using System;
using System.Collections.Generic;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

    public class NumberRenderer : BaseComponent
    {
        private static readonly Color[] Colors =
        {
            HexToColor.Convert("287271"),
            HexToColor.Convert("2A9D8F"),
            HexToColor.Convert("E9C46A"),
            HexToColor.Convert("F4A261"),
            HexToColor.Convert("E76F51"),
            HexToColor.Convert("8B687F"),
            HexToColor.Convert("7B435F")
        };

        private readonly BoundingRect boundingRect;
        private readonly Number number;
        private float totalTime;

        public NumberRenderer(Actor actor, Number number) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.number = number;
        }

        private int ShortSide => Math.Min(this.boundingRect.Rect.Height, this.boundingRect.Rect.Width);
        private int Radius => ShortSide / 2;

        public override void Update(float dt)
        {
            this.totalTime += dt;
        }

        private void DrawRing(SpriteBatch spriteBatch, float radius, int sides, Color color, float animationFactor,
            RotationLevel rotationLevel)
        {
            var time = this.totalTime * animationFactor;
            var fullRadians = MathF.PI * 2;
            var sideIncrement = fullRadians / sides;
            var center = this.boundingRect.Rect.Center.ToVector2();

            for (var i = 0; i < sides; i++)
            {
                var horizontalRadius = rotationLevel == RotationLevel.Horizontal || rotationLevel == RotationLevel.Both
                    ? radius * MathF.Sin(time * 3)
                    : radius;
                var verticalRadius = rotationLevel == RotationLevel.Vertical || rotationLevel == RotationLevel.Both
                    ? radius * MathF.Cos(time * 3)
                    : radius;

                var angle = sideIncrement * i + time;

                var edgePoint1 = center
                                 + new Vector2(
                                     MathF.Cos(angle - sideIncrement) * horizontalRadius,
                                     MathF.Sin(angle - sideIncrement) * verticalRadius);
                var edgePoint2 = center
                                 + new Vector2(
                                     MathF.Cos(angle) * horizontalRadius,
                                     MathF.Sin(angle) * verticalRadius);

                LineDrawer.DrawLine(spriteBatch, edgePoint1, edgePoint2, color, transform.Depth, 10f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.number is SpecialNumber specialNumber)
            {
                var lightGray = new Color(0.9f, 0.9f, 0.9f, 1f);

                if (specialNumber is Zero)
                {
                    DrawRing(spriteBatch,
                        Radius / 2f,
                        20,
                        lightGray,
                        0f,
                        RotationLevel.Normal);
                }

                if (specialNumber is Infinity)
                {
                    DrawRing(spriteBatch, Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 2f,
                        RotationLevel.Both);
                    DrawRing(spriteBatch, Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 1f,
                        RotationLevel.Horizontal);
                    DrawRing(spriteBatch, Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 0.5f,
                        RotationLevel.Vertical);
                }

                if (specialNumber is One)
                {
                    var center = this.boundingRect.Rect.Center.ToVector2();
                    var offset = new Vector2(0, Radius / 2f);
                    LineDrawer.DrawLine(spriteBatch, center - offset, center + offset, lightGray, transform.Depth, 10f);

                    DrawRing(spriteBatch,
                        Radius / 4f,
                        20,
                        lightGray,
                        0f,
                        RotationLevel.Normal);
                }
            }

            if (this.number is Prime prime)
            {
                var primeIndex = GetPrimeIndex(prime);

                var pageSize = 5;
                var color = GetColor(primeIndex);
                var sideCount = primeIndex % pageSize + 3;
                var pageIndex = primeIndex / pageSize + 1;

                var sign = primeIndex % 2 == 0 ? 1 : -1;

                for (var i = 0; i < pageIndex; i++)
                {
                    DrawRing(spriteBatch,
                        Radius - i * 5,
                        sideCount,
                        color,
                        0.5f * (i + 1) * sign,
                        (RotationLevel) (i % Enum.GetValues(typeof(RotationLevel)).Length));
                }
            }
        }

        public Color GetColor(int index)
        {
            return NumberRenderer.Colors[index % NumberRenderer.Colors.Length];
        }

        private int GetPrimeIndex(Prime prime)
        {
            var index = 0;
            foreach (var currentPrime in Prime.All.Values)
            {
                if (currentPrime == prime)
                {
                    return index;
                }

                index++;
            }

            MachinaGame.Print(prime, "is not a prime?");
            return 0;
        }

        private enum RotationLevel
        {
            Normal,
            Horizontal,
            Vertical,
            Both
        }
    }
}
