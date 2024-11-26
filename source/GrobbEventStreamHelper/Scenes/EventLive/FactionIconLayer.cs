using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionIconLayer : Scene
    {
        private EventModel _eventModel;

        private Dictionary<Faction, FactionIconAlphaFadeController> _fadeControllers = new Dictionary<Faction, FactionIconAlphaFadeController>();

        public FactionIconLayer(EventModel eventModel)
        {
            _eventModel = eventModel ?? throw new ArgumentNullException();
            _eventModel.ControllingFactionChanged += this.EventModel_ControllingFactionChanged;
        }

        protected override void OnInitialize()
        {
            GraphicsDeviceManager graphics = this.Parent.Components.GetComponent<GraphicsDeviceManager>();
            SpriteBatch spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();

            IAssetBank assetBank = this.Parent.Components.GetComponent<IAssetBank>();
            SpriteSheet factionIcons = assetBank.Get(AssetRepository.SpriteSheets.FactionIcons);

            Point screenSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Tuple<Faction, Color>[] factionSet =
            {
                new Tuple<Faction, Color>(Faction.Alliance, GlobalConstants.Colours.Alliance.Normal),
                new Tuple<Faction, Color>(Faction.Neutral, GlobalConstants.Colours.Neutral.Dark),
                new Tuple<Faction, Color>(Faction.Horde, GlobalConstants.Colours.Horde.Normal),
            };

            foreach (var item in factionSet)
            {
                FactionIcon iconModel = new FactionIcon(item.Item1)
                {
                    Bounds = new Rectangle(new Point((screenSize.X - factionIcons.SpriteSize.X) / 2, 300), factionIcons.SpriteSize),
                    Colour = item.Item2,
                    Alpha = (item.Item1 == _eventModel.ControllingFaction) ? 1f : 0f,
                };

                FactionIconAlphaFadeController controller = new FactionIconAlphaFadeController(iconModel);
                _fadeControllers.Add(item.Item1, controller);

                this.Controllers.Add(controller);
                this.Views.Add(new FactionIconView(iconModel, spriteBatch, factionIcons));
            }

            base.OnInitialize();
        }

        private void EventModel_ControllingFactionChanged(object sender, FactionChangeEventArgs e)
        {
            float transitionTime = 1;
            _fadeControllers[e.Previous].SetupTransition(0, transitionTime);
            _fadeControllers[e.Current].SetupTransition(1, transitionTime);
        }
    }
}
