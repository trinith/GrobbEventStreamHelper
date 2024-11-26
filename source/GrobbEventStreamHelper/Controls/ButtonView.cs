using ArbitraryPixel.Tenvis.Core;
using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static GrobbEventStreamHelper.Controls.ButtonView;

namespace GrobbEventStreamHelper.Controls
{
    public class ButtonView : ViewBase
    {
        public class RenderSettings
        {
            public SpriteBatch SpriteBatch;
            public Texture2D Texture;
            public SpriteFont Font;

            public RenderSettings(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font)
            {
                this.SpriteBatch = spriteBatch ?? throw new ArgumentNullException();
                this.Texture = texture ?? throw new ArgumentNullException();
                this.Font = font ?? throw new ArgumentNullException();
            }
        }

        public class ColourSettings
        {
            public Color Normal = Color.White;
            public Color Pressed = Color.White;
            public Color Hot = Color.White;
        }

        protected class ViewSettings
        {
            public RenderSettings Render { get; private set; }
            public ColourSettings Colour { get; private set; }

            public ViewSettings(RenderSettings renderSettings, ColourSettings colourSettings)
            {
                this.Render = renderSettings ?? throw new ArgumentNullException();
                this.Colour = colourSettings ?? throw new ArgumentNullException();
            }
        }

        private Button _button;

        protected ViewSettings Settings { get; private set; }

        public ButtonView(Button button, RenderSettings renderSettings, ColourSettings colourSettings)
        {
            _button = button ?? throw new ArgumentNullException();
            this.Settings = new ViewSettings(renderSettings, colourSettings);
        }

        protected override void OnDraw(GameTime gameTime)
        {
            Color activeColour = (_button.IsHot)
                ? (_button.State == ButtonState.Pressed)
                    ? this.Settings.Colour.Pressed
                    : this.Settings.Colour.Hot
                : this.Settings.Colour.Normal;

            DrawButton(_button, _button.Bounds, activeColour);

            base.OnDraw(gameTime);
        }

        protected virtual void DrawButton(Button button, Rectangle bounds, Color activeColour)
        {
            this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                this.Settings.Render.Texture,
                bounds,
                Color.Black
            );

            bounds.Inflate(-2, -2);

            this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                this.Settings.Render.Texture,
                bounds,
                activeColour
            );

            bounds.Inflate(-4, -4);

            this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                this.Settings.Render.Texture,
                bounds,
                Color.Black
            );

            if (!string.IsNullOrEmpty(_button.Text))
            {
                SpriteFont font = this.Settings.Render.Font;
                Point textSize = font.MeasureString(_button.Text).ToPoint();
                Point textPos = new Point(
                    bounds.Center.X - textSize.X / 2,
                    bounds.Center.Y - textSize.Y / 2
                );
                this.Settings.Render.SpriteBatch.DrawString(font, _button.Text, textPos.ToVector2(), activeColour);
            }
        }
    }
}
