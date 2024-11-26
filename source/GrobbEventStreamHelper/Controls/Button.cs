using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GrobbEventStreamHelper.Controls
{
    public class Button
    {
        public Rectangle Bounds;
        public string Text;
        public bool IsHot { get; set; } = false;
        public ButtonState State = ButtonState.Released;
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public object Tag = null;

        public event EventHandler<EventArgs> Clicked;
        public event EventHandler<EventArgs> Pressed;
        public event EventHandler<EventArgs> Released;

        public void Press()
        {
            this.State = ButtonState.Pressed;
            if (this.Pressed != null)
                this.Pressed(this, new EventArgs());
        }

        public void Release()
        {
            this.State = ButtonState.Released;
            if (this.Released != null)
                this.Released(this, new EventArgs());
        }

        public void Click()
        {
            if (this.Clicked != null)
                this.Clicked(this, new EventArgs());
        }
    }
}
