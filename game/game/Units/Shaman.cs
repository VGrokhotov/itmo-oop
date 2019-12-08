using game.MarchingArmy;

namespace game.Units
{
    public class Shaman : Unit
    {
        public Shaman() : base("Shaman", 40, 12, 10, damage: (7, 12), 10.5)
        {
            this.accessibleMagic.Add(Curse.GetInstance());
            this.accessibleMagic.Add(Attenuation.GetInstance());
            this.accessibleMagic.Add(PunishingStrike.GetInstance());
            this.accessibleMagic.Add(Acceleration.GetInstance());
        }
    }
}
