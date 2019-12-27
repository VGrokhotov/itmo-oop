using System.Collections.Generic;
using game.BattleArmyClasses;

namespace game
{
    public class InitiativeScale
    {
        public List<(BattleUnitsStack, TypeOfArmy)> Scale;
        public List<(BattleUnitsStack, TypeOfArmy)> WaitScale;
        private string firstPlayerName;
        private string secondPlayerName;

        public InitiativeScale(string firstPlName, string secondPlName)
        {
            Scale = new List<(BattleUnitsStack, TypeOfArmy)>();
            WaitScale = new List<(BattleUnitsStack, TypeOfArmy)>();
            firstPlayerName = firstPlName;
            secondPlayerName = secondPlName;
        }
        public void MakeInitiativeScale(BattleArmy firstBattleArmy, BattleArmy secondBattleArmy)
        {
            Scale.Clear();
            WaitScale.Clear();
            foreach (var stack in firstBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    Scale.Add((stack, TypeOfArmy.First));
            }
            foreach (var stack in secondBattleArmy.StacksList)
            {
                if (stack.IsAlive)
                    Scale.Add((stack, TypeOfArmy.Second));
            }
            ComparerOfInitiative comparerOfInitiative = new ComparerOfInitiative();
            Scale.Sort(comparerOfInitiative);
        }

        public void SortScales()
        {
            ComparerOfInitiative comparerOfInitiative = new ComparerOfInitiative();
            Scale.Sort(comparerOfInitiative);
            ComparerOfWaitInitiative comparerOfWaitInitiative = new ComparerOfWaitInitiative();
            WaitScale.Sort(comparerOfWaitInitiative);
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
                string armyName = stack.Item2 == TypeOfArmy.First ? firstPlayerName : secondPlayerName;
                result += $"Army of {armyName}, {stack.Item1}";
            }
            result += "Initiative Wait Scale:\n";
            foreach (var stack in WaitScale)
            {
                string armyName = stack.Item2 == TypeOfArmy.First ? firstPlayerName : secondPlayerName;
                result += $"Army of {armyName}, {stack.Item1}";
            }

            return result;

        }
    }
}
