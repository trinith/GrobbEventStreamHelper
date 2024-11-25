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
            Rectangle bounds = new Rectangle(padding.X, padding.Y, screenSize.X - padding.X * 2, Constants.ProgressHeight);

            // Event duration progress.
            this.Views.Add(
                new ProgressBarView(
                    new ProgressBar()
                    {
                        Bounds = bounds,
                        CurrentProgress = () => { return _model.ElapsedTime.TotalSeconds / _model.Duration.TotalSeconds; },
                    },
                    renderSettings,
                    new ProgressBarView.ColourSettings()
                    {
                        Background = Color.Black,
                        Foreground = Color.Green,
                    }
                )
            );

            // Neutral progress.
            bounds.Location += new Point(0, bounds.Height + padding.Y);
            this.Views.Add(
                new ProgressBarView(
                    new ProgressBar()
                    {
                        Bounds = bounds,
                        CurrentProgress = () => { return _model.ControlTime[Faction.Neutral] / _model.ControlTime[_model.WinningFaction]; },
                    },
                    renderSettings,
                    new ProgressBarView.ColourSettings()
                    {
                        Background = Color.Black,
                        Foreground = GlobalConstants.Colours.Neutral.Normal,
                    }
                )
            );

            // Alliance progress.
            bounds.Location += new Point(0, bounds.Height + padding.Y);
            this.Views.Add(
                new ProgressBarView(
                    new ProgressBar()
                    {
                        Bounds = bounds,
                        CurrentProgress = () => { return _model.ControlTime[Faction.Alliance] / _model.ControlTime[_model.WinningFaction]; },
                    },
                    renderSettings,
                    new ProgressBarView.ColourSettings()
                    {
                        Background = Color.Black,
                        Foreground = GlobalConstants.Colours.Alliance.Normal,
                    }
                )
            );

            // Horde progress.
            bounds.Location += new Point(0, bounds.Height + padding.Y);
            this.Views.Add(
                new ProgressBarView(
                    new ProgressBar()
                    {
                        Bounds = bounds,
                        CurrentProgress = () => { return _model.ControlTime[Faction.Horde] / _model.ControlTime[_model.WinningFaction]; },
                    },
                    renderSettings,
                    new ProgressBarView.ColourSettings()
                    {
                        Background = Color.Black,
                        Foreground = GlobalConstants.Colours.Horde.Normal,
                    }
                )
            );

            base.OnInitialize();
        }
    }
}
