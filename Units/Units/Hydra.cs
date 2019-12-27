using game.BattleArmyClasses;
using game.MarchingArmy;

namespace Units
{
    public class Hydra : Unit
    {
        public Hydra() : base("Hydra", 80, 15, 12, damage: (7, 14), 7)
        {
            congenitalEffects.Add(new BeatAll());
            congenitalEffects.Add(new EnemyDoesNotRespond());
        }
    }
}
