using System;
using System.Collections.Generic;
using System.Linq;
using game.BattleArmyClasses;

namespace game
{
    public class Battle
    {
        private BattleArmy FirstBattleArmy;
        private BattleArmy SecondBattleArmy;
        private InitiativeScale Scale;
        public bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive();

        private void Attack(BattleUnitsStack attacking, BattleUnitsStack attacked)
        {
            int Damage(BattleUnitsStack attackingBUS, BattleUnitsStack attackedBUS)
            {
                int IncreasedAttack = 0;
                int DecreasedAttack = 0;
                int DecreasedDefence = 0;
                foreach (var effect in attackingBUS.Effects.AllEffects)
                {
                    if (effect.Item1 == TypeOfEffect.DecreasedAttack)
                        DecreasedAttack = (int)Config.DECREASED_ATTACK;
                    if (effect.Item1 == TypeOfEffect.IncreasedAttack)
                        DecreasedAttack = (int)Config.INCREASED_ATTACK;
                }
                foreach (var effect in attackedBUS.Effects.AllEffects)
                {
                    if (effect.Item1 == TypeOfEffect.DecreasedDefence)
                        DecreasedDefence = (int)Config.DECREASED_DEFENCE;
                }
                double damage1;
                double damage2;
                int attack = (int)attackingBUS.UnitType.Attack - DecreasedAttack + IncreasedAttack;
                int defence = (int)attackedBUS.UnitType.Defence - DecreasedDefence;
                if (attack > defence)
                {
                    damage1 = attackingBUS.Amount * (int)attackingBUS.UnitType.Damage.Item1 * (1 + 0.05 * (attack - defence));
                    damage2 = attackingBUS.Amount * (int)attackingBUS.UnitType.Damage.Item2 * (1 + 0.05 * (attack - defence));
                }
                else
                {
                    damage1 = attackingBUS.Amount * (int)attackingBUS.UnitType.Damage.Item1 / (1 + 0.05 * (defence - attack));
                    damage2 = attackingBUS.Amount * (int)attackingBUS.UnitType.Damage.Item2 / (1 + 0.05 * (defence - attack));
                }
                Random rnd = new Random();
                return  rnd.Next((int)Math.Ceiling(damage1), (int)Math.Floor(damage2));
            }

            attacked.Hp -= Damage(attacking, attacked);
            if (!attacked.HasRespondThisTurn)
            {
                attacked.HasRespondThisTurn = true;
                attacking.Hp -= Damage(attacked, attacking);
            }
        }

        private void Wait((BattleUnitsStack, int) stackInScale)
        {
            Scale.WaitScale.Add(stackInScale);
            Scale.WaitScale.Sort();
        }

        private BattleArmy WhoWin()
        { 
            if (FirstBattleArmy.IsArmyAlive())
            {
                return FirstBattleArmy;
            }
            else
            {
                return SecondBattleArmy;
            }
        }

        public Battle(BattleArmy firstBattleArmy, BattleArmy secondBattleArmy)
        {
            this.FirstBattleArmy = firstBattleArmy.Clone();
            this.SecondBattleArmy = secondBattleArmy.Clone();
            Scale = new InitiativeScale();
        }

        public void StartBattle()
        {
            //while (!HasBattleEnded)
            //{
                Scale.MakeInitiativeScale(FirstBattleArmy, SecondBattleArmy);
                foreach (var stack in Scale.Scale )
                {
                    
                }
                foreach (var stack in Scale.WaitScale)
                {

                }

                foreach (var stack in FirstBattleArmy.StacksList)
                {
                    stack.CheckEffectsAtEndOfTern();
                    stack.HasRespondThisTurn = false;
                }
                foreach (var stack in SecondBattleArmy.StacksList)
                {
                    stack.CheckEffectsAtEndOfTern();
                    stack.HasRespondThisTurn = false;
            }
            //}
        }
    }
}
