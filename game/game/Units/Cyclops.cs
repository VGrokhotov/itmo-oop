using game.BattleArmyClasses;
using game.MarchingArmy;

namespace game.Units
{
    public class Cyclops: Unit
    {
        public Cyclops() : base("Cyclops", 85, 20, 15, damage: (18, 26), 10)
        {
            this.accessibleMagic.Add(Attenuation.GetInstance());
        }
    }
}
