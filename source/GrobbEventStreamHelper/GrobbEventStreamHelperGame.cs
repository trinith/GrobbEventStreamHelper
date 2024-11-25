using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper
{
    public class GrobbEventStreamHelperGame : Game
    {
        private GraphicsDeviceManager _graphics;

        private EngineCore _engine;

        public GrobbEventStreamHelperGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            IComponentContainer components = new SimpleComponentContainer();
            components.RegisterComponent(_graphics);
            components.RegisterComponent(this.Content);
#if DEBUG
            components.RegisterComponent(new TimeScale() { Value = 100.0 });
#endif

            _engine = new EngineCore(components);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _engine.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            _engine.UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime);
            if (_engine.IsFinished)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
