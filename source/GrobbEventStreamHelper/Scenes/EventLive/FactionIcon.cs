using GrobbEventStreamHelper.EventStatus;
using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper.Scenes.EventLive
{
    public class FactionIcon
    {
        public Faction Faction { get; private set; }
        public Rectangle Bounds { get; set; }
        public Color Colour { get; set; } = Color.White;
        public float Alpha { get; set; } = 1f;

        public FactionIcon(Faction faction)
        {
            this.Faction = faction;
        }
    }
}
