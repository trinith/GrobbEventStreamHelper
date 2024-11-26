using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes.Intro
{
    public class InfoLayer : Scene
    {
        public static class Constants
        {
            public static int BorderThickness = 4;

            public static class PanelColours
            {
                public static Color Foreground => Color.DarkGreen;
                public static Color Background => Color.Black;
            }

            public static class TextColours
            {
                public static Color Highlight => GlobalConstants.Colours.Neutral.Light;
                public static Color Normal => GlobalConstants.Colours.Neutral.Normal;
            }
        }

        private Rectangle _bounds;

        private SpriteBatch _spriteBatch;
        private Texture2D _pixel;
        private SpriteFont _headingFont;
        private SpriteFont _normalFont;

        public InfoLayer(Rectangle bounds)
        {
            _bounds = bounds;
        }

        protected override void OnInitialize()
        {
            _spriteBatch = this.Parent.Components.GetComponent<SpriteBatch>();

            IAssetBank assetBank = this.Parent.Components.GetComponent<IAssetBank>();
            _pixel = assetBank.Get(AssetRepository.Textures.Pixel);
            _headingFont = assetBank.Get(AssetRepository.Fonts.FactionButtonText);
            _normalFont = assetBank.Get(AssetRepository.Fonts.ProgressBarText);

            base.OnInitialize();
        }

        protected override void OnDraw(GameTime gameTime)
        {
            Rectangle bounds = _bounds;
            DrawPanelBackground(ref bounds);
            DrawPanelInfoText(ref bounds);

            base.OnDraw(gameTime);
        }

        private void DrawPanelBackground(ref Rectangle bounds)
        {
            _spriteBatch.Draw(_pixel, bounds, Constants.PanelColours.Background);
            bounds.Inflate(-Constants.BorderThickness / 2, -Constants.BorderThickness / 2);
            _spriteBatch.Draw(_pixel, bounds, Constants.PanelColours.Foreground);
            bounds.Inflate(-Constants.BorderThickness, -Constants.BorderThickness);
            _spriteBatch.Draw(_pixel, bounds, Constants.PanelColours.Background);
        }

        private void DrawPanelInfoText(ref Rectangle bounds)
        {
            float y = bounds.Top + Constants.BorderThickness;
            DrawPanelText($"{GlobalConstants.Event.Name}", ref y, _headingFont, bounds, Constants.TextColours.Highlight);
            DrawPanelText("Prepare yourselves, heroes, we will be starting in ...", ref y, _normalFont, bounds, Constants.TextColours.Normal);
            
            y += _normalFont.LineSpacing;
            TimeSpan timeRemaining = GlobalConstants.Event.StartTime - DateTime.Now;
            if (timeRemaining.TotalSeconds <= 0)
                DrawPanelText("NOW", ref y, _headingFont, bounds, Constants.TextColours.Highlight);
            else
                DrawPanelText($"{(int)timeRemaining.TotalMinutes + 1}", ref y, _headingFont, bounds, Constants.TextColours.Highlight);
            y -= _headingFont.LineSpacing;
            y += _normalFont.LineSpacing * 2;
            DrawPanelText("... minutes!", ref y, _normalFont, bounds, Constants.TextColours.Normal);
        }

        private void DrawPanelText(string text, ref float yPos, SpriteFont font, Rectangle panelBounds, Color colour)
        {
            _spriteBatch.DrawString(
                font,
                text,
                new Vector2(GetHorizontalPositionForText(text, panelBounds, font), yPos),
                colour
            );
            yPos += font.LineSpacing;
        }

        private float GetHorizontalPositionForText(string text, Rectangle bounds, SpriteFont font)
        {
            Vector2 textSize = font.MeasureString(text);
            return bounds.Left + (bounds.Width / 2) - textSize.X / 2;
        }
    }
}
