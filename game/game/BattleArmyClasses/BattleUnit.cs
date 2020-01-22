using System;
using System.Collections.Generic;
using System.Text;
using game.MarchingArmy;

namespace game.BattleArmyClasses
{
    public class BattleUnit
    {
        public int whiteHp;
        public int greenHp = 0;
        public int Hp => whiteHp + greenHp;

        public int whiteAttack;
        public int greenAttack = 0;
        public int Attack => whiteAttack + greenAttack;

        public int whiteDefence;
        public int greenDefence = 0;
        public int Defence => whiteDefence + greenDefence;

        public double whiteInitiative;
        public double greenInitiative = 0;
        public double Initiative => whiteInitiative + greenInitiative;

        public int whiteDamage1;
        public int greenDamage1 = 0;
        public int Damage1 => whiteDamage1 + greenDamage1;

        public int whiteDamage2;
        public int greenDamage2 = 0;
        public int Damage2 => whiteDamage2 + greenDamage2;
        public BattleUnit(Unit unit)
        {
            whiteHp = (int) unit.HitPoints;
            whiteAttack = (int) unit.Attack;
            whiteDefence = (int) unit.Defence;
            whiteDamage1 = (int) unit.Damage.Item1;
            whiteDamage2 = (int) unit.Damage.Item2;
            whiteInitiative = (int) unit.Initiative;
        }
    }
}
