using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionIconLayer : Scene
    {
        private EventModel _eventModel;

        private SpriteBatch _spriteBatch;
        private SpriteSheet _factionIcons;

        public FactionIconLayer(EventModel eventModel)
        {
            _eventModel = eventModel ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            _spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();

            IAssetBank assetBank = this.Parent.Components.GetComponent<IAssetBank>();
            _factionIcons = assetBank.Get(AssetRepository.SpriteSheets.FactionIcons);

            base.OnInitialize();
        }

        protected override void OnDraw(GameTime gameTime)
        {
            //_spriteBatch.Draw(_factionIcons.Texture, new Vector2(0, 300), _factionIcons[0], GlobalConstants.Colours.Neutral.Dark);

            base.OnDraw(gameTime);
        }
    }
}
