using game.BattleArmyClasses;
using game.MarchingArmy;

namespace game.Units
{
    public class Lich : Unit
    {
        public Lich() : base("Lich", 50, 15, 15, (12, 17), 10)
        {
            this.accessibleMagic.Add(TypeOfMagic.Resurrection);
            congenitalEffects.Add(TypeOfEffect.Archer);
        }
    }
}
