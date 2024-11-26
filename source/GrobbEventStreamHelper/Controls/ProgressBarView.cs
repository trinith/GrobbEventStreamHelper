using ArbitraryPixel.Tenvis.Core;
using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Controls
{

    public class ProgressBarView : ViewBase
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
            public Color Background = Color.White;
            public Color Foreground = Color.White;
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

        private ProgressBar _model;
        protected ViewSettings Settings { get; private set; }

        public ProgressBarView(ProgressBar model, RenderSettings renderSettings, ColourSettings colourSettings)
        {
            _model = model ?? throw new ArgumentNullException();
            this.Settings = new ViewSettings(renderSettings, colourSettings);
        }

        protected override void OnDraw(GameTime gameTime)
        {
            Rectangle bounds = _model.Bounds;

            Color bgColour = this.Settings.Colour.Background;
            Color fgColour = this.Settings.Colour.Foreground;
            SpriteBatch spriteBatch = this.Settings.Render.SpriteBatch;
            Texture2D pixel = this.Settings.Render.Texture;

            int padding = 4;

            spriteBatch.DrawFilledRectangle(pixel, bounds, fgColour);
            bounds.Inflate(-padding, -padding);
            spriteBatch.DrawFilledRectangle(pixel, bounds, bgColour);
            bounds.Inflate(-padding, -padding);
            spriteBatch.DrawFilledRectangle(
                pixel,
                bounds.Location.ToVector2(),
                new Vector2(
                    (float)(bounds.Width * _model.CurrentProgress()),
                    bounds.Height
                ),
                fgColour
            );

            if (!string.IsNullOrEmpty(_model.Text))
            {
                SpriteFont font = this.Settings.Render.Font;
                int textPadding = 4;
                Point textSize = font.MeasureString(_model.Text).ToPoint();
                Rectangle textBounds = new Rectangle(
                    bounds.Left + 8,
                    bounds.Top + bounds.Height / 2 - textSize.Y / 2,
                    textSize.X + textPadding,
                    textSize.Y + textPadding - textPadding
                );

                spriteBatch.DrawFilledRectangle(pixel, textBounds, bgColour);
                spriteBatch.DrawString(font, _model.Text, textBounds.Location.ToVector2() + new Vector2(textPadding / 2f, textPadding / 2f), fgColour);
            }

            // GT_TODO: Consider adding a supplementary label on the right side, to show duration or control time?

            base.OnDraw(gameTime);
        }
    }
}
