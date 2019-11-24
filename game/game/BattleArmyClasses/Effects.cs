using System.Collections.Generic;


namespace game.BattleArmyClasses
{
    public class Effects {
        public List<(TypeOfEffect, int)> AllEffects;

        public Effects()
        {
            AllEffects = new List<(TypeOfEffect, int)>();
        }

        public void Add((TypeOfEffect, int) effect)
        {
            AllEffects.Add(effect);
        }

        public void DecreaseTurns()
        {
            List <(TypeOfEffect, int)> temp = new List<(TypeOfEffect, int)>();
            foreach (var effect in AllEffects)
            {
                if (effect.Item2 > 1)
                    temp.Add((effect.Item1, effect.Item2 - 1));
                if (effect.Item2 == -1 )
                    temp.Add((effect.Item1, effect.Item2));
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
    public enum TypeOfEffect
        {
            DecreasedInitiative,
            IncreasedInitiative,
            IncreasedAttack,
            DecreasedAttack,
            DecreasedDefence,
            IsDefends,

            Archer,
            AccurateShot,
            EnemyDoesNotRespond,
            BeatAll,
            EndlessRebuff
    }
}
