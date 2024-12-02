using ArbitraryPixel.Tenvis.Assets.AssetDescriptors;
using GrobbEventStreamHelper.Assets;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper
{
    public static partial class GlobalConstants
    {
        public static class Event
        {
            public static string Name { get; private set; } = "Blood and Beard";
            public static DateTime StartTime { get; private set; } = new DateTime(new DateOnly(2024, 12, 02), new TimeOnly(17, 00));
            public static BasicAssetDescriptor<Texture2D> BackgroundAsset = AssetRepository.Textures.BlackrockMountainBackground;
        }
    }
}
