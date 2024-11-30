using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class RootScene_EventLive : Scene
    {
        public static class Constants
        {
            public const double EventDurationMinutes = 90;
        }

        private IComponentContainer _parentComponents;

        private GraphicsDevice _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _backdrop;

        public RootScene_EventLive(IComponentContainer components)
        {
            _parentComponents = components ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            this.Components.CopyRegisteredComponents(_parentComponents);

            _graphics = this.Components.GetComponent<GraphicsDeviceManager>().GraphicsDevice;

            // Register components.
            _spriteBatch = this.Components.RegisterComponent(new SpriteBatch(_graphics));
            EventModel eventModel = this.Components.RegisterComponent(new EventModel(TimeSpan.FromMinutes(Constants.EventDurationMinutes)));
            eventModel.ControllingFactionChanged += (sender, e) => Trace.WriteLine($"Controlling faction changed to {e.Current}.");

            this.Components.RegisterComponent<IAssetBank>(new AssetBank());

            // Load content.
            this.LoadContent();

            _backdrop = this.Components.GetComponent<IAssetBank>().Get(GlobalConstants.Event.BackgroundAsset);

            // Register Controllers.
            this.Controllers.Add(new EventStatusTimeController(eventModel, this.Components.TryGetComponent<TimeScale>()));

            this.Views.Add(
                new GenericView(
                    (elapsedTime) =>
                    {
                        _spriteBatch.Draw(_backdrop, Vector2.Zero, Color.White);
                    }
                )
            );

            // Register child scenes.
            this.CreateChild<ProgressBarLayer>(eventModel);
            this.CreateChild<FactionToggleButtonLayer>(eventModel);
            this.CreateChild<FactionIconLayer>(eventModel);

            base.OnInitialize();
        }

        private void LoadContent()
        {
            IAssetBank assetBank = this.Components.GetComponent<IAssetBank>();
            ContentManager contentManager = this.Components.GetComponent<ContentManager>();

            assetBank.Put(AssetRepository.Textures.Pixel, _graphics);
            assetBank.Put(contentManager, AssetRepository.Fonts.FactionButtonText);
            assetBank.Put(contentManager, GlobalConstants.Event.BackgroundAsset);
            assetBank.Put(contentManager, AssetRepository.Fonts.ProgressBarText);
            assetBank.Put(contentManager, AssetRepository.SpriteSheets.FactionIcons);
        }

        protected override void OnUninitialize()
        {
            this.Components.GetComponent<ContentManager>().Unload();
            base.OnUninitialize();
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);
        }

        protected override void OnDrawBegin(GameTime gameTIme)
        {
            _graphics.Clear(Color.DarkMagenta);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            base.OnDrawBegin(gameTIme);
        }

        protected override void OnDrawEnd(GameTime gameTime)
        {
            _spriteBatch.End();

            base.OnDrawEnd(gameTime);
        }
    }
}
