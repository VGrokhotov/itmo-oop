using System;
using game.BattleArmyClasses;

namespace game
{
    class Battle
    {
        private BattleArmy FirstBattleArmy;
        private BattleArmy SecondBattleArmy;
        public bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive();

        public BattleArmy WhoWin()
        { 
            if (FirstBattleArmy.IsArmyAlive())
            {
                return FirstBattleArmy;
            }
            else
            {
                return SecondBattleArmy;
            }
        }

        public Battle(BattleArmy firstBattleArmy, BattleArmy secondBattleArmy)
        {
            this.FirstBattleArmy = firstBattleArmy.Clone();
            this.SecondBattleArmy = secondBattleArmy.Clone();
        }

        public void StartBattle()
        {
            while (!HasBattleEnded)
            {
                ///
            }
        }
    }
}
