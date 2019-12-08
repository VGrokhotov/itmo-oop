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
            Hydra hydra = new Hydra();
            Griffin griffin = new Griffin();
            Fury fury = new Fury();


            UnitsStack stack1 = new UnitsStack(angel, 3);
            UnitsStack stack2 = new UnitsStack(devil, 3);
            UnitsStack stack3 = new UnitsStack(arbalester, 3);
            UnitsStack stack4 = new UnitsStack(lich, 3);
            UnitsStack stack5 = new UnitsStack(hydra, 3);
            UnitsStack stack6 = new UnitsStack(griffin, 3);
            UnitsStack stack7 = new UnitsStack(fury, 3);

            List<UnitsStack> unitsStacks = new List<UnitsStack>() { stack1, stack2, /*stack3,*/ stack4, stack5, stack6, stack7};
            Army usArmy1 = new Army(unitsStacks);
            Army usArmy2 = new Army(unitsStacks);
            Battle game = new Battle(usArmy1, "first", usArmy2, "second");

            game.StartBattle();
        }
    }
}
