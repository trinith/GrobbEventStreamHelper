﻿using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.VisualBasic;
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

            // Event duration progress.
            this.Views.Add(
                new ProgressView(
                    new ProgressView.Settings()
                    {
                        Bounds = barBounds,
                        CurrentProgress = () => { return _model.ElapsedTime.TotalSeconds / _model.Duration.TotalSeconds; },
                        PixelTexture = this.Parent.Components.GetComponent<IAssetBank>().Get<Texture2D>(AssetRepository.Textures.Pixel.Name),
                        ForegroundColour = Color.DarkGreen,
                        BackgroundColour = Color.Black,
                    },
                    _spriteBatch
                )
            );

            base.OnInitialize();
        }

        protected override void OnUninitialize()
        {
            // GT_TODO: Roll clear calls into the scene base class.
            /* Context:
             *  If the intention is to create/setup this scene's components, controllers, and views in Initialize then
             *  the convention should be to destroy them during Uninitialize. This would imply tha the base class
             *  should do this automatically.
             */
            this.Components.Clear();
            this.Controllers.Clear();
            this.Views.Clear();
            base.OnUninitialize();
        }
    }
}
