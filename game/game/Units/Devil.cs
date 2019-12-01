using game.MarchingArmy;

namespace game.Units
{
    public class Devil : Unit
    {
        public Devil() : base("Devil", 166, 27, 25, damage: (36, 66), 11)
        {
            this.accessibleMagic.Add(new Attenuation());
            this.accessibleMagic.Add(new Curse());
            this.accessibleMagic.Add(new PunishingStrike());
        }
    }
}
