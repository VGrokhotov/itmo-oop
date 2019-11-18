using System;
using System.Collections.Generic;
using game.BattleArmyClasses;

namespace game
{
    public class Wizard
    {
        public bool Wiz((BattleUnitsStack, int) currentBattleStack, BattleArmy FirstBattleArmy, BattleArmy SecondBattleArmy, List<(BattleUnitsStack, int)> Scale)
        {
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
                                return false;
                            }
                            else
                            {
                                
                                Scale.RemoveAt(0);
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
                                                    Resurrection(toWhatStackUseMagic, currentBattleStack.Item1);
                                                    break;
                                                case TypeOfMagic.Acceleration:
                                                    Acceleration(toWhatStackUseMagic);
                                                    break;
                                                case TypeOfMagic.Attenuation: 
                                                    Attenuation(toWhatStackUseMagic);
                                                    break;
                                                case TypeOfMagic.PunishingStrike:
                                                    PunishingStrike(toWhatStackUseMagic);
                                                    break;
                                                case TypeOfMagic.Curse:
                                                    Curse(toWhatStackUseMagic);
                                                    break;
                                            }
                                            break;
                                        }
                                    }
                                }

                                return true;
                            }
                            break;
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("This stack can not wiz, choose another action");
                return false;
            }
        }
        public void PunishingStrike(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((TypeOfEffect.IncreasedAttack, 3));
        }
        public void  Curse(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((TypeOfEffect.DecreasedAttack, 3));
        }
        public void Attenuation(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((TypeOfEffect.DecreasedDefence, 5));
        }
        public void Acceleration(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.Effects.Add((TypeOfEffect.IncreasedInitiative, 3));
        }
        public void Resurrection(BattleUnitsStack currentBattleUnitsStack, BattleUnitsStack whoHeal)
        {
            int healing =  whoHeal.Amount;
            switch (whoHeal.UnitType.Name)
            {
                case "Angel":
                    healing *= Config.ANGEL_HEALING;
                    break;
                case "Lich":
                    healing *= Config.LICH_HEALING;
                    break;
                default:
                    healing *= Config.DEFAULT_HEALING;
                    break;
            }

            int amountBeforeHealing;
            int startHp = currentBattleUnitsStack.StartAmount * (int)currentBattleUnitsStack.UnitType.HitPoints;
            if (currentBattleUnitsStack.Hp < 0)
            {
                currentBattleUnitsStack.Hp = 0;
                amountBeforeHealing = 0;
            }
            else
            {
                amountBeforeHealing = currentBattleUnitsStack.Amount;
            }

            if (healing + currentBattleUnitsStack.Hp >= startHp)
                currentBattleUnitsStack.Hp = startHp;
            else
                currentBattleUnitsStack.Hp += healing;
            Console.WriteLine($"{currentBattleUnitsStack.Amount - amountBeforeHealing} {currentBattleUnitsStack.UnitType.Name} has been returned");
        }
    }

    public enum TypeOfMagic
    {
        PunishingStrike,
        Curse,//проклятие
        Attenuation,//ослабление
        Acceleration,
        Resurrection//воскрешение
    }
}
