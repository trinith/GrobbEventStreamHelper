using System;

namespace GrobbEventStreamHelper.EventStatus
{
    public class ControllingFactionChangedEventArgs : EventArgs
    {
        public Faction PreviousController { get; private set; }
        public Faction NewController { get; private set; }

        public ControllingFactionChangedEventArgs(Faction previousController, Faction newController)
        {
            this.PreviousController = previousController;
            this.NewController = newController;
        }
    }
}
