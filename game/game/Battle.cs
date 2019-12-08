using System;
using System.Collections.Generic;
using game.BattleArmyClasses;
using game.MarchingArmy;

namespace game
{
    public class Battle
    {
        private BattleArmy FirstBattleArmy;
        private BattleArmy SecondBattleArmy;
        private InitiativeScale Scale;
        private BattleArmy Winner;
        private Attacker Attacker;
        private Wizard Wizard;
        private string reason;
        private bool HasBattleEnded => !FirstBattleArmy.IsArmyAlive() || !SecondBattleArmy.IsArmyAlive() || Winner != null;


        private void Wait((BattleUnitsStack, TypeOfArmy) stackInScale)
        {
            Scale.WaitScale.Add(stackInScale);
            ComparerOfWaitInitiative comparerOfWaitInitiative = new ComparerOfWaitInitiative();
            Scale.WaitScale.Sort(comparerOfWaitInitiative);
        }

        private void Defend(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((new IsDefends(), 1));
        }

        private void GiveUp((BattleUnitsStack, TypeOfArmy) stackInScale)
        {
            Winner = stackInScale.Item2 == TypeOfArmy.Second ? FirstBattleArmy : SecondBattleArmy;
        }

        private void WhoWin()
        {
            if (Winner == null)
            {
                Winner = FirstBattleArmy.IsArmyAlive() ? FirstBattleArmy : SecondBattleArmy;
                string name = FirstBattleArmy.IsArmyAlive() ? SecondBattleArmy.ArmyName : FirstBattleArmy.ArmyName;
                reason = $"There is no alive stacks in army {name}";
            }
            else
            {
                string name = Winner.ArmyName == FirstBattleArmy.ArmyName ? SecondBattleArmy.ArmyName : FirstBattleArmy.ArmyName;
                reason = $"Army {name} has given up";
            }
        }



        public Battle(Army firstArmy, string firstArmyName,  Army secondArmy, string secondArmyName)
        {
            this.FirstBattleArmy = new BattleArmy(firstArmy, firstArmyName);
            this.SecondBattleArmy = new BattleArmy(secondArmy, secondArmyName);
            Scale = new InitiativeScale();
            Attacker = new Attacker();
            Wizard = new Wizard();
        }

        public void StartBattle()
        {
            bool flag = false;
            while (!HasBattleEnded)
            {
                Scale.MakeInitiativeScale(FirstBattleArmy, SecondBattleArmy);
                
                while (Scale.Scale.Count > 0)
                {
                    
                    (BattleUnitsStack, TypeOfArmy) currentBattleStack = Scale.Scale[0];
                    Console.WriteLine(Scale);
                    string name = currentBattleStack.Item2 == TypeOfArmy.First ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;
                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz\n[4] Wait\n[5] Give up\n");
                    string action = Console.ReadLine();
                    bool HasActionChosen = false;
                    while (!HasActionChosen)
                    {
                        switch (action)
                        {
                            case "1":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                BattleArmy attackedArmy =
                                    currentBattleStack.Item2 == TypeOfArmy.First ? SecondBattleArmy : FirstBattleArmy;
                                List<BattleUnitsStack> attackedStacks = Attacker.Attack(currentBattleStack, attackedArmy);
                                foreach (var stack in attackedStacks)
                                {
                                    stack.Effects.ReturnFeatures();
                                    Scale.CheckAttackedStack(stack);
                                }

                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"");
                                Defend(currentBattleStack.Item1);
                                break;
                            case "3":
                                Console.WriteLine("You chose \"Wiz\"");
                                HasActionChosen = Wizard.Wiz(currentBattleStack, FirstBattleArmy, SecondBattleArmy, Scale.Scale);
                                Scale.SortScales();
                                if (!HasActionChosen)
                                    action = Console.ReadLine();
                                break;
                            case "4":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Wait\"");
                                Wait(currentBattleStack);
                                break;
                            case "5":
                                HasActionChosen = true;
                                //Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Give up\"");
                                GiveUp(currentBattleStack);
                                break;
                            default:
                                Console.WriteLine("Incorrect command, try again");
                                action = Console.ReadLine();
                                break;

                        }
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
                    (BattleUnitsStack, TypeOfArmy) currentBattleStack = Scale.WaitScale[0];
                    Console.WriteLine(Scale);

                    string name = currentBattleStack.Item2 == TypeOfArmy.First ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;

                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz\n[4] Give up\n");
                    string action = Console.ReadLine();
                    bool HasActionChosen = false;
                    while (!HasActionChosen)
                    {
                        switch (action)
                        {
                            case "1":
                                HasActionChosen = true;
                                Scale.WaitScale.RemoveAt(0);
                                BattleArmy attackedArmy =
                                    currentBattleStack.Item2 == TypeOfArmy.First ? SecondBattleArmy : FirstBattleArmy;
                                List<BattleUnitsStack> attackedStacks = Attacker.Attack(currentBattleStack, attackedArmy);
                                foreach (var stack in attackedStacks)
                                {
                                    stack.Effects.ReturnFeatures();
                                    Scale.CheckAttackedStack(stack);
                                }

                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.WaitScale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"");
                                Defend(currentBattleStack.Item1);
                                break;
                            case "3":
                                Console.WriteLine("You chose \"Wiz\"");
                                HasActionChosen = Wizard.Wiz(currentBattleStack, FirstBattleArmy, SecondBattleArmy, Scale.WaitScale);
                                Scale.SortScales();
                                if (!HasActionChosen)
                                    action = Console.ReadLine();
                                break;
                            case "4":
                                HasActionChosen = true;
                                //Scale.WaitScale.RemoveAt(0);
                                Console.WriteLine("You chose \"Give up\"");
                                GiveUp(currentBattleStack);
                                break;
                            default:
                                Console.WriteLine("Incorrect command, try again");
                                action = Console.ReadLine();
                                break;

                        }
                    }

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
                    stack.HasRespondThisTurn = false;
                    stack.CheckEffectsAtEndOfTurn();
                }
                foreach (var stack in SecondBattleArmy.StacksList)
                {
                    stack.HasRespondThisTurn = false;
                    stack.CheckEffectsAtEndOfTurn();
                }
            }
            WhoWin();
            Console.WriteLine($"Winner of the battle: {Winner.ArmyName}");
            Console.WriteLine(reason);
            Console.WriteLine("Losses:");
            Console.WriteLine($"Army {FirstBattleArmy.ArmyName}:\n{FirstBattleArmy.GetLosses()}\n");
            Console.WriteLine($"Army {SecondBattleArmy.ArmyName}:\n{SecondBattleArmy.GetLosses()}\n");

        }
    }

    public enum TypeOfArmy
    {
        First,
        Second
    }
}
