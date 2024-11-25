using Microsoft.Xna.Framework;
using System;

namespace GrobbEventStreamHelper.Controls
{
    public class Button
    {
        public Rectangle Bounds;
        public string Text;
        public ButtonState State = ButtonState.UnPressed;
        public object Tag = null;

        public event EventHandler<EventArgs> OnClicked;
    }
}
