using System;
using System.Collections.Generic;
using System.Linq;
using game.BattleArmyClasses;

namespace game
{
    public class InitiativeScale
    {
        public List<(BattleUnitsStack, int)> Scale;
        public List<(BattleUnitsStack, int)> WaitScale;

        public InitiativeScale()
        {
            Scale = new List<(BattleUnitsStack, int)>();
            WaitScale = new List<(BattleUnitsStack, int)>();
        }
        public void MakeInitiativeScale(BattleArmy FirstBattleArmy, BattleArmy SecondBattleArmy)
        {
            Scale.Clear();
            WaitScale.Clear();
            List<(BattleUnitsStack, int)> tempBUS = new List<(BattleUnitsStack, int)>();
            foreach (var stack in FirstBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    tempBUS.Add((stack, 1));
            }
            foreach (var stack in SecondBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    tempBUS.Add((stack, 2));
            }
            //ComparerOfInitiative comparerOfInitiative = new ComparerOfInitiative();
            //tempBUS.Sort(comparerOfInitiative);
            var sorted = from stackAndArmy in tempBUS
                orderby stackAndArmy.Item1.UnitType.Initiative descending
                select stackAndArmy;
            foreach (var stack in sorted)
            {
                Scale.Add(stack);
            }
        }

        public void CheckInitiativeScale()
        {
            if (Scale.Count != 0)
            {
                while (Scale.Count > 0)
                    if (!Scale[0].Item1.IsAlive)
                        Scale.RemoveAt(0);
                    else
                        break;
            }
            if (Scale.Count == 0)
            {
                if (WaitScale.Count > 0)
                {
                    while (WaitScale.Count > 0)
                        if (!WaitScale[0].Item1.IsAlive)
                            WaitScale.RemoveAt(0);
                        else
                            break;
                }
            }
        }
    }
}
