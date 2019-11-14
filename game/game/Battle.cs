using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using game.BattleArmyClasses;

namespace game
{
    public class Battle
    {
        private BattleArmy FirstBattleArmy;
        private BattleArmy SecondBattleArmy;
        private InitiativeScale Scale;
        private BattleArmy Winner;
        public bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive() || Winner != null;

        private void Attack(BattleUnitsStack attacking, BattleUnitsStack attacked)
        {
            int Damage(BattleUnitsStack attackingBUS, BattleUnitsStack attackedBUS)
            {
                int IncreasedAttack = 0;
                int DecreasedAttack = 0;
                int DecreasedDefence = 0;
                double IsDefend = 1;
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
                    if (effect.Item1 == TypeOfEffect.IsDefends)
                        IsDefend = Config.IS_DEFEND;
                }
                double damage1;
                double damage2;
                double attack = (int)attackingBUS.UnitType.Attack - DecreasedAttack + IncreasedAttack;
                double defence = ((int)attackedBUS.UnitType.Defence - DecreasedDefence) * IsDefend;
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
                if (attackingBUS.UnitType.Damage.Item1 == attackingBUS.UnitType.Damage.Item2)
                    return (int) Math.Floor(damage2);
                Random rnd = new Random();
                return  rnd.Next((int)Math.Ceiling(damage1), (int)Math.Floor(damage2));
            }

            attacked.Hp -= Damage(attacking, attacked);
            if (!attacked.HasRespondThisTurn && attacked.IsAlive)
            {
                attacked.HasRespondThisTurn = true;
                attacking.Hp -= Damage(attacked, attacking);
            }
        }

        private void Wait((BattleUnitsStack, int) stackInScale)
        {
            Scale.WaitScale.Add(stackInScale);
            ComparerOfWaitInitiative comparerOfWaitInitiative = new ComparerOfWaitInitiative();
            Scale.WaitScale.Sort(comparerOfWaitInitiative);
        }

        private void Defend(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((TypeOfEffect.IsDefends, 1));
        }

        private void GiveUp((BattleUnitsStack, int) stackInScale)
        {
            if (stackInScale.Item2 == 2)
                Winner = FirstBattleArmy;
            else
                Winner = SecondBattleArmy;
        }

        private void WhoWin()
        {
            if (Winner == null)
            {
                if (FirstBattleArmy.IsArmyAlive())
                    Winner = FirstBattleArmy;
                else
                    Winner = SecondBattleArmy;
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
            bool flag = false;
            while (!HasBattleEnded)
            {
                Scale.MakeInitiativeScale(FirstBattleArmy, SecondBattleArmy);
                
                while (Scale.Scale.Count > 0)
                {
                    
                    (BattleUnitsStack, int) currentBattleStack = Scale.Scale[0];
                    Console.WriteLine(Scale);
                    string name;
                    if (currentBattleStack.Item2 == 1)
                        name = FirstBattleArmy.ArmyName;
                    else
                        name = SecondBattleArmy.ArmyName;
                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Don't choose it\n[4] Wait\n[5] Give up\n");
                    string action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            Scale.Scale.RemoveAt(0);
                            Console.WriteLine("You chose \"Attack\"");
                            Console.WriteLine("You can now attack following stacks from ");
                            BattleArmy attackedArmy;
                            if (currentBattleStack.Item2 == 1)
                                attackedArmy = SecondBattleArmy;
                            else
                                attackedArmy = FirstBattleArmy;
                            Console.WriteLine(attackedArmy);
                            Console.WriteLine("Enter the index of stack you wanna attack (from 0)");
                            string numberOfStack;
                            while (true)
                            {
                                numberOfStack = Console.ReadLine();
                                if (!int.TryParse(numberOfStack, out int i))
                                    Console.WriteLine("Incorrect input, try again");
                                else
                                {
                                    if (i < 0 || i > attackedArmy.StacksList.Count - 1 )
                                        Console.WriteLine("Incorrect input, try again");
                                    else
                                    {
                                        Attack(currentBattleStack.Item1, attackedArmy.StacksList[i]);
                                        break;
                                    }
                                }
                            }

                            break;
                        case "2":
                            Scale.Scale.RemoveAt(0);
                            Console.WriteLine("You chose \"Defend\"");
                            Defend(currentBattleStack.Item1);
                            break;
                        case "3":
                            //Scale.Scale.RemoveAt(0);
                            break;
                        case "4":
                            Scale.Scale.RemoveAt(0);
                            Console.WriteLine("You chose \"Wait\"");
                            Wait(currentBattleStack);
                            break;
                        case "5":
                            //Scale.Scale.RemoveAt(0);
                            Console.WriteLine("You chose \"Give up\"");
                            GiveUp(currentBattleStack);
                            break;
                        default:
                            Console.WriteLine("Incorrect command, try again");
                            break;

                    }
                    if (HasBattleEnded)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    break;


                foreach (var stack in Scale.WaitScale)
                {


                    if (HasBattleEnded)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    break;

                
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
            }
        }
    }
}
