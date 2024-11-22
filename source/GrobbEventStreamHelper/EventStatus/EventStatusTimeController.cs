using ArbitraryPixel.Tenvis.Core;
using Microsoft.Xna.Framework;
using System;

namespace GrobbEventStreamHelper.EventStatus
{
    public class EventStatusTimeController : ControllerBase
    {
        public EventModel _model;

        public double TimeScale { get; set; } = 1.0;

        public EventStatusTimeController(EventModel model)
        {
            _model = model ?? throw new ArgumentNullException();
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            _model.AddElapsedTime(gameTime.ElapsedGameTime * this.TimeScale);
            base.OnUpdate(gameTime);
        }
    }
}
