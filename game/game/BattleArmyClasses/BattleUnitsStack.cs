using System;
using System.Collections.Generic;
using game.MarchingArmy;

namespace game.BattleArmyClasses
{
    public class BattleUnitsStack
    {
        public Unit UnitType { get; }

        public int StartAmount { get; }

        public int Amount => ( Math.Abs((double)Hp / (int)(UnitType.HitPoints) - Hp / (int)(UnitType.HitPoints))  <= double.Epsilon ? Hp / (int)(UnitType.HitPoints) : Hp / (int)(UnitType.HitPoints) + 1);

        public int Hp { get; set; }

        public bool HasRespondThisTurn = false;

        public bool IsAlive => Hp > 0;

        public Effects Effects;

        public void CheckEffectsAtEndOfTern()
        {
            if (IsAlive)
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
            UnitType = unitsStack.UnitType.Clone();
            StartAmount = unitsStack.Amount;
            Hp = unitsStack.Amount * (int) (unitsStack.UnitType.HitPoints);
            Effects = new Effects();
        }

        public BattleUnitsStack Clone()
        {
            return new BattleUnitsStack(new UnitsStack(UnitType.Clone(), Amount));
        }

        public override string ToString()
        {
            return ($"Name: {UnitType.Name}, Amount: {Amount}\n");
        }
    }
}
