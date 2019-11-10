using System;
using System.Collections.Generic;

namespace game.MarchingArmy
{
    public class Army
    {
        private readonly List<UnitsStack> _stacksList;

        public List<UnitsStack> StacksList
        {
            get
            {
                var newStacksList = new List<UnitsStack>();
                _stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
                return newStacksList;
            }
        }

        //public int Amount => _stacksList.Count;

        public Army(List<UnitsStack> stacksList)
        {
            if (stacksList.Count > Config.MAX_ARMY_NUMBER)
            {
                throw new ArgumentException("To much Stacks");
            }
            var newStacksList = new List<UnitsStack>();
            stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
            this._stacksList = newStacksList;
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

        public Army Clone()
        {
            return new Army(this.StacksList);
        }
    }
}
