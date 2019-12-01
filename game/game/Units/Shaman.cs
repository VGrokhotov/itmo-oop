using game.MarchingArmy;

namespace game.Units
{
    public class Shaman : Unit
    {
        public Shaman() : base("Shaman", 40, 12, 10, damage: (7, 12), 10.5)
        {
            this.accessibleMagic.Add(new Curse());
            this.accessibleMagic.Add(new Attenuation());
            this.accessibleMagic.Add(new PunishingStrike());
            this.accessibleMagic.Add(new Acceleration());
        }
    }
}
