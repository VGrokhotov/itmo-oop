using game.BattleArmyClasses;
using game.MarchingArmy;

namespace Units
{
    public class Arbalester : Unit
    {
        public Arbalester() : base("Arbalester", 10, 4, 4, (2, 8), 8)
        {
            congenitalEffects.Add(new Archer());
            //congenitalEffects.Add(TypeOfEffect.AccurateShot);
        }
    }
}
