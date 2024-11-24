﻿using ArbitraryPixel.Tenvis.Assets;
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
        private SpriteBatch _spriteBatch;

        private ProgressBarModel _durationProgressBar;

        public ProgressBarLayer(EventModel model)
        {
            _model = model ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            GraphicsDeviceManager graphics = this.Parent.Components.GetComponent<GraphicsDeviceManager>();
            Point screenSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            _spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();

            Rectangle barBounds = new Rectangle(0, 0, screenSize.X, Constants.ProgressHeight);

            _durationProgressBar = new ProgressBarModel()
            {
                Bounds = barBounds,
                CurrentProgress = () => { return _model.ElapsedTime.TotalSeconds / _model.Duration.TotalSeconds; },
                PixelTexture = this.Parent.Components.GetComponent<IAssetBank>().Get<Texture2D>(AssetRepository.Textures.Pixel.Name),
                ForegroundColour = Color.DarkGreen,
                BackgroundColour = Color.Black,
            };

            // Event duration progress.
            this.Views.Add(
                new ProgressBarView(
                    _durationProgressBar,
                    _spriteBatch
                )
            );

            base.OnInitialize();
        }
    }
}
