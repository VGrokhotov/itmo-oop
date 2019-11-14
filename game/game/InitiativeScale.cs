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
            foreach (var stack in FirstBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    Scale.Add((stack, 1));
            }
            foreach (var stack in SecondBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    Scale.Add((stack, 2));
            }
            ComparerOfInitiative comparerOfInitiative = new ComparerOfInitiative();
            Scale.Sort(comparerOfInitiative);
        }

        //переделать или удолить - надо чистить шкалу при драке поцанов
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

        public override string ToString()
        {
            string result = "Initiative Scale:\n";
            foreach (var stack in Scale)
            {
                result += $"Army: {stack.Item2}, {stack.Item1}";
            }
            result += "Initiative Wait Scale:\n";
            foreach (var stack in WaitScale)
            {
                result += $"Army: {stack.Item2}, {stack.Item1}";
            }

            return result;

        }
    }
}
