using System.Collections.Generic;
using lab2;

namespace lab3
{
    public class BattleUnitsStack
    {
        public Unit UnitType { get; }

        public int StartAmount { get; }

        public int Amount => ( this.Hp / (int)(this.UnitType.HitPoints));

        public int Hp { get; }

        public bool IsAlive => this.Hp > 0;

        public List<Effects> Effects;
        public BattleUnitsStack(UnitsStack unitsStack)
        {
            this.UnitType = unitsStack.UnitType.Clone();
            this.StartAmount = unitsStack.Amount;
            this.Hp = unitsStack.Amount * (int) (unitsStack.UnitType.HitPoints);
        }

        public BattleUnitsStack Clone()
        {
            return new BattleUnitsStack(new UnitsStack(this.UnitType.Clone(), this.Amount));
        }

    }
}
