using  game.BattleArmyClasses;
using System.Collections.Generic;


namespace game
{
    public class ComparerOfInitiative : IComparer<(BattleUnitsStack, int)>
    {
        public int Compare((BattleUnitsStack, int) o1, (BattleUnitsStack, int) o2)
        {
            double initiativeMultiplier1 = 1;
            double initiativeMultiplier2 = 1;
            foreach (var effect in o1.Item1.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.IncreasedInitiative)
                {
                    initiativeMultiplier1 *= Config.INCREASED_INITIATIVE;
                    break;
                }
                    
            }
            foreach (var effect in o2.Item1.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.IncreasedInitiative)
                {
                    initiativeMultiplier2 *= Config.INCREASED_INITIATIVE;
                    break;
                }

            }
            if (o1.Item1.UnitType.Initiative * initiativeMultiplier1 > o2.Item1.UnitType.Initiative * initiativeMultiplier2)
            {
                return -1;
            }
            else if (o1.Item1.UnitType.Initiative * initiativeMultiplier1 < o2.Item1.UnitType.Initiative * initiativeMultiplier2)
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
            double initiativeMultiplier1 = 1;
            double initiativeMultiplier2 = 1;
            foreach (var effect in o1.Item1.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.IncreasedInitiative)
                {
                    initiativeMultiplier1 *= Config.INCREASED_INITIATIVE;
                    break;
                }

            }
            foreach (var effect in o2.Item1.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.IncreasedInitiative)
                {
                    initiativeMultiplier2 *= Config.INCREASED_INITIATIVE;
                    break;
                }

            }
            if (o1.Item1.UnitType.Initiative * initiativeMultiplier1 > o2.Item1.UnitType.Initiative * initiativeMultiplier2)
            {
                return 1;
            }
            else if (o1.Item1.UnitType.Initiative * initiativeMultiplier1 < o2.Item1.UnitType.Initiative * initiativeMultiplier2)
            {
                return -1;
            }
            return 0;
        }
    }
}
