using System;
using  lab2;

namespace lab3
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
            this.FirstBattleArmy = firstBattleArmy;
            this.SecondBattleArmy = secondBattleArmy;
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
