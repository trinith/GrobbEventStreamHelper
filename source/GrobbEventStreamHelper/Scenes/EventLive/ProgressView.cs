using ArbitraryPixel.Tenvis.Core;
using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class ProgressView : ViewBase
    {
        public delegate double CurrentProgressFunction();

        public class Settings
        {
            public Rectangle Bounds;
            public Texture2D PixelTexture;
            public CurrentProgressFunction CurrentProgress;

            public Color ForegroundColour = Color.White;
            public Color BackgroundColour = Color.Black;
        }

        private Settings _settings;
        private SpriteBatch _spriteBatch;

        public ProgressView(Settings settings, SpriteBatch spriteBatch)
        {
            _settings = settings;
            _spriteBatch = spriteBatch;
        }

        protected override void OnDraw(GameTime gameTime)
        {
            Rectangle bounds = _settings.Bounds;

            Color backgroundColour = _settings.BackgroundColour;
            Color fillColour = _settings.ForegroundColour;
            int padding = 2;

            _spriteBatch.DrawFilledRectangle(_settings.PixelTexture, bounds, backgroundColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(_settings.PixelTexture, bounds, fillColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(_settings.PixelTexture, bounds, backgroundColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(
                _settings.PixelTexture,
                bounds.Location.ToVector2(),
                new Vector2(
                    (float)(bounds.Width * _settings.CurrentProgress()),
                    bounds.Height
                ),
                fillColour
            );

            // GT_TODO: Draw a label over the bar showing whatever text we get from a get text function we put on Settings.

            base.OnDraw(gameTime);
        }
    }
}
