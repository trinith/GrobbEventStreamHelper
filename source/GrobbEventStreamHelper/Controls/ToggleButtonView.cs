using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper.Controls
{
    public class ToggleButtonView : ButtonView
    {
        public ToggleButtonView(ToggleButton button, RenderSettings renderSettings, ColourSettings colourSettings)
            : base(button, renderSettings, colourSettings)
        {
        }

        protected override void DrawButton(Button button, Rectangle bounds)
        {
            ToggleButton toggleButton = (ToggleButton)button;

            if (toggleButton.IsActive)
            {
                Color activteIconColour = this.Settings.Colour.Normal;

                this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                    this.Settings.Render.Texture,
                    bounds,
                    activteIconColour
                );

                int maskSize = MathHelper.Min(bounds.Width, bounds.Height) / 4;

                this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                    this.Settings.Render.Texture,
                    new Rectangle(
                        bounds.Left,
                        bounds.Top + maskSize,
                        bounds.Width,
                        bounds.Height - maskSize * 2
                    ),
                    Color.Black
                );

                this.Settings.Render.SpriteBatch.DrawFilledRectangle(
                    this.Settings.Render.Texture,
                    new Rectangle(
                        bounds.Left + maskSize,
                        bounds.Top,
                        bounds.Width - maskSize * 2,
                        bounds.Height
                    ),
                    Color.Black
                );
            }
            bounds.Inflate(-4, -4);

            base.DrawButton(button, bounds);
        }
    }
}
