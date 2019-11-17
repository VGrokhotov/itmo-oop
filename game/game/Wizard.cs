using System;
using game.BattleArmyClasses;

namespace game
{
    public class Wizard
    {
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
