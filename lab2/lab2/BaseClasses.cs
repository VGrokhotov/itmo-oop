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

        public Unit Clone()
        {
            return new Unit(this.Type, this.Name, this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
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

        public UnitsStack Clone()
        {
            return new UnitsStack(this.UnitType.Clone(), this.Amount);
        }
    }

    class Army
    {
        private List<UnitsStack> stacksList = new List<UnitsStack>();

        public List<UnitsStack> StacksList
        {
            get
            {
                var newStacksList = new List<UnitsStack>();
                stacksList.ForEach((item) => newStacksList.Add(item.Clone()));
                return newStacksList;
            }
        }


        public int Amount => stacksList.Count;

        public void AppendStack(UnitsStack currentStack)
        {
            if (this.Amount > 5)
            {
                //Console.WriteLine("Too much Unit Stacks");
            }
            else
                stacksList.Add(currentStack);
        }

        public bool DeleteStack(UnitsStack currentStack)
        {
            if (this.stacksList.Remove(currentStack))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string result = "Army:\n";
            foreach (var stack in stacksList)
            {
                result += stack.ToString();
            }
            return result;
        }
    }
}
