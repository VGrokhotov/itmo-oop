using System.Collections.Generic;

namespace lab2
{
    
    class Unit
    {
        public uint Type { get; }
        public string Name { get; }
        public int HitPoints { get; }
        public int Attack { get; }
        public int Defence { get; }
        public (int, int) Damage { get; }
        public double Initiative { get; }

        public Unit(uint type, string name, int hitPoints, int attack, int defence, (int, int) damage,
            double initiative)
        {
            this.Type = type;
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage;
            this.Initiative = initiative;
        }
    }

    class UnitsStack
    {
        public Unit UnitType { get; }

        public int Amount { get; set; }

        public bool IsStackAlive
        {
            get
            {
                if (Amount > 0)
                    return true;
                else
                    return false;
            }
        }


        public UnitsStack(Unit unitType, int amount)
        {
            this.Amount = amount;
            this.UnitType = unitType;
        }

        public override string ToString()
        {
            return ($"{this.UnitType.Name}: {this.Amount}\n");
        }
    }

    class Army
    {
        public List<UnitsStack> StacksList { get; } = new List<UnitsStack>();

        public int Amount => StacksList.Count;

        public void AppendStack(UnitsStack currentStack)
        {
            if (this.Amount > 5)
            {
                //Console.WriteLine("Too much Unit Stacks");
            }
            else
                StacksList.Add(currentStack);
        }

        public bool DeleteStack(UnitsStack currentStack)
        {
            if (this.StacksList.Remove(currentStack))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string result = "Army: ";
            foreach (var stack in StacksList)
            {
                result += stack.ToString();
            }
            return result;
        }
    }
}
