﻿using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using GrobbEventStreamHelper.Controls;
using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionToggleButtonLayer : Scene
    {
        public static class Constants
        {
            public static class ButtonColourSettings
            {
                public static ButtonView.ColourSettings Alliance { get; private set; } = new ButtonView.ColourSettings()
                {
                    Normal = GlobalConstants.Colours.Alliance.Dark,
                    Hot = GlobalConstants.Colours.Alliance.Normal,
                    Pressed = GlobalConstants.Colours.Alliance.Light,
                };

                public static ButtonView.ColourSettings Horde { get; private set; } = new ButtonView.ColourSettings()
                {
                    Normal = GlobalConstants.Colours.Horde.Dark,
                    Hot = GlobalConstants.Colours.Horde.Normal,
                    Pressed = GlobalConstants.Colours.Horde.Light,
                };

                public static ButtonView.ColourSettings Neutral { get; private set; } = new ButtonView.ColourSettings()
                {
                    Normal = GlobalConstants.Colours.Neutral.Dark,
                    Hot = GlobalConstants.Colours.Neutral.Normal,
                    Pressed = GlobalConstants.Colours.Neutral.Light,
                };
            }
        }

        private EventModel _eventModel;

        private Dictionary<Faction, ToggleButton> _factionToggleButtons = new Dictionary<Faction, ToggleButton>();

        private Dictionary<Faction, ButtonView.ColourSettings> _buttonColourSettings = new Dictionary<Faction, ButtonView.ColourSettings>();

        public FactionToggleButtonLayer(EventModel eventModel)
        {
            _eventModel = eventModel ?? throw new ArgumentNullException();

            _buttonColourSettings.Add(Faction.Alliance, Constants.ButtonColourSettings.Alliance);
            _buttonColourSettings.Add(Faction.Neutral, Constants.ButtonColourSettings.Neutral);
            _buttonColourSettings.Add(Faction.Horde, Constants.ButtonColourSettings.Horde);
        }

        protected override void OnInitialize()
        {
            GraphicsDeviceManager graphics = this.Parent.Components.GetComponent<GraphicsDeviceManager>();
            Point screenSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            GenerateButtons(screenSize);

            base.OnInitialize();
        }

        private void GenerateButtons(Point screenSize)
        {
            _factionToggleButtons.Clear();

            Faction[] factionButtons = { Faction.Alliance, Faction.Neutral, Faction.Horde };

            Point padding = new Point(8, 8);
            Point buttonSize = GlobalConstants.Interface.ButtonSize;
            Point anchor = new Point(
                screenSize.X / 2 - (factionButtons.Length * buttonSize.X + (factionButtons.Length - 1) * padding.X) / 2,
                screenSize.Y - buttonSize.Y - padding.Y
            );

            IAssetBank assetBank = this.Parent.Components.GetComponent<IAssetBank>();

            ButtonView.RenderSettings renderSettings = new ButtonView.RenderSettings(
                this.Parent.Components.GetComponent<SpriteBatch>(),
                assetBank.Get(AssetRepository.Textures.Pixel),
                assetBank.Get(AssetRepository.Fonts.FactionButtonText)
            );

            for (int i = 0; i < factionButtons.Length; i++)
            {
                Faction faction = factionButtons[i];

                ToggleButton newButton = new ToggleButton()
                {
                    Bounds = new Rectangle(
                        new Point(anchor.X + i * buttonSize.X + i * padding.X, anchor.Y),
                        buttonSize
                    ),
                    Text = faction.ToString(),
                    Tag = faction
                };

                if (faction == _eventModel.ControllingFaction)
                    newButton.Activate();

                _factionToggleButtons.Add(faction, newButton);

                newButton.Clicked += this.EventHandler_ButtonClicked;

                this.Controllers.Add(new ButtonController(newButton));
                this.Views.Add(new ToggleButtonView(newButton, renderSettings, _buttonColourSettings[faction]));
            }

            IEnumerable<ToggleButton> buttonGroup = _factionToggleButtons.Values.ToArray();
            foreach (ToggleButton button in buttonGroup)
                button.LinkTo(buttonGroup);
        }

        private void EventHandler_ButtonClicked(object sender, EventArgs e)
        {
            ToggleButton b = sender as ToggleButton;
            if (b == null)
                return;

            if (b.Tag is not Faction)
                return;

            b.Activate();
            _eventModel.ControllingFaction = (Faction)b.Tag;
        }
    }
}
