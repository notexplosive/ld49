using System;
using Machina.Engine;
using Machina.Components;
using Machina.Data;
using Microsoft.Xna.Framework.Graphics;

namespace LD49.Components
{
    public class UIButton : BaseComponent
    {
        private readonly Action callback;
        private readonly Clickable clickable;

        public UIButton(Actor actor, Action callback) : base(actor)
        {
            this.callback = callback;
            new Hoverable(this.actor);
            this.clickable = new Clickable(this.actor);

            this.clickable.OnClick += OnClick;
        }

        private void OnClick(MouseButton button)
        {
            this.callback();
        }

        public override void OnDeleteFinished()
        {
            this.clickable.OnClick -= OnClick;
        }
    }
}
