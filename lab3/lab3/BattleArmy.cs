using System;
using System.Collections.Generic;
using lab2;

namespace lab3
{
    public class BattleArmy
    {
        private readonly List<BattleUnitsStack> _stacksList;

        public string ArmyName{ get; }

        public List<BattleUnitsStack> StacksList
        {
            get
            {
                //var newStacksList = new List<BattleUnitsStack>();
                //_stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
                //return newStacksList;
                return _stacksList;
            }
        }

        private void AddStack(BattleUnitsStack battleUnitsStack)
        {
            if (_stacksList.Count < Config.MAX_ARMY_NUMBER)
            
                _stacksList.Add(battleUnitsStack);
            else 
                throw new ArgumentException("To much BattleStacks");
        }

        public bool IsArmyAlive()
        {
            foreach (var stack in StacksList)
            {
                if (stack.IsAlive)
                    return true;
            }
            return false;
        }

        public BattleArmy(List<BattleUnitsStack> stacksList)
        {
            if (stacksList.Count > Config.MAX_ARMY_NUMBER)
            {
                throw new ArgumentException("To much Stacks");
            }
            var newStacksList = new List<BattleUnitsStack>();
            stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
            this._stacksList = newStacksList;
            Console.WriteLine("Enter Army name");
            ArmyName = Console.ReadLine();
        }
        public override string ToString()
        {
            string result = "Army:\n";
            foreach (var stack in _stacksList)
            {
                result += stack.ToString();
            }
            return result;
        }

        public BattleArmy Clone()
        {
            return new BattleArmy(this.StacksList);
        }
    }
}
