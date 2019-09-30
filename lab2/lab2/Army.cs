using System.Collections.Generic;

namespace lab2
{
    class Army
    {
        private readonly List<UnitsStack> _stacksList = new List<UnitsStack>();

        public List<UnitsStack> StacksList
        {
            get
            {
                var newStacksList = new List<UnitsStack>();
                _stacksList.ForEach((stack) => newStacksList.Add(stack.Clone()));
                return newStacksList;
            }
        }


        public int Amount => _stacksList.Count;

        public void AppendStack(UnitsStack currentStack)
        {
            if (this.Amount > 5)
            {
                //Console.WriteLine("Too much Unit Stacks");
            }
            else
                _stacksList.Add(currentStack);
        }

        public bool DeleteStack(UnitsStack currentStack)
        {
            if (this._stacksList.Remove(currentStack))
                return true;
            else
                return false;
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
