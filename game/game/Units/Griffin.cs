using game.BattleArmyClasses;
using game.MarchingArmy;

namespace game.Units
{
    public class Griffin: Unit
    {
        public Griffin() : base("Griffin", 30, 7, 5, damage: (7, 12), 15)
        {
            congenitalEffects.Add(TypeOfEffect.EndlessRebuff);
        }
    }
}
