using System;
using System.Collections.Generic;


namespace game.BattleArmyClasses
{
    public class Effects
    {
        public BattleUnitsStack CurrentBattleUnitsStack;

        public List<(TypeOfEffect, int)> AllEffects;

        public Effects(BattleUnitsStack currentBattleUnitsStack)
        {
            AllEffects = new List<(TypeOfEffect, int)>();
            CurrentBattleUnitsStack = currentBattleUnitsStack;
        }

        public void Add((TypeOfEffect, int) effect)
        {
            AllEffects.Add(effect);
            effect.Item1.Modify(CurrentBattleUnitsStack);
        }

        public void DecreaseTurns()
        {
            List <(TypeOfEffect, int)> temp = new List<(TypeOfEffect, int)>();
            foreach (var effect in AllEffects)
            {
                if (effect.Item2 > 1)
                    temp.Add((effect.Item1, effect.Item2 - 1));
                else
                    effect.Item1.Unmodify(CurrentBattleUnitsStack);

            }
            AllEffects.Clear();
            AllEffects = temp;
        }

        public bool IsEffectApplied(TypeOfEffect Effect)
        {
            foreach (var effect in AllEffects)
            {
                if (effect.Item1 == Effect)
                    return true;
            }

            return false;
        }

        public void Clear()
        {
            if (this.AllEffects.Count != 0)
                this.AllEffects.Clear();
        }

    }
    /*public enum TypeOfEffect
        {
            DecreasedInitiative,
            IncreasedInitiative,+
            IncreasedAttack,+
            DecreasedAttack,+
            DecreasedDefence,+
            IsDefends,+

            Archer,
            AccurateShot,
            EnemyDoesNotRespond,
            BeatAll,
            EndlessRebuff
    }*/

    public interface TypeOfEffect
    {
        void Modify(BattleUnitsStack currentBattleUnitsStack);
        void Unmodify(BattleUnitsStack currentBattleUnitsStack);
    }

    public class IncreasedInitiative : TypeOfEffect
    {
        public void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative *= 1.4;
        }

        public void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative /= 1.4;
        }
    }

    public class IncreasedAttack : TypeOfEffect
    {
        public void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack += 12;
        }

        public void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack -= 12;
        }
    }

    public class DecreasedAttack : TypeOfEffect
    {
        public void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack -= 12;
        }

        public void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack += 12;
        }
    }

    public class DecreasedDefence : TypeOfEffect
    {
        public void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenDefence -= 12;
        }

        public void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenDefence += 12;
        }
    }

    public class IsDefends : TypeOfEffect
    {
        public void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteDefence = (int)Math.Floor(currentBattleUnitsStack.BattleUnit.whiteDefence * 1.3);
        }

        public void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteDefence = (int)Math.Ceiling(currentBattleUnitsStack.BattleUnit.whiteDefence / 1.3);
        }
    }
}
