using  game.BattleArmyClasses;
using System.Collections.Generic;


namespace game
{
    public class ComparerOfInitiative : IComparer<(BattleUnitsStack, int)>
    {
        public int Compare((BattleUnitsStack, int) o1, (BattleUnitsStack, int) o2)
        {
            if (o1.Item1.UnitType.Initiative > o2.Item1.UnitType.Initiative)
            {
                return -1;
            }
            else if (o1.Item1.UnitType.Initiative < o2.Item1.UnitType.Initiative)
            {
                return 1;
            }

            return 0;
        }
    }
    class ComparerOfWaitInitiative : IComparer<(BattleUnitsStack, int)>
    {
        public int Compare((BattleUnitsStack, int) o1, (BattleUnitsStack, int) o2)
        {
            if (o1.Item1.UnitType.Initiative > o2.Item1.UnitType.Initiative)
            {
                return 1;
            }
            else if (o1.Item1.UnitType.Initiative < o2.Item1.UnitType.Initiative)
            {
                return -1;
            }

            return 0;
        }
    }
}
