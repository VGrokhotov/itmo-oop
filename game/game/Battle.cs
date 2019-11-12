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
            uint IncreasedAttack = 0;
            uint DecreasedAttack = 0;
            uint DecreasedDefence = 0;
            foreach (var effect in attacking.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.DecreasedAttack)
                    DecreasedAttack = Config.DECREASED_ATTACK;
                if (effect.Item1 == TypeOfEffect.IncreasedAttack)
                    DecreasedAttack = Config.INCREASED_ATTACK;
            }
            foreach (var effect in attacked.Effects.AllEffects)
            {
                if (effect.Item1 == TypeOfEffect.DecreasedDefence)
                    DecreasedDefence = Config.DECREASED_DEFENCE;
            }

            double damage1;
            double damage2;

            if (attacking.UnitType.Attack > attacked.UnitType.Defence)
            {
                damage1 = attacking.Amount * attacking.UnitType.Damage.Item1 * ( 1 + 0.05 * (int)(attacking.UnitType.Attack - attacked.UnitType.Defence));
                damage2 = attacking.Amount * attacking.UnitType.Damage.Item2 * (1 + 0.05 * (int)(attacking.UnitType.Attack - attacked.UnitType.Defence));
            }
            else
            {
                damage1 = attacking.Amount * attacking.UnitType.Damage.Item1 / (1 + 0.05 * (int)(attacked.UnitType.Defence - attacking.UnitType.Attack ));
                damage2 = attacking.Amount * attacking.UnitType.Damage.Item2 / (1 + 0.05 * (int)(attacked.UnitType.Defence - attacking.UnitType.Attack));
            }
            Random rnd = new Random();
            int damage = rnd.Next((int)Math.Ceiling(damage1), (int)Math.Floor(damage2));
            attacked.Hp -= damage;
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
                    if (stack.IsAlive)
                    {
                        stack.Effects.DecreaseTurns();
                        stack.Effects.Check();
                    }
                }
                foreach (var stack in SecondBattleArmy.StacksList)
                {
                    if (stack.IsAlive)
                    {
                        stack.Effects.DecreaseTurns();
                        stack.Effects.Check();
                    }
                }
            //}
        }
    }
}
