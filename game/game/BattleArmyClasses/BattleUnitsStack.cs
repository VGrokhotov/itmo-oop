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

        public List<(TypeOfMagic, bool)> Magic;

        public int AvailableMagic()
        {
            int answer = 0;
            foreach (var magic in Magic)
            {
                if (magic.Item2)
                    answer++;
            }

            return answer;
        }

        public TypeOfMagic AvailableMagicAt(int index)
        {
            int i = -1;
            foreach (var magic in Magic)
            {
                if (magic.Item2)
                    i++;
                if (i == index)
                    return magic.Item1;
            }

            return TypeOfMagic.PunishingStrike;//надо чето придумать, тут не должно ничего быть
        }
        public BattleUnitsStack(UnitsStack unitsStack)
        {
            UnitType = unitsStack.UnitType.Clone();
            StartAmount = unitsStack.Amount;
            Hp = unitsStack.Amount * (int) (unitsStack.UnitType.HitPoints);
            Effects = new Effects();
            Magic = new List<(TypeOfMagic, bool)>();
            foreach (var magic in unitsStack.UnitType.AccessibleMagic)
            {
                Magic.Add((magic, true));
            }
        }
        
        public override string ToString()
        {
            return ($"Name: {UnitType.Name}, Amount: {Amount}\n");
        }
    }
}
