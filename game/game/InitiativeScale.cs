using System;
using System.Collections.Generic;
using System.Linq;
using game.BattleArmyClasses;

namespace game
{
    public class InitiativeScale
    {
        public List<BattleUnitsStack> Scale;
        public List<BattleUnitsStack> WaitScale;

        public InitiativeScale()
        {
            Scale = new List<BattleUnitsStack>();
            WaitScale = new List<BattleUnitsStack>();
        }
        public void MakeInitiativeScale(BattleArmy FirstBattleArmy, BattleArmy SecondBattleArmy)
        {
            Scale.Clear();
            WaitScale.Clear();
            List<BattleUnitsStack> tempBUS = new List<BattleUnitsStack>();
            foreach (var stack in FirstBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    tempBUS.Add(stack);
            }
            foreach (var stack in SecondBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    tempBUS.Add(stack);
            }
            var sorted = from stack in tempBUS
                orderby stack.UnitType.Initiative descending
                select stack;
            foreach (var stack in sorted)
            {
                Scale.Add(stack);
            }
        }
    }
}
