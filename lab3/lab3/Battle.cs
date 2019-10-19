using System;
using  lab2;

namespace lab3
{
    class Battle
    {
        public BattleArmy FirstBattleArmy;
        public BattleArmy SecondBattleArmy;
        public bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive();

        public string WhoWin()
        {
            if (HasBattleEnded)
            {
                if (FirstBattleArmy.IsArmyAlive())
                {
                    return $"{FirstBattleArmy.ArmyName} wins";
                }
                else
                {
                    return $"{SecondBattleArmy.ArmyName} wins";
                }
            }
            else
            {
                return "Battle is not ended";
            }
        }
    }
}
