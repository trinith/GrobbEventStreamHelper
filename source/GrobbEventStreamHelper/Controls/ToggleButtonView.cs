using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

namespace GrobbEventStreamHelper.Controls
{
    public class ToggleButtonView : ButtonView
    {
        public ToggleButtonView(ToggleButton button, RenderSettings renderSettings, ColourSettings colourSettings)
            : base(button, renderSettings, colourSettings)
        {
        }

        protected override void DrawButton(Button button, Rectangle bounds, Color activeColour)
        {
            ToggleButton toggleButton = (ToggleButton)button;

            int padding = 2;

            if (toggleButton.IsActive)
            {
                SpriteBatch spriteBatch = this.Settings.Render.SpriteBatch;
                Texture2D texture = this.Settings.Render.Texture;

                int indicatorSize = MathHelper.Min(bounds.Width, bounds.Height) / 4;
                Rectangle[] indicatorRects =
                {
                    new Rectangle(bounds.Left, bounds.Top, indicatorSize, indicatorSize),
                    new Rectangle(bounds.Left, bounds.Bottom - indicatorSize, indicatorSize, indicatorSize),
                    new Rectangle(bounds.Right - indicatorSize, bounds.Top, indicatorSize, indicatorSize),
                    new Rectangle(bounds.Right - indicatorSize, bounds.Bottom - indicatorSize, indicatorSize, indicatorSize),
                };

                foreach (Rectangle indicatorRect in indicatorRects)
                {
                    spriteBatch.Draw(texture, indicatorRect, Color.Black);
                    indicatorRect.Inflate(-padding, -padding);
                    spriteBatch.Draw(texture, indicatorRect, activeColour);
                }
            }

            padding *= 2;
            bounds.Inflate(-padding, -padding);

            base.DrawButton(button, bounds, activeColour);
        }
    }
}
