using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static GrobbEventStreamHelper.Controls.ProgressBarView;

namespace GrobbEventStreamHelper.Controls
{
    public class ProgressBarModel
    {
        public Rectangle Bounds;
        public Texture2D PixelTexture;
        public CurrentProgressFunction CurrentProgress;

        public Color ForegroundColour = Color.White;
        public Color BackgroundColour = Color.Black;
    }
}
