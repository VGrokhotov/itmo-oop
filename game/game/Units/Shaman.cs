using game.MarchingArmy;

namespace game.Units
{
    public class Shaman : Unit
    {
        public Shaman() : base("Shaman", 40, 12, 10, damage: (7, 12), 10.5)
        {
            this.accessibleMagic.Add(TypeOfMagic.Curse);
            this.accessibleMagic.Add(TypeOfMagic.Attenuation);
            this.accessibleMagic.Add(TypeOfMagic.PunishingStrike);
            this.accessibleMagic.Add(TypeOfMagic.Acceleration);
        }
    }
}
