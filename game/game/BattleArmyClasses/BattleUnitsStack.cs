using System.Collections.Generic;
using game.MarchingArmy;

namespace game.BattleArmyClasses
{
    public class BattleUnitsStack
    {
        public Unit UnitType { get; }

        public int StartAmount { get; }

        public int Amount => ( this.Hp / (int)(this.UnitType.HitPoints));

        public int Hp { get; set; }

        public bool HasRespondThisTurn = false;

        public bool IsAlive => this.Hp > 0;

        public Effects Effects;

        public void CheckEffectsAtEndOfTern()
        {
            if (this.IsAlive)
            {
                Effects.DecreaseTurns();
            }
            else
            {
                Effects.Clear();
            }
        }
        public BattleUnitsStack(UnitsStack unitsStack)
        {
            this.UnitType = unitsStack.UnitType.Clone();
            this.StartAmount = unitsStack.Amount;
            this.Hp = unitsStack.Amount * (int) (unitsStack.UnitType.HitPoints);
            this.Effects = new Effects();
        }

        public BattleUnitsStack Clone()
        {
            return new BattleUnitsStack(new UnitsStack(this.UnitType.Clone(), this.Amount));
        }

        public override string ToString()
        {
            return ($"Name: {this.UnitType.Name}, Amount: {this.Amount}\n");
        }
    }
}
