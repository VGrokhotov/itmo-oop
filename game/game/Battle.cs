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
        private Attacker Attacker;
        public bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive() || Winner != null;


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
            Attacker = new Attacker();
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
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz(Don't choose it)\n[4] Wait(Don't choose it)\n[5] Give up\n");
                    string action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            Scale.Scale.RemoveAt(0);
                            BattleArmy attackedArmy;
                            if (currentBattleStack.Item2 == 1)
                                attackedArmy = SecondBattleArmy;
                            else
                                attackedArmy = FirstBattleArmy;
                            BattleUnitsStack attackedStack = Attacker.attack(currentBattleStack, attackedArmy);
                            Scale.CheckAttackedStack(attackedStack);
                            
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


                while (Scale.WaitScale.Count > 0)
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
            WhoWin();
            Console.WriteLine($"Winner of the battle: {Winner.ArmyName}");
            Console.WriteLine("Losses:");
            Console.WriteLine($"Army {FirstBattleArmy.ArmyName}:\n{FirstBattleArmy.GetLosses()}\n");
            Console.WriteLine($"Army {SecondBattleArmy.ArmyName}:\n{SecondBattleArmy.GetLosses()}\n");

        }
    }
}
