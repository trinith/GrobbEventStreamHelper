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
            public static BasicAssetDescriptor<SpriteFont> FactionButtonText => new BasicAssetDescriptor<SpriteFont>("FactionButtonText", @"Fonts\Debug");
        }
    }
}
