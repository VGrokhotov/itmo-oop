using game.MarchingArmy;

namespace game.Units
{
    public class Devil : Unit
    {
        public Devil() : base("Devil", 166, 27, 25, damage: (36, 66), 11)
        {
            this.accessibleMagic.Add(Attenuation.GetInstance());
            this.accessibleMagic.Add(Curse.GetInstance());
            this.accessibleMagic.Add(PunishingStrike.GetInstance());
        }
    }
}
