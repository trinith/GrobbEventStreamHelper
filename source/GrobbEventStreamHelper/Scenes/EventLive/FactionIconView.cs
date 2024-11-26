using ArbitraryPixel.Tenvis.Assets;
using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionIconView : ViewBase
    {
        private FactionIcon _model;
        private SpriteBatch _spriteBatch;
        private SpriteSheet _spriteSheet;

        private struct IconRenderInfo
        {
            public Vector2 Offset;
            public Color? Colour;
        }

        private List<IconRenderInfo> _iconRenders = new List<IconRenderInfo>();

        public FactionIconView(FactionIcon model, SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            _model = model ?? throw new ArgumentNullException();
            _spriteBatch = spriteBatch ?? throw new ArgumentNullException();
            _spriteSheet = spriteSheet ?? throw new ArgumentNullException();

            _iconRenders.AddRange(
                new IconRenderInfo[]
                {
                    new IconRenderInfo() { Offset = new Vector2(-1, 0), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(1, 0), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(0, 1), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(0, -1), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(-2, 0), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(2, 0), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(0, 2), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(0, -2), Colour = Color.Black },
                    new IconRenderInfo() { Offset = new Vector2(0, 0), Colour = null },
                }
            );
        }

        protected override void OnDraw(GameTime gameTime)
        {
            if (_model.Alpha <= 0)
                return;

            foreach (IconRenderInfo info in _iconRenders)
            {
                Color colour = (info.Colour != null) ? info.Colour.Value : _model.Colour;

                _spriteBatch.Draw(
                    _spriteSheet.Texture,
                    _model.Bounds.Location.ToVector2() + info.Offset,
                    _spriteSheet[(uint)_model.Faction],
                    new Color(colour, _model.Alpha)
                );
            }

            base.OnDraw(gameTime);
        }
    }
}
