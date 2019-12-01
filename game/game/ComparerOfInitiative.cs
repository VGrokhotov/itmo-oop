using  game.BattleArmyClasses;
using System.Collections.Generic;


namespace game
{
    public class ComparerOfInitiative : IComparer<(BattleUnitsStack, TypeOfArmy)>
    {
        public int Compare((BattleUnitsStack, TypeOfArmy) o1, (BattleUnitsStack, TypeOfArmy) o2)
        {
            if (o1.Item1.BattleUnit.Initiative > o2.Item1.BattleUnit.Initiative )
            {
                return -1;
            }
            else if (o1.Item1.BattleUnit.Initiative  < o2.Item1.BattleUnit.Initiative )
            {
                return 1;
            }

            return 0;
        }
    }
    class ComparerOfWaitInitiative : IComparer<(BattleUnitsStack, TypeOfArmy)>
    {
        public int Compare((BattleUnitsStack, TypeOfArmy) o1, (BattleUnitsStack, TypeOfArmy) o2)
        {
            if (o1.Item1.BattleUnit.Initiative > o2.Item1.BattleUnit.Initiative )
            {
                return 1;
            }
            else if (o1.Item1.BattleUnit.Initiative < o2.Item1.BattleUnit.Initiative)
            {
                return -1;
            }
            return 0;
        }
    }
}
