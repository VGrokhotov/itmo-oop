using System;
using System.Collections.Generic;
using System.Text;
using game;
using game.BattleArmyClasses;
using game.MarchingArmy;

namespace mod1
{
    public class LolKekovich : Unit
    {
        public LolKekovich() : base("Lol Kekovich", 10, 50, 10, damage: (50, 50), 20)
        {
            this.accessibleMagic.Add(Slowdown.GetInstance());
            this.congenitalEffects.Add(new BeatAll());
            this.congenitalEffects.Add(new EnemyDoesNotRespond());
        }
    }
}
