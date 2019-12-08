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

        public void ReturnFeatures()
        {
            foreach (var effect in AllEffects)
            {
                if (effect.Item2 == -1)
                {
                    effect.Item1.Modify(CurrentBattleUnitsStack);
                }
            }
        }

        public void DecreaseTurns()
        {
            List <(TypeOfEffect, int)> temp = new List<(TypeOfEffect, int)>();
            foreach (var effect in AllEffects)
            {
                if (effect.Item2 > 1 )
                    temp.Add((effect.Item1, effect.Item2 - 1));
                else
                {
                    if (effect.Item2 == -1)
                        temp.Add((effect.Item1, effect.Item2));
                    else 
                        effect.Item1.Unmodify(CurrentBattleUnitsStack);
                }
                    

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
    

    public abstract class TypeOfEffect
    {
        public abstract void Modify(BattleUnitsStack currentBattleUnitsStack);
        public abstract void Unmodify(BattleUnitsStack currentBattleUnitsStack);
    }

    public class IncreasedInitiative : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative *= 1.4;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative /= 1.4;
        }
    }

    public class IncreasedAttack : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack += 12;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack -= 12;
        }
    }

    public class DecreasedAttack : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack -= 12;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenAttack += 12;
        }
    }

    public class DecreasedDefence : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenDefence -= 12;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.greenDefence += 12;
        }
    }

    public class IsDefends : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteDefence = (int)Math.Floor(currentBattleUnitsStack.BattleUnit.whiteDefence * 1.3);
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteDefence = (int)Math.Ceiling(currentBattleUnitsStack.BattleUnit.whiteDefence / 1.3);
        }
    }

    public class Archer : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.EnemyDoesNotRespond = true;
            currentBattleUnitsStack.HasRespondThisTurn = true;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack) {}
    }

    public class EnemyDoesNotRespond : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.EnemyDoesNotRespond = true;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack) { }
    }

    public class EndlessRebuff : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.HasRespondThisTurn = false;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack) { }
    }
    
}
