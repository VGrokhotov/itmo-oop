using game.BattleArmyClasses;
using game.MarchingArmy;

namespace game.Units
{
    public class Lich : Unit
    {
        public Lich() : base("Lich", 50, 15, 15, (12, 17), 10)
        {
            accessibleMagic.Add(Resurrection.GetInstance());
            congenitalEffects.Add(new Archer());
        }
    }
}
