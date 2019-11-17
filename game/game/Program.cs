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
            Army usArmy1 = new Army(unitsStacks);
            Army usArmy2 = new Army(unitsStacks);
            Battle game = new Battle(usArmy1, "first", usArmy2, "second");

            game.StartBattle();
        }
    }
}
