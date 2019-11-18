using System;
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
            Winner = stackInScale.Item2 == 2 ? FirstBattleArmy : SecondBattleArmy;
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
                    
                    (BattleUnitsStack, int) currentBattleStack = Scale.Scale[0];
                    Console.WriteLine(Scale);
                    string name = currentBattleStack.Item2 == 1 ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;
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
                                    currentBattleStack.Item2 == 1 ? SecondBattleArmy : FirstBattleArmy;
                                BattleUnitsStack attackedStack = Attacker.Attack(currentBattleStack, attackedArmy);
                                Scale.CheckAttackedStack(attackedStack);

                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.Scale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"");
                                Defend(currentBattleStack.Item1);
                                break;
                            case "3":
                                Console.WriteLine("You chose \"Wiz\"");
                                if (currentBattleStack.Item1.Magic.Count > 0)
                                {
                                    Console.WriteLine("Available magic:");
                                    foreach (var magic in currentBattleStack.Item1.Magic)
                                    {
                                        if (magic.Item2)
                                        {
                                            Console.WriteLine(magic.Item1);
                                        }
                                    }
                                    Console.WriteLine("Enter the index of magic you wanna cast(from 0) or \"-1\" to choose another action");
                                    while (true)
                                    {
                                        var indexOfMagic = Console.ReadLine();
                                        if (!int.TryParse(indexOfMagic, out int i))
                                            Console.WriteLine("Incorrect input, try again");
                                        else
                                        {
                                            if (i < -1 || i > currentBattleStack.Item1.AvailableMagic() - 1)
                                                Console.WriteLine("Incorrect input, try again");
                                            else
                                            {
                                                if (i == -1)
                                                {
                                                    Console.WriteLine("You decided not to wiz, choose another action");
                                                    action = Console.ReadLine();
                                                }
                                                else
                                                {
                                                    HasActionChosen = true;
                                                    Scale.Scale.RemoveAt(0);
                                                    TypeOfMagic chosenMagic = currentBattleStack.Item1.AvailableMagicAt(i);
                                                    Console.WriteLine($"You chose {chosenMagic}");
                                                    //to do
                                                    //вызов волшебства
                                                    //перенести это все в wait scale
                                                    BattleArmy toWhatArmyUseMagic;
                                                    if (chosenMagic == TypeOfMagic.Curse || chosenMagic == TypeOfMagic.Attenuation)
                                                        toWhatArmyUseMagic = currentBattleStack.Item2 == 1 ? SecondBattleArmy : FirstBattleArmy;
                                                    else
                                                        toWhatArmyUseMagic = currentBattleStack.Item2 == 1 ? FirstBattleArmy : SecondBattleArmy;
                                                    Console.WriteLine($"You can now use {chosenMagic} to following stacks from ");
                                                    Console.WriteLine(toWhatArmyUseMagic.AliveStacks());
                                                    Console.WriteLine("Enter the index of stack you wanna attack (from 0)");
                                                    while (true)
                                                    {
                                                        var numberOfStack = Console.ReadLine();
                                                        if (!int.TryParse(numberOfStack, out int j))
                                                            Console.WriteLine("Incorrect input, try again");
                                                        else
                                                        {
                                                            if (j < 0 || j > toWhatArmyUseMagic.AmountOfAliveStacks() - 1)
                                                                Console.WriteLine("Incorrect input, try again");
                                                            else
                                                            {
                                                                BattleUnitsStack toWhatStackUseMagic = toWhatArmyUseMagic.AliveStackAt(j);
                                                                switch (chosenMagic)
                                                                {
                                                                    case TypeOfMagic.Resurrection:
                                                                        Wizard.Resurrection(toWhatStackUseMagic, currentBattleStack.Item1);
                                                                        break;
                                                                    case TypeOfMagic.Acceleration:
                                                                        Wizard.Acceleration(toWhatStackUseMagic);
                                                                        break;
                                                                    case TypeOfMagic.Attenuation:
                                                                        Wizard.Attenuation(toWhatStackUseMagic);
                                                                        break;
                                                                    case TypeOfMagic.PunishingStrike:
                                                                        Wizard.PunishingStrike(toWhatStackUseMagic);
                                                                        break;
                                                                    case TypeOfMagic.Curse:
                                                                        Wizard.Curse(toWhatStackUseMagic);
                                                                        break;
                                                                }
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("This stack can not wiz, choose another action");
                                    action = Console.ReadLine();
                                }

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
                    (BattleUnitsStack, int) currentBattleStack = Scale.WaitScale[0];
                    Console.WriteLine(Scale);

                    string name = currentBattleStack.Item2 == 1 ? FirstBattleArmy.ArmyName : SecondBattleArmy.ArmyName;

                    Console.WriteLine($"Now is turn of: {currentBattleStack.Item1} from Army {name}\n");
                    Console.WriteLine("Choose one of possible actions:\n[1] Attack\n[2] Defend\n[3] Wiz(Don't choose it)\n[4] Give up\n");
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
                                    currentBattleStack.Item2 == 1 ? SecondBattleArmy : FirstBattleArmy;
                                BattleUnitsStack attackedStack = Attacker.Attack(currentBattleStack, attackedArmy);
                                Scale.CheckAttackedStack(attackedStack);

                                break;
                            case "2":
                                HasActionChosen = true;
                                Scale.WaitScale.RemoveAt(0);
                                Console.WriteLine("You chose \"Defend\"");
                                Defend(currentBattleStack.Item1);
                                break;
                            case "3":
                                //Scale.WaitScale.RemoveAt(0);
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
            Console.WriteLine(reason);
            Console.WriteLine("Losses:");
            Console.WriteLine($"Army {FirstBattleArmy.ArmyName}:\n{FirstBattleArmy.GetLosses()}\n");
            Console.WriteLine($"Army {SecondBattleArmy.ArmyName}:\n{SecondBattleArmy.GetLosses()}\n");

        }
    }
}
