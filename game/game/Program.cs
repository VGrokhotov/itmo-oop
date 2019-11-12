using System;
using System.Collections.Generic;
using game.BattleArmyClasses;
using game.MarchingArmy;
using game.Units;


namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            Angel angel = new Angel();
            Devil devil = new Devil();
            Arbalester arbalester = new Arbalester();
            Lich lich = new Lich();

            UnitsStack stack1 = new UnitsStack(angel, 3);
            UnitsStack stack2 = new UnitsStack(devil, 3);
            UnitsStack stack3 = new UnitsStack(arbalester, 3);
            UnitsStack stack4 = new UnitsStack(lich, 3);
            List<UnitsStack> unitsStacks = new List<UnitsStack>() { stack1, stack2, stack3, stack4 };
            Army usArmy = new Army(unitsStacks);
            List<BattleUnitsStack> battleUnitsStacks = new List<BattleUnitsStack>();
            foreach (var stack in usArmy.StacksList)
            {
                battleUnitsStacks.Add(new BattleUnitsStack(stack));
            }
            BattleArmy fbusArmy = new BattleArmy(battleUnitsStacks, "first");
            BattleArmy sbusArmy = new BattleArmy(battleUnitsStacks, "second");
            Battle game = new Battle(fbusArmy, sbusArmy);

            game.StartBattle();
        }
    }
}
