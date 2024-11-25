using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.Controls;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class ProgressBarLayer : Scene
    {
        public static class Constants
        {
            public static int ProgressHeight = 50;
        }

        private EventModel _model;

        private ProgressBar _durationProgressBar;

        public ProgressBarLayer(EventModel model)
        {
            _model = model ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            GraphicsDeviceManager graphics = this.Parent.Components.GetComponent<GraphicsDeviceManager>();
            SpriteBatch spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();

            ProgressBarView.RenderSettings renderSettings = new ProgressBarView.RenderSettings(
                this.Parent.Components.GetComponent<SpriteBatch>(),
                this.Parent.Components.GetComponent<IAssetBank>().Get(AssetRepository.Textures.Pixel)
            );

            Point screenSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Point padding = new Point(8, 8);
            Rectangle barBounds = new Rectangle(padding.X, padding.Y, screenSize.X - padding.X * 2, Constants.ProgressHeight);

            _durationProgressBar = new ProgressBar()
            {
                Bounds = barBounds,
                CurrentProgress = () => { return _model.ElapsedTime.TotalSeconds / _model.Duration.TotalSeconds; },
            };

            // Event duration progress.
            this.Views.Add(
                new ProgressBarView(
                    _durationProgressBar,
                    renderSettings,
                    new ProgressBarView.ColourSettings()
                    {
                        Background = Color.Black,
                        Foreground = Color.Green,
                    }
                )
            );

            base.OnInitialize();
        }
    }
}
