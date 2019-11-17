using game.MarchingArmy;

namespace game.Units
{
    public class Angel : Unit
    {
        public Angel() : base("Angel", 180, 27, 27, damage: (45, 45), 11)
        {
            this.accessibleMagic.Add(TypeOfMagic.Resurrection);
            this.accessibleMagic.Add(TypeOfMagic.PunishingStrike);
        }
    }
}
