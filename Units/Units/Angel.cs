using game;
using game.MarchingArmy;

namespace Units
{
    public class Angel : Unit
    {
        public Angel() : base("Angel", 180, 27, 27, damage: (45, 45), 11)
        {
            this.accessibleMagic.Add(Resurrection.GetInstance());
            this.accessibleMagic.Add(PunishingStrike.GetInstance());
        }
    }
}
