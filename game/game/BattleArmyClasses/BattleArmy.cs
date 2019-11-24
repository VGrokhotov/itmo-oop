using System;
using System.Collections.Generic;
using game.MarchingArmy;

namespace game.BattleArmyClasses
{
    public class BattleArmy
    {
        private readonly List<BattleUnitsStack> _stacksList;

        public string ArmyName{ get; }

        public List<BattleUnitsStack> StacksList => _stacksList;

        public int NumberOfAliveStacks
        {
            get
            {
                int number = 0;
                foreach (var stack in this.StacksList)
                {
                    if (stack.IsAlive)
                        number += 1;
                }

                return number;
            }
        } 

        public void AddStack(BattleUnitsStack battleUnitsStack)
        {
            if (this.NumberOfAliveStacks < Config.MAX_BATTLE_ARMY_NUMBER)
            
                _stacksList.Add(battleUnitsStack);
            else 
                throw new ArgumentException("To much BattleStacks");
        }

        public bool IsArmyAlive()
        {
            if (this.NumberOfAliveStacks > 0)
                return true;
            else
                return false;
        }

        public BattleArmy(Army army, string name)
        {
            if (army.StacksList.Count > Config.MAX_ARMY_NUMBER)
            {
                throw new ArgumentException("Too much Stacks");
            }
            var newStacksList = new List<BattleUnitsStack>();
            army.StacksList.ForEach((stack) => newStacksList.Add(new BattleUnitsStack(stack)));
            this._stacksList = newStacksList;
            ArmyName = name;
        }

        public string AliveStacks()
        {
            string result = $"Army {ArmyName} :\n";
            int i = 1;
            foreach (var stack in StacksList)
            {
                if (stack.IsAlive)
                {
                    result += $"[{i}] " + stack;
                    i++;
                }
            }
            return result;
        }

        public int AmountOfAliveStacks()
        {
            int result = 0;
            foreach (var stack in StacksList)
            {
                if (stack.IsAlive)
                    result++;
            }
            return result;
        }
        public BattleUnitsStack AliveStackAt(int index)
        {
            int i = 0;
            foreach (var stack in StacksList)
            {
                if (stack.IsAlive)
                    i++;
                if (i == index)
                    return stack;
            }

            return null;
        }

        public string GetLosses()
        {
            string result = "";

            foreach (var stack in StacksList)
            {
                int amount = stack.StartAmount;
                if (stack.IsAlive)
                {
                    amount -= stack.Amount;
                }
                result += $"{stack.UnitType.Name}: {amount}\n";
            }

            return result;
        }
        public override string ToString()
        {
            string result = $"Army {ArmyName} :\n";
            foreach (var stack in _stacksList)
            {
                result += stack.ToString();
            }
            return result;
        }

    }
}
