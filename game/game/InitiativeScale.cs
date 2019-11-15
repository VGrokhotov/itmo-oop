﻿using System;
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

        public void CheckAttackedStack(BattleUnitsStack attackedStack)
        {
            bool isInScale = false;
            for (int j = 0; j < Scale.Count; j++)
            {
                if (Scale[j].Item1 == attackedStack)
                {
                    isInScale = true;
                    if (!Scale[j].Item1.IsAlive)
                        Scale.RemoveAt(j);
                    break;
                }
            }

            if (!isInScale)
            {
                for (int j = 0; j < WaitScale.Count; j++)
                {
                    if (WaitScale[j].Item1 == attackedStack)
                    {
                        if (!WaitScale[j].Item1.IsAlive)
                            WaitScale.RemoveAt(j);
                        break;
                    }
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
