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

            public RenderSettings(SpriteBatch spriteBatch, Texture2D texture)
            {
                this.SpriteBatch = spriteBatch ?? throw new ArgumentNullException();
                this.Texture = texture ?? throw new ArgumentNullException();
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

            Color backgroundColour = this.Settings.Colour.Background;
            Color fillColour = this.Settings.Colour.Foreground;
            int padding = 4;

            this.Settings.Render.SpriteBatch.DrawFilledRectangle(this.Settings.Render.Texture, bounds, fillColour);
            bounds.Inflate(-padding, -padding);
            this.Settings.Render.SpriteBatch.DrawFilledRectangle(this.Settings.Render.Texture, bounds, backgroundColour);
            bounds.Inflate(-padding, -padding);
            this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                this.Settings.Render.Texture,
                bounds.Location.ToVector2(),
                new Vector2(
                    (float)(bounds.Width * _model.CurrentProgress()),
                    bounds.Height
                ),
                fillColour
            );

            // GT_TODO: Draw a label over the bar showing whatever text we get from a get text function we put on Settings.

            base.OnDraw(gameTime);
        }
    }
}
