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
                reason = $"{name} has given up";
            }
        }



        public Battle(Army firstArmy, string firstArmyName,  Army secondArmy, string secondArmyName)
        {
            this.FirstBattleArmy = new BattleArmy(firstArmy, firstArmyName);
            this.SecondBattleArmy = new BattleArmy(secondArmy, secondArmyName);
            Scale = new InitiativeScale(FirstBattleArmy.ArmyName, SecondBattleArmy.ArmyName);
            Attacker = new Attacker();
            Wizard = new Wizard();
        }

        public void StartBattle()
        {
            Console.WriteLine($"Welcome to battle, {FirstBattleArmy.ArmyName} and {SecondBattleArmy.ArmyName}.");
            Console.WriteLine("Let explain you rules of the battle:");
            Console.WriteLine("[1] Every unit can attack any unit from enemy army ones a turn");
            Console.WriteLine("[2] Some units can wiz");
            Console.WriteLine("[3] Every unit can do nothing during its turn and just defend");
            Console.WriteLine("[4] The turn of moves is built on the units' initiative");
            Console.WriteLine("[5] Once a turn unit can wait");
            Console.WriteLine("[6] You can look trough your and enemy army on your unit turn");
            Console.WriteLine("[7] Also, any player can give up on his or her unit turn");
            Console.WriteLine("Now we can start. Press Enter to continue...");
            Console.ReadLine();

            bool flag = false;
            while (!HasBattleEnded)
            {
                Console.Clear();
                Scale.MakeInitiativeScale(FirstBattleArmy, SecondBattleArmy);
                
                while (Scale.Scale.Count > 0)
                {
                    
                    (BattleUnitsStack, TypeOfArmy) currentBattleStack = Scale.Scale[0];
                    Console.Clear();
                    Console.WriteLine(Scale);
                    string name = currentBattleStack.Item2 == TypeOfArmy.First ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;
                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army of {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz\n[4] Wait\n[5] Give up\n[6] Show armies\n");
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
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"\n\n");
                                Defend(currentBattleStack.Item1);
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "3":
                                Console.WriteLine("You chose \"Wiz\"\n");
                                HasActionChosen = Wizard.Wiz(currentBattleStack, FirstBattleArmy, SecondBattleArmy, Scale.Scale);
                                Scale.SortScales();
                                if (!HasActionChosen)
                                    action = Console.ReadLine();
                                else
                                {
                                    Console.WriteLine("Press Enter to continue...");
                                    Console.ReadLine();
                                }
                                break;
                            case "4":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Wait\"\n\n");
                                Wait(currentBattleStack);
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "5":
                                HasActionChosen = true;
                                //Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Give up\"\n");
                                GiveUp(currentBattleStack);
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "6":
                                HasActionChosen = true;
                                Console.WriteLine("You chose \"Show armies\"\n");
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine(FirstBattleArmy.AliveStacks()+"\n");
                                Console.WriteLine(SecondBattleArmy.AliveStacks());
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
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
                    Console.Clear();
                    Console.WriteLine(Scale);

                    string name = currentBattleStack.Item2 == TypeOfArmy.First ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;

                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz\n[4] Give up\n[5] Show armies\n");
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
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.WaitScale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"\n\n");
                                Defend(currentBattleStack.Item1);
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "3":
                                Console.WriteLine("You chose \"Wiz\"\n");
                                HasActionChosen = Wizard.Wiz(currentBattleStack, FirstBattleArmy, SecondBattleArmy, Scale.WaitScale);
                                Scale.SortScales();
                                if (!HasActionChosen)
                                    action = Console.ReadLine();
                                else
                                {
                                    Console.WriteLine("Press Enter to continue...");
                                    Console.ReadLine();
                                }
                                break;
                            case "4":
                                HasActionChosen = true;
                                //Scale.WaitScale.RemoveAt(0);
                                Console.WriteLine("You chose \"Give up\"\n");
                                GiveUp(currentBattleStack);
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                break;
                            case "5":
                                HasActionChosen = true;
                                Console.WriteLine("You chose \"Show armies\"\n");
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine(FirstBattleArmy.AliveStacks() + "\n");
                                Console.WriteLine(SecondBattleArmy.AliveStacks());
                                Console.WriteLine("Press Enter to continue...");
                                Console.ReadLine();
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
            Console.Clear();
            Console.WriteLine($"Winner of the battle: {Winner.ArmyName}");
            Console.WriteLine(reason);
            Console.WriteLine("Losses:");
            Console.WriteLine($"Army of {FirstBattleArmy.ArmyName}:\n{FirstBattleArmy.GetLosses()}\n");
            Console.WriteLine($"Army of {SecondBattleArmy.ArmyName}:\n{SecondBattleArmy.GetLosses()}\n");

        }
    }

    public enum TypeOfArmy
    {
        First,
        Second
    }
}
