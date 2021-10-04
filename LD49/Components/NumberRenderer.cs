using System;
using LD49.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class NumberRenderer : BaseComponent
    {
        public static readonly Color[] Colors =
        {
            HexToColor.Convert("287271"),
            HexToColor.Convert("2A9D8F"),
            HexToColor.Convert("E9C46A"),
            HexToColor.Convert("F4A261"),
            HexToColor.Convert("E76F51"),
            HexToColor.Convert("8B687F"),
            HexToColor.Convert("7B435F")
        };

        private readonly Number number;
        private readonly ReckonRenderer reckonRenderer;

        public NumberRenderer(Actor actor, Number number, bool isHoverable) : base(actor)
        {
            this.reckonRenderer = new ReckonRenderer(actor, RequireComponent<BoundingRect>());
            this.number = number;

            if (isHoverable)
            {
                new Hoverable(this.actor);
            }

            new TooltipProvider(this.actor, number.ToString());
        }

        public override void Update(float dt)
        {
            this.reckonRenderer.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.number is SpecialNumber specialNumber)
            {
                var lightGray = new Color(0.9f, 0.9f, 0.9f, 1f);

                if (specialNumber is Zero)
                {
                    this.reckonRenderer.DrawRing(spriteBatch,
                        this.reckonRenderer.Radius / 2f,
                        20,
                        lightGray,
                        0f,
                        ReckonRenderer.RotationLevel.Normal);
                }

                if (specialNumber is Infinity)
                {
                    this.reckonRenderer.DrawRing(spriteBatch, this.reckonRenderer.Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 2f,
                        ReckonRenderer.RotationLevel.Both);
                    this.reckonRenderer.DrawRing(spriteBatch, this.reckonRenderer.Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 1f,
                        ReckonRenderer.RotationLevel.Horizontal);
                    this.reckonRenderer.DrawRing(spriteBatch, this.reckonRenderer.Radius, 20,
                        NumberRenderer.Colors[MachinaGame.Random.Dirty.Next(NumberRenderer.Colors.Length)], 0.5f,
                        ReckonRenderer.RotationLevel.Vertical);
                }

                if (specialNumber is One)
                {
                    var center = this.reckonRenderer.boundingRect.Rect.Center.ToVector2();
                    var offset = new Vector2(0, this.reckonRenderer.Radius / 2f);
                    LineDrawer.DrawLine(spriteBatch, center - offset, center + offset, lightGray, transform.Depth, 10f);

                    this.reckonRenderer.DrawRing(spriteBatch,
                        this.reckonRenderer.Radius / 4f,
                        20,
                        lightGray,
                        0f,
                        ReckonRenderer.RotationLevel.Normal);
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
                    this.reckonRenderer.DrawRing(spriteBatch,
                        this.reckonRenderer.Radius - i * 5,
                        sideCount,
                        color,
                        0.5f * (i + 1) * sign,
                        (ReckonRenderer.RotationLevel) (i % Enum.GetValues(typeof(ReckonRenderer.RotationLevel))
                            .Length));
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
    }
}
