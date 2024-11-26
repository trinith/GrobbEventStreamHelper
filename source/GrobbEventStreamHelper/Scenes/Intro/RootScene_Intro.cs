using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.Controls;
using GrobbEventStreamHelper.Scenes.Intro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes
{
    public class RootScene_Intro : Scene
    {
        private static class Constants
        {
            public static ButtonView.ColourSettings GoEnabledColours { get; private set; } = new ButtonView.ColourSettings()
            {
                Normal = Color.DarkGreen,
                Hot = Color.Green,
                Pressed = Color.LightGreen
            };

            public static ButtonView.ColourSettings GoDisabledColours { get; private set; } = new ButtonView.ColourSettings()
            {
                Normal = GlobalConstants.Colours.Neutral.Dark,
            };
        }

        private IComponentContainer _parentComponents;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Button _goButton;
        private ButtonView _goButtonView;

        public RootScene_Intro(IComponentContainer components)
        {
            _parentComponents = components ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            this.Components.CopyRegisteredComponents(_parentComponents);

            _graphics = this.Components.GetComponent<GraphicsDeviceManager>();

            // Register components.
            _spriteBatch = this.Components.RegisterComponent(new SpriteBatch(_graphics.GraphicsDevice));
            IAssetBank assetBank = this.Components.RegisterComponent<IAssetBank>(new AssetBank());

            LoadContent(assetBank);

            Vector2 screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 buttonSize = GlobalConstants.Interface.ButtonSize.ToVector2();

            _goButton = new Button()
            {
                Bounds = new Rectangle(
                    (screenSize / 2 - buttonSize / 2).ToPoint(),
                    buttonSize.ToPoint()
                ),
                Text = "Begin",
                Enabled = false,
            };
            _goButton.Clicked += this.GoButton_Clicked;

            ButtonView.RenderSettings renderSettings = new ButtonView.RenderSettings(
                _spriteBatch,
                assetBank.Get(AssetRepository.Textures.Pixel),
                assetBank.Get(AssetRepository.Fonts.FactionButtonText)
            );

            this.Controllers.Add(new ButtonController(_goButton));
            this.Views.Add(_goButtonView = new ButtonView(_goButton, renderSettings, Constants.GoDisabledColours));

            int padding = 4;
            Rectangle infoPanelBounds = new Rectangle(
                padding,
                padding,
                (int)screenSize.X - padding * 2,
                _goButton.Bounds.Top - padding * 2
            );
            this.CreateChild<InfoLayer>(infoPanelBounds);

            base.OnInitialize();
        }

        private void GoButton_Clicked(object sender, EventArgs e)
        {
            SceneManager<RootSceneId> sceneManager = this.Components.GetComponent<SceneManager<RootSceneId>>();
            sceneManager.SetNextScene(RootSceneId.EventLive);
            sceneManager.EndCurrentScene();
        }

        private void LoadContent(IAssetBank assetBank)
        {
            ContentManager contentManager = this.Components.GetComponent<ContentManager>();

            assetBank.Put(AssetRepository.Textures.Pixel, _graphics.GraphicsDevice);
            assetBank.Put(contentManager, AssetRepository.Fonts.FactionButtonText);
            assetBank.Put(contentManager, AssetRepository.Textures.FerelasBackground);
            assetBank.Put(contentManager, AssetRepository.Fonts.ProgressBarText);
        }

        private void EnableGoButton()
        {
            _goButton.Enabled = true;
            _goButtonView.Colours = Constants.GoEnabledColours;
        }

        protected override void OnFixedUpdate(GameTime gameTime)
        {
            DateTime now = DateTime.Now;
            if (_goButton.Enabled == false && (GlobalConstants.Event.StartTime - now).TotalMinutes < 5)
                EnableGoButton();

            base.OnFixedUpdate(gameTime);
        }

        protected override void OnDrawBegin(GameTime gameTIme)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            IAssetBank assetBank = this.Components.GetComponent<IAssetBank>();
            _spriteBatch.Draw(assetBank.Get(AssetRepository.Textures.FerelasBackground), Vector2.Zero, Color.White);

            base.OnDrawBegin(gameTIme);
        }

        protected override void OnDrawEnd(GameTime gameTime)
        {
            _spriteBatch.End();
            base.OnDrawEnd(gameTime);
        }
    }
}
