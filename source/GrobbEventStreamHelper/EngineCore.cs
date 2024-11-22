using ArbitraryPixel.Tenvis.Core;
using GrobbEventStreamHelper.Scenes;
using GrobbEventStreamHelper.Scenes.EventLive;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrobbEventStreamHelper
{
    public class EngineCore : GameEngine
    {
        private SceneManager<RootSceneId> _sceneManager = new SceneManager<RootSceneId>();

        public EngineCore(IComponentContainer components)
        {
            this.Components.CopyRegisteredComponents(components);
            this.Components.RegisterComponent(_sceneManager);

            _sceneManager.RegisterScene(RootSceneId.EventLive, new RootScene_EventLive(this.Components));
        }

        protected override void OnLoadContent()
        {
            _sceneManager.SetScene(RootSceneId.EventLive);
            base.OnLoadContent();
        }

        protected override void OnExit()
        {
            _sceneManager.UnregisterScenes();
            base.OnExit();
        }

        protected override void OnFixedUpdate(GameTime gameTime)
        {
            _sceneManager.FixedUpdate(gameTime);
            base.OnFixedUpdate(gameTime);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
#if DEBUG
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                _sceneManager.EndCurrentScene();
#endif
            _sceneManager.Update(gameTime);

            if (_sceneManager.CurrentScene == null)
                this.Exit();

            base.OnUpdate(gameTime);
        }

        protected override void OnDraw(GameTime gameTime)
        {
#if DEBUG
            this.Components.GetComponent<GraphicsDeviceManager>().GraphicsDevice.Clear(Color.DarkMagenta);
#endif
            _sceneManager.Draw(gameTime);
            base.OnDraw(gameTime);
        }
    }
}
