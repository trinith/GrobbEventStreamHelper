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

            double progress = (_model.CurrentProgress != null)
                ? MathHelper.Clamp((float)_model.CurrentProgress(), 0, 1)
                : 1.0;

            // Background border.            
            spriteBatch.DrawFilledRectangle(pixel, bounds, bgColour);

            // Border fill.
            bounds.Inflate(-padding/2, -padding/2);
            spriteBatch.DrawFilledRectangle(pixel, bounds, fgColour);

            // Interior fill.
            bounds.Inflate(-padding, -padding);
            spriteBatch.DrawFilledRectangle(pixel, bounds, bgColour);
            
            // Progress fill.
            bounds.Inflate(-padding, -padding);
            spriteBatch.DrawFilledRectangle(
                pixel,
                bounds.Location.ToVector2(),
                new Vector2(
                    (float)(bounds.Width * progress),
                    bounds.Height
                ),
                fgColour
            );

            // Left label.
            string leftLabel = (_model.LeftLabel != null) ? _model.LeftLabel() : string.Empty;
            if (!string.IsNullOrEmpty(leftLabel))
            {
                SpriteFont font = this.Settings.Render.Font;
                int textPadding = 4;
                Point textSize = font.MeasureString(leftLabel).ToPoint();
                Rectangle textBounds = new Rectangle(
                    bounds.Left + textPadding,
                    bounds.Top + bounds.Height / 2 - textSize.Y / 2,
                    textSize.X + textPadding,
                    textSize.Y + textPadding - textPadding
                );

                spriteBatch.DrawFilledRectangle(pixel, textBounds, bgColour);
                spriteBatch.DrawString(font, leftLabel, textBounds.Location.ToVector2() + new Vector2(textPadding / 2f, textPadding / 2f), fgColour);
            }

            // Right label.
            string rightLabel = (_model.RightLabel != null) ? _model.RightLabel() : string.Empty;
            if (!string.IsNullOrEmpty(rightLabel))
            {
                SpriteFont font = this.Settings.Render.Font;
                int textPadding = 4;
                Point textSize = font.MeasureString(rightLabel).ToPoint();
                Rectangle textBounds = new Rectangle(
                    bounds.Right - textPadding * 2 - textSize.X,
                    bounds.Top + bounds.Height / 2 - textSize.Y / 2,
                    textSize.X + textPadding,
                    textSize.Y + textPadding - textPadding
                );

                spriteBatch.DrawFilledRectangle(pixel, textBounds, bgColour);
                spriteBatch.DrawString(font, rightLabel, textBounds.Location.ToVector2() + new Vector2(textPadding / 2f, textPadding / 2f), fgColour);
            }

            // GT_TODO: Consider adding a supplementary label on the right side, to show duration or control time?

            base.OnDraw(gameTime);
        }
    }
}
