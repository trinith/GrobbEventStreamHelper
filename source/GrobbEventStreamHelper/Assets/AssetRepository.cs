using ArbitraryPixel.Tenvis.Assets.AssetDescriptors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Assets
{
    public static class AssetRepository
    {
        public static class Textures
        {
            public static CustomAssetDescriptor<Texture2D> Pixel => new CustomAssetDescriptor<Texture2D>("PixelTexture", CreatePixelTexture);
            public static BasicAssetDescriptor<Texture2D> FactionIcons => new BasicAssetDescriptor<Texture2D>("FactionIcons", @"Textures\FactionIcons");
            public static BasicAssetDescriptor<Texture2D> BlackrockMountainBackground => new BasicAssetDescriptor<Texture2D>("BlackrockMountainBackground", @"Textures\BlackrockMountainBackground");
            public static BasicAssetDescriptor<Texture2D> FerelasBackground => new BasicAssetDescriptor<Texture2D>("FerelasBackground", @"Textures\FerelasBackground");
            public static BasicAssetDescriptor<Texture2D> StonetalonBackground => new BasicAssetDescriptor<Texture2D>("StonetalonBackground", @"Textures\StonetalonBackground");
            public static BasicAssetDescriptor<Texture2D> WetlandsBackground => new BasicAssetDescriptor<Texture2D>("WetlandsBackground", @"Textures\WetlandsBackground");

            private static Texture2D CreatePixelTexture(params object[] parameters)
            {
                if (parameters == null || parameters.Length == 0)
                    throw new ArgumentException();

                GraphicsDevice graphicsDevice = parameters[0] as GraphicsDevice;
                if (graphicsDevice == null)
                    throw new ArgumentException();

                Texture2D pixelTexture = new Texture2D(graphicsDevice, 1, 1);
                pixelTexture.SetData(new Color[] { Color.White });
                return pixelTexture;
            }
        }

        public static class Fonts
        {
            public static BasicAssetDescriptor<SpriteFont> FactionButtonText => new BasicAssetDescriptor<SpriteFont>("FactionButtonText", @"Fonts\FactionButtonText");
            public static BasicAssetDescriptor<SpriteFont> ProgressBarText => new BasicAssetDescriptor<SpriteFont>("ProgressBarText", @"Fonts\ProgressBarText");
        }

        public static class SpriteSheets
        {
            public static SpriteSheetAssetDescriptor FactionIcons => new SpriteSheetAssetDescriptor("FactionIcons", Textures.FactionIcons, new Point(256, 256));
        }
    }
}
