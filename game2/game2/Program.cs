using System;
using System.Collections.Generic;
using game;
using game.BattleArmyClasses;
using game.MarchingArmy;
using game.Units;

namespace game2
{
    public class DecreasedInitiative : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative /= 1.4;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BattleUnit.whiteInitiative *= 1.4;
        }
    }

    public class BeatAll : TypeOfEffect
    {
        public override void Modify(BattleUnitsStack currentBattleUnitsStack)
        {
            currentBattleUnitsStack.BeatAll = true;
        }

        public override void Unmodify(BattleUnitsStack currentBattleUnitsStack) { }
    }

    public class Slowdown : TypeOfMagic
    {
        private static Slowdown Instance;
        private Slowdown() { toWhatArmyCasts = ToWhatArmyCasts.Enemy; }

        public static Slowdown GetInstance()
        {
            if (Slowdown.Instance == null)
            {
                Slowdown.Instance = new Slowdown();
            }

            return Slowdown.Instance;
        }

        public override void Wiz(BattleUnitsStack wizStack, BattleUnitsStack target)
        {
            target.Effects.Add((new DecreasedInitiative(), 3));
        }
        public override string ToString()
        {
            return "Slowdown";
        }
    }

    public class LolKekovich : Unit
    {
        public LolKekovich() : base("Lol Kekovich", 10, 50, 10, damage: (50, 50), 20)
        {
            this.accessibleMagic.Add(Slowdown.GetInstance());
            this.congenitalEffects.Add(new BeatAll());
            this.congenitalEffects.Add(new EnemyDoesNotRespond());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Angel angel = new Angel();
            Arbalester arbalester = new Arbalester();
            Fury fury = new Fury();
            LolKekovich lolKekovich = new LolKekovich();


            UnitsStack stack1 = new UnitsStack(angel, 3);
            UnitsStack stack3 = new UnitsStack(arbalester, 3);
            UnitsStack stack7 = new UnitsStack(fury, 3);
            UnitsStack stack8 = new UnitsStack(lolKekovich, 3);

            List<UnitsStack> unitsStacks = new List<UnitsStack>() { stack1, stack3, stack7, stack8};
            Army usArmy1 = new Army(unitsStacks);
            Army usArmy2 = new Army(unitsStacks);
            Battle game = new Battle(usArmy1, "first", usArmy2, "second");

            game.StartBattle();
        }
    }
}
