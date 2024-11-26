using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper
{
    public static partial class GlobalConstants
    {
        public static partial class Colours
        {
            public static class Alliance
            {
                public static Color Dark { get; private set; } = new Color(0, 0, 128);
                public static Color Normal { get; private set; } = new Color(32, 64, 256);
                public static Color Light { get; private set; } = new Color(64, 128, 256);
            }

            public static class Horde
            {
                public static Color Dark { get; private set; } = new Color(128, 0, 0);
                public static Color Normal { get; private set; } = new Color(192, 0, 0);
                public static Color Light { get; private set; } = new Color(256, 128, 128);
            }

            public static class Neutral
            {
                public static Color Dark { get; private set; } = new Color(64, 64, 64);
                public static Color Normal { get; private set; } = new Color(128, 128, 128);
                public static Color Light { get; private set; } = new Color(192, 192, 192);
            }
        }
    }
}
