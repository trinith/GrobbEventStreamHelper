using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class RootScene_EventLive : Scene
    {
        private IComponentContainer _parentComponents;

        private GraphicsDevice _graphics;

        public RootScene_EventLive(IComponentContainer components)
        {
            _parentComponents = components ?? throw new ArgumentNullException();
        }

        protected override void OnInitialize()
        {
            this.Components.CopyRegisteredComponents(_parentComponents);

            _graphics = this.Components.GetComponent<GraphicsDeviceManager>().GraphicsDevice;

            base.OnInitialize();
        }

        protected override void OnUninitialize()
        {
            this.Components.GetComponent<ContentManager>().Unload();
            this.Components.Clear();

            base.OnUninitialize();
        }

        protected override void OnDraw(GameTime gameTime)
        {
            _graphics.Clear(Color.Black);
        }
    }
}
