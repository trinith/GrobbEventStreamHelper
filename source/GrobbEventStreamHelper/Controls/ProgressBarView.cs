using ArbitraryPixel.Tenvis.Core;
using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Controls
{

    public class ProgressBarView : ViewBase
    {
        public delegate double CurrentProgressFunction();

        private ProgressBarModel _model;
        private SpriteBatch _spriteBatch;

        public ProgressBarView(ProgressBarModel model, SpriteBatch spriteBatch)
        {
            _model = model;
            _spriteBatch = spriteBatch;
        }

        protected override void OnDraw(GameTime gameTime)
        {
            Rectangle bounds = _model.Bounds;

            Color backgroundColour = _model.BackgroundColour;
            Color fillColour = _model.ForegroundColour;
            int padding = 2;

            _spriteBatch.DrawFilledRectangle(_model.PixelTexture, bounds, backgroundColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(_model.PixelTexture, bounds, fillColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(_model.PixelTexture, bounds, backgroundColour);
            bounds.Inflate(-padding, -padding);
            _spriteBatch.DrawFilledRectangle(
                _model.PixelTexture,
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
