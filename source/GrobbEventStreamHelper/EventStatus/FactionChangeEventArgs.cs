using System;

namespace GrobbEventStreamHelper.EventStatus
{
    public class FactionChangeEventArgs : EventArgs
    {
        public Faction Previous { get; private set; }
        public Faction Current { get; private set; }

        public FactionChangeEventArgs(Faction previousController, Faction newController)
        {
            this.Previous = previousController;
            this.Current = newController;
        }
    }
}
