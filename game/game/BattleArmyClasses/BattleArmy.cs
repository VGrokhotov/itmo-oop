using System;
using System.Collections.Generic;

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

        public BattleArmy(List<BattleUnitsStack> stacksList, string name)
        {
            if (stacksList.Count > Config.MAX_ARMY_NUMBER)
            {
                throw new ArgumentException("To much Stacks");
            }
            var newStacksList = new List<BattleUnitsStack>();
            stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
            this._stacksList = newStacksList;
            ArmyName = name;
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

        public BattleArmy Clone()
        {
            var newStacksList = new List<BattleUnitsStack>();
            _stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
            return new BattleArmy(newStacksList, this.ArmyName);
        }
    }
}
