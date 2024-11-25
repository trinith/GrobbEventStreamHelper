using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using ArbitraryPixel.Tenvis.Rendering.DebugDrawing;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.Controls;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionToggleButtonLayer : Scene
    {
        private EventModel _eventModel;

        private Dictionary<Faction, Button> _factionToggleButtons = new Dictionary<Faction, Button>();
        private SpriteBatch _spriteBatch;
        private Texture2D _pixel;

        public FactionToggleButtonLayer(EventModel eventModel)
        {
            _eventModel = eventModel ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            GraphicsDeviceManager graphics = this.Parent.Components.GetComponent<GraphicsDeviceManager>();
            Point screenSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            _spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();
            _pixel = this.Parent.Components.GetComponent<IAssetBank>().Get<Texture2D>(AssetRepository.Textures.Pixel.Name);

            GenerateButtons(screenSize);

            base.OnInitialize();
        }

        private void GenerateButtons(Point screenSize)
        {
            _factionToggleButtons.Clear();

            Faction[] factionButtons = { Faction.Alliance, Faction.Neutral, Faction.Horde };

            Point padding = new Point(8, 8);
            Point buttonSize = new Point(150, 100);
            Point anchor = new Point(
                screenSize.X / 2 - (factionButtons.Length * buttonSize.X + (factionButtons.Length - 1) * padding.X) / 2,
                screenSize.Y - buttonSize.Y - padding.Y
            );

            for (int i = 0; i < factionButtons.Length; i++)
            {
                Button newButton = new Button()
                {
                    Bounds = new Rectangle(
                        new Point(anchor.X + i * buttonSize.X + i * padding.X, anchor.Y),
                        buttonSize
                    ),
                    Text = factionButtons[i].ToString(),
                    Tag = factionButtons[i]
                };
                _factionToggleButtons.Add(factionButtons[i], newButton);
            }
        }

        protected override void OnDraw(GameTime gameTime)
        {
            foreach (Button b in _factionToggleButtons.Values)
            {
                _spriteBatch.DrawRectangle(
                    _pixel,
                    b.Bounds,
                    Color.Pink
                );
            }

            base.OnDraw(gameTime);
        }
    }
}
