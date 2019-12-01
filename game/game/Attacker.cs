using System;
using System.Collections.Generic;
using game.BattleArmyClasses;
// ReSharper disable InconsistentNaming

namespace game
{
    public class Attacker
    {
        public List<BattleUnitsStack> Attack((BattleUnitsStack, TypeOfArmy) currentBattleStack, BattleArmy attackedArmy)
        {
            Console.WriteLine("You chose \"Attack\"");
            Console.WriteLine("You can now attack following stacks from ");
            Console.WriteLine(attackedArmy.AliveStacks());
            Console.WriteLine("Enter the index of stack you wanna attack");
            while (true)
            {
                var numberOfStack = Console.ReadLine();
                if (!int.TryParse(numberOfStack, out int i))
                    Console.WriteLine("Incorrect input, try again");
                else
                {
                    if (i < 1 || i > attackedArmy.AmountOfAliveStacks())
                        Console.WriteLine("Incorrect input, try again");
                    else
                    {
                        /*if (currentBattleStack.Item1.Effects.IsEffectApplied(TypeOfEffect.BeatAll))
                        {
                            List<BattleUnitsStack> attackedStacks = new List<BattleUnitsStack>();
                            for (int j = attackedArmy.AmountOfAliveStacks(); j > 0; j--)
                            {
                                attackedStacks.Add(attackedArmy.AliveStackAt(j));
                                Attack(currentBattleStack.Item1, attackedArmy.AliveStackAt(j));
                            }

                            return attackedStacks;
                        }
                        else {*/
                            BattleUnitsStack attackedStack = attackedArmy.AliveStackAt(i);
                            Attack(currentBattleStack.Item1, attackedArmy.AliveStackAt(i)); 
                            return new List<BattleUnitsStack>(){ attackedStack };
                        //}
                        //механика ударить всех
                        
                    }
                }
            }
        }
        public int Damage(BattleUnitsStack attackingBUS, BattleUnitsStack attackedBUS)
        {
            double damage1;
            double damage2;
            double attack = attackingBUS.BattleUnit.Attack;
            double defence = attackedBUS.BattleUnit.Defence;
            if (attack < 0)
                attack = 0;
            if (defence < 0)
                defence = 0;
            if (attack > defence)
            {
                damage1 = attackingBUS.Amount * attackingBUS.BattleUnit.Damage1 * (1 + 0.05 * (attack - defence));
                damage2 = attackingBUS.Amount * attackingBUS.BattleUnit.Damage2 * (1 + 0.05 * (attack - defence));
            }
            else
            {
                damage1 = attackingBUS.Amount * attackingBUS.BattleUnit.Damage1 / (1 + 0.05 * (defence - attack));
                damage2 = attackingBUS.Amount * attackingBUS.BattleUnit.Damage2 / (1 + 0.05 * (defence - attack));
            }
            if (attackingBUS.BattleUnit.Damage1 == attackingBUS.BattleUnit.Damage2)
                return (int)Math.Floor(damage2);
            Random rnd = new Random();
            return rnd.Next((int)Math.Ceiling(damage1), (int)Math.Floor(damage2));
        }
        public void Attack(BattleUnitsStack attacking, BattleUnitsStack attacked)
        {
            int damage = Damage(attacking, attacked);
            int dead = attacked.Amount;
            if (damage < attacked.Hp)
            {
                attacked.Hp -= damage;
                dead -= attacked.Amount;
            }
            else
            {
                attacked.Hp = 0;//-= damage
            }

            Console.WriteLine($"{attacking.UnitType.Name} make {damage} damage to {attacked.UnitType.Name}, {dead} dead");

            bool enemyDoesNotRespond = false;
            //модификаторы на ответ

            if (!attacked.HasRespondThisTurn && attacked.IsAlive && !enemyDoesNotRespond)
            {
                attacked.HasRespondThisTurn = true;
                //вернуть false если есть бесконечный отпор
                int damageOfRespond = Damage(attacked, attacking);
                dead = attacking.Amount;
                if (damageOfRespond < attacking.Hp)
                {
                    attacking.Hp -= damageOfRespond;
                    dead -= attacking.Amount;
                }
                else
                {
                    attacking.Hp = 0; //-= damageOfRespond
                }
                Console.WriteLine($"{attacked.UnitType.Name} make {damageOfRespond} damage to {attacking.UnitType.Name} in return, {dead} dead");

            }
        }
    }
}
