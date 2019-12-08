using System;
using System.Collections.Generic;
using game.BattleArmyClasses;

namespace game
{
    public class Wizard
    {
        public TypeOfMagic ChosenMagic;
        public bool Wiz((BattleUnitsStack, TypeOfArmy) currentBattleStack, BattleArmy FirstBattleArmy, BattleArmy SecondBattleArmy, List<(BattleUnitsStack, TypeOfArmy)> Scale)
        {
            if (currentBattleStack.Item1.AmountOfAvailableMagic() > 0)
            {
                Console.WriteLine(currentBattleStack.Item1.AvailableMagic());
                Console.WriteLine("Enter the index of magic you wanna cast or \"0\" to choose another action");
                while (true)
                {
                    var indexOfMagic = Console.ReadLine();
                    if (!int.TryParse(indexOfMagic, out int i))
                        Console.WriteLine("Incorrect input, try again");
                    else
                    {
                        if (i < 0 || i > currentBattleStack.Item1.AmountOfAvailableMagic())
                            Console.WriteLine("Incorrect input, try again");
                        else
                        {
                            if (i == 0)
                            {
                                Console.WriteLine("You decided not to wiz, choose another action");
                                return false;
                            }
                            else
                            {
                                
                                Scale.RemoveAt(0);
                                ChosenMagic = currentBattleStack.Item1.AvailableMagicAt(i);
                                Console.WriteLine($"You chose {ChosenMagic}");
                                currentBattleStack.Item1.BunToWiz(ChosenMagic);

                                BattleArmy toWhatArmyUseMagic;
                                if (ChosenMagic.toWhatArmyCasts == ToWhatArmyCasts.Enemy)
                                    toWhatArmyUseMagic = currentBattleStack.Item2 == TypeOfArmy.First ? SecondBattleArmy : FirstBattleArmy;
                                else
                                    toWhatArmyUseMagic = currentBattleStack.Item2 == TypeOfArmy.First ? FirstBattleArmy : SecondBattleArmy;
                                Console.WriteLine($"You can now use {ChosenMagic} to following stacks from ");
                                //нужно вызывать не живые стаки, а все, если выбрали возраждене
                                if (ChosenMagic.GetType() == Resurrection.GetInstance().GetType())
                                    Console.WriteLine(toWhatArmyUseMagic);
                                else
                                    Console.WriteLine(toWhatArmyUseMagic.AliveStacks());
                                Console.WriteLine("Enter the index of stack you wanna wiz");
                                while (true)
                                {
                                    var numberOfStack = Console.ReadLine();
                                    if (!int.TryParse(numberOfStack, out int j))
                                        Console.WriteLine("Incorrect input, try again");
                                    else
                                    {
                                        if (ChosenMagic.GetType() == Resurrection.GetInstance().GetType())
                                        {
                                            if (j < 1 || j > toWhatArmyUseMagic.StacksList.Count)
                                                Console.WriteLine("Incorrect input, try again");
                                            else
                                            {
                                                BattleUnitsStack toWhatStackUseMagic = toWhatArmyUseMagic.StacksList[j-1];
                                                ChosenMagic.Wiz(currentBattleStack.Item1, toWhatStackUseMagic);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (j < 1 || j > toWhatArmyUseMagic.AmountOfAliveStacks())
                                                Console.WriteLine("Incorrect input, try again");
                                            else
                                            {
                                                BattleUnitsStack toWhatStackUseMagic = toWhatArmyUseMagic.AliveStackAt(j);
                                                ChosenMagic.Wiz(currentBattleStack.Item1, toWhatStackUseMagic);
                                                break;
                                            }
                                        }
                                    }
                                }

                                return true;
                            }
                            //break;
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
    }

    public enum ToWhatArmyCasts
    {
        Allied,//союзная
        Enemy//вражеская
    }

    public abstract class TypeOfMagic
    {
        public ToWhatArmyCasts toWhatArmyCasts;
        public abstract void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target);

    }

    public class PunishingStrike : TypeOfMagic
    {
        private static PunishingStrike Instance;
        private PunishingStrike() { toWhatArmyCasts = ToWhatArmyCasts.Allied; }

        public static PunishingStrike GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PunishingStrike();
            }

            return Instance;
        }

        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            target.Effects.Add((new IncreasedAttack(), 3));
        }

        public override string ToString()
        {
            return "PunishingStrike";
        }
    }

    public class Curse : TypeOfMagic
    {
        private static Curse Instance;
        private Curse() { toWhatArmyCasts = ToWhatArmyCasts.Enemy; }

        public static Curse GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Curse();
            }

            return Instance;
        }


        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            target.Effects.Add((new DecreasedAttack(), 3));
        }
        public override string ToString()
        {
            return "Curse";
        }
    }

    public class Attenuation : TypeOfMagic
    {
        private static Attenuation Instance;
        private Attenuation() { toWhatArmyCasts = ToWhatArmyCasts.Enemy; }

        public static Attenuation GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Attenuation();
            }

            return Instance;
        }

        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            target.Effects.Add((new DecreasedDefence(), 5));
        }
        public override string ToString()
        {
            return "Attenuation";
        }
    }

    public class Acceleration : TypeOfMagic
    {
        private static Acceleration Instance;
        private Acceleration() { toWhatArmyCasts = ToWhatArmyCasts.Allied; }

        public static Acceleration GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Acceleration();
            }

            return Instance;
        }

        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            target.Effects.Add((new IncreasedInitiative(), 3));
        }
        public override string ToString()
        {
            return "Acceleration";
        }
    }

    public class Resurrection : TypeOfMagic
    {
        private static Resurrection Instance;
        private Resurrection() { toWhatArmyCasts = ToWhatArmyCasts.Allied; }

        public static Resurrection GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Resurrection();
            }

            return Instance;
        }

        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            int healing = wizStack.Amount;
            switch (wizStack.UnitType.Name)
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
            int startHp = target.StartAmount * (int)target.UnitType.HitPoints;
            if (target.Hp < 0)
            {
                target.Hp = 0;
                amountBeforeHealing = 0;
            }
            else
            {
                amountBeforeHealing = target.Amount;
            }

            if (healing + target.Hp >= startHp)
                target.Hp = startHp;
            else
                target.Hp += healing;
            Console.WriteLine($"{target.Amount - amountBeforeHealing} {target.UnitType.Name} has been returned");
        }

        public override string ToString()
        {
            return "Resurrection";
        }
    }
}
