using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using System;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionIconAlphaFadeController : ControllerBase
    {
        private FactionIcon _model;
        private float _targetAlpha;
        private float _currentTime = 1;
        private float _speed = 0;

        public FactionIconAlphaFadeController(FactionIcon model)
        {
            _model = model ?? throw new ArgumentNullException();
            _targetAlpha = model.Alpha;
        }

        public void SetupTransition(float newAlpha, float transitionTime)
        {
            if (newAlpha == _model.Alpha)
                return;

            _targetAlpha = MathHelper.Clamp(newAlpha, 0, 1);
            _speed = (_targetAlpha - _model.Alpha) / transitionTime;
        }

        protected override void OnFixedUpdate(GameTime gameTime)
        {
            if (_targetAlpha == _model.Alpha)
                return;

            _model.Alpha += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(_model.Alpha - _targetAlpha) < 0.0001)
                _model.Alpha = _targetAlpha;

            base.OnFixedUpdate(gameTime);
        }
    }
}
