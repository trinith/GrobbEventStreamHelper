using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using System;

namespace GrobbEventStreamHelper.EventStatus
{
    public class EventStatusTimeController : ControllerBase
    {
        private EventModel _model;
        private TimeScale _timeScale;

        public EventStatusTimeController(EventModel model, TimeScale timeScale)
        {
            _model = model ?? throw new ArgumentNullException();
            _timeScale = timeScale ?? new TimeScale();
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            _model.AddElapsedTime(gameTime.ElapsedGameTime * _timeScale.Value);
            base.OnUpdate(gameTime);
        }
    }
}
