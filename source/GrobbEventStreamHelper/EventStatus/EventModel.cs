using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.EventStatus
{
    public class EventModel
    {
        private Faction _controllingFaction = Faction.Neutral;
        private Faction _winningFaction = Faction.Neutral;

        public TimeSpan Duration { get; private set; }
        public TimeSpan ElapsedTime { get; private set; } = TimeSpan.Zero;
        public Faction ControllingFaction
        {
            get { return _controllingFaction; }
            set
            {
                if (this.IsComplete)
                    return;

                if (_controllingFaction == value)
                    return;

                FactionChangeEventArgs args = new FactionChangeEventArgs(_controllingFaction, value);
                _controllingFaction = args.Current;

                if (this.ControllingFactionChanged != null)
                    this.ControllingFactionChanged(this, args);
            }
        }

        public Faction WinningFaction
        {
            get { return _winningFaction; }
            set
            {
                if (this.IsComplete)
                    return;

                if (_winningFaction == value)
                    return;

                FactionChangeEventArgs args = new FactionChangeEventArgs(_winningFaction, value);
                _winningFaction = args.Current;

                if (this.WinningFactionChanged != null)
                    this.WinningFactionChanged(this, args);
            }
        }

        public Dictionary<Faction, TimeSpan> ControlTime { get; private set; } = new Dictionary<Faction, TimeSpan>();

        public bool IsComplete
        {
            get { return this.ElapsedTime >= this.Duration; }
        }

        public event EventHandler<FactionChangeEventArgs> ControllingFactionChanged;
        public event EventHandler<FactionChangeEventArgs> WinningFactionChanged;

        public EventModel(TimeSpan duration)
        {
            if (duration == TimeSpan.Zero)
                throw new ArgumentException();

            this.Duration = duration;

            foreach (Faction faction in Enum.GetValues<Faction>())
                this.ControlTime.Add(faction, TimeSpan.Zero);

            this.WinningFaction = this.ControllingFaction;
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

            if (this.ControllingFaction != this.WinningFaction && this.ControlTime[this.ControllingFaction] > this.ControlTime[this.WinningFaction])
                this.WinningFaction = this.ControllingFaction;
        }
    }
}
