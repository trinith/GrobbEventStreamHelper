using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.EventStatus
{
    public class EventModel
    {
        public TimeSpan Duration { get; private set; }
        public TimeSpan ElapsedTime { get; private set; } = TimeSpan.Zero;
        public Faction ControllingFaction { get; private set; } = Faction.Neutral;
        public Dictionary<Faction, TimeSpan> ControlTime { get; private set; } = new Dictionary<Faction, TimeSpan>();

        public bool IsComplete
        {
            get { return this.ElapsedTime >= this.Duration; }
        }

        public event EventHandler<ControllingFactionChangedEventArgs> ControllingFactionChanged;

        public EventModel(TimeSpan duration)
        {
            if (duration == TimeSpan.Zero)
                throw new ArgumentException();

            this.Duration = duration;

            foreach (Faction faction in Enum.GetValues<Faction>())
                this.ControlTime.Add(faction, TimeSpan.Zero);
        }

        public void SetControllingFaction(Faction faction)
        {
            if (this.IsComplete)
                return;

            if (this.ControllingFaction == faction)
                return;

            Faction oldFaction = this.ControllingFaction;
            this.ControllingFaction = faction;

            if (this.ControllingFactionChanged != null)
                this.ControllingFactionChanged(this, new ControllingFactionChangedEventArgs(oldFaction, faction));
        }

        public void AddElapsedTime(TimeSpan elapsedTime)
        {
            if (this.IsComplete)
                return;

            if (this.ElapsedTime > this.Duration)
            {
                this.ElapsedTime = this.Duration;
                return;
            }

            this.ElapsedTime += elapsedTime;
            this.ControlTime[this.ControllingFaction] += elapsedTime;
        }
    }
}
