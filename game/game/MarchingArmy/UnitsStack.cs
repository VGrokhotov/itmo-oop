using System;

namespace game.MarchingArmy
{
    public class UnitsStack
    {
        public Unit UnitType { get; }

        public int Amount { get;}



        public UnitsStack(Unit unitType, int amount)
        {
            
            if (amount > Config.MAX_STACK_NUMBER)
            {
                throw new ArgumentException("Too much Units in one stack");
            }
            this.Amount = amount;
            this.UnitType = unitType;
            
        }

        public override string ToString()
        {
            return ($"Name: {this.UnitType.Name}, Amount: {this.Amount}\n");
        }

        public UnitsStack Clone()
        {
            return new UnitsStack(this.UnitType.Clone(), this.Amount);
        }
    }
}
