using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics.Eventing.Reader;

namespace GrobbEventStreamHelper.Controls
{
    public class ButtonController : ControllerBase
    {
        private Button _button;
        private MouseState _oldState;

        Point? _previousClick = null;

        public ButtonController(Button button)
        {
            _button = button ?? throw new ArgumentNullException();
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            MouseState currentState = Mouse.GetState();

            _button.IsHot = _button.Bounds.Contains(currentState.Position);

            if (currentState.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released && _button.IsHot && _previousClick == null)
            {
                // Button clicked for the first time.
                _previousClick = currentState.Position;
                _button.Press();
            }
            else if (currentState.LeftButton == ButtonState.Released && _oldState.LeftButton == ButtonState.Pressed && _previousClick != null)
            {
                // Previous click is released.
                _button.Release();
                _previousClick = null;

                if (_button.IsHot)
                    _button.Click();
            }

            _oldState = currentState;
            
            base.OnUpdate(gameTime);
        }
    }
}
