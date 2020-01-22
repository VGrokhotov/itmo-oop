using System;
using System.Collections.Generic;
using game.MarchingArmy;

namespace game.BattleArmyClasses
{
    public class BattleUnitsStack
    {
        public Unit UnitType { get; }

        public BattleUnit BattleUnit;

        public int StartAmount { get; }

        public int Amount => ( Math.Abs((double)Hp / (int)(UnitType.HitPoints) - Hp / (int)(UnitType.HitPoints))  <= double.Epsilon ? Hp / (int)(UnitType.HitPoints) : Hp / (int)(UnitType.HitPoints) + 1);

        public int Hp { get; set; }

        public bool HasRespondThisTurn = false;
        public bool EnemyDoesNotRespond = false;
        public bool BeatAll = false;

        public bool IsAlive => Hp > 0;

        public Effects Effects;


        public void CheckEffectsAtEndOfTurn()
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

        public string AvailableMagic()
        {
            string result = "Available magic:\n";
            int index = 1;
            foreach (var magic in this.Magic)
            {
                if (magic.Item2)
                {
                    result += $"[{index}] " + magic.Item1 + "\n";
                    index++;
                }
            }
            return result;
        }

        public int AmountOfAvailableMagic()
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
            int i = 0;
            foreach (var magic in Magic)
            {
                if (magic.Item2)
                    i++;
                if (i == index)
                    return magic.Item1;
            }

            return PunishingStrike.GetInstance();//надо чето придумать, тут не должно ничего быть, но оно сюда и не приходит по логике вызыва
        }

        public void BunToWiz(TypeOfMagic chosenMagic)
        {
            for (int i = 0; i < Magic.Count; i++)
            {
                if (Magic[i].Item1 == chosenMagic)
                {
                    Magic[i] = (chosenMagic, false);
                }
            }
        }
        public BattleUnitsStack(UnitsStack unitsStack)
        {
            UnitType = unitsStack.UnitType.Clone();
            BattleUnit = new BattleUnit(unitsStack.UnitType);
            StartAmount = unitsStack.Amount;
            Hp = unitsStack.Amount * (int) (unitsStack.UnitType.HitPoints);
            Effects = new Effects(this);
            Magic = new List<(TypeOfMagic, bool)>();
            foreach (var magic in unitsStack.UnitType.AccessibleMagic)
            {
                Magic.Add((magic, true));
            }
            foreach (var effect in unitsStack.UnitType.CongenitalEffects)
            {
                Effects.Add((effect, -1));
            }
        }
        
        public override string ToString()
        {
            return ($"Name: {UnitType.Name}, Amount: {Amount}\n");
        }
    }
}
