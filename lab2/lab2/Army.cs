using System.Collections.Generic;

namespace lab2
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
            this._stacksList = stacksList;
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
    }
}
