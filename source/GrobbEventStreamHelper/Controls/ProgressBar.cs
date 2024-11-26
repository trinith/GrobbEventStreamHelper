using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper.Controls
{
    public class ProgressBar
    {
        public delegate double CurrentProgressFunction();
        public delegate string LabelFunction();

        public Rectangle Bounds;
        public LabelFunction LeftLabel;
        public LabelFunction RightLabel;
        public CurrentProgressFunction CurrentProgress;
    }
}
