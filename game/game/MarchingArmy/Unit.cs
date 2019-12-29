using System.Collections.Generic;
using game.BattleArmyClasses;

namespace game.MarchingArmy
{
    public class Unit
    {
        public string Name { get; }
        public uint HitPoints { get; }
        public uint Attack { get; }
        public uint Defence { get; }
        public (uint, uint) Damage { get; }
        public double Initiative { get; }

        public string GetAllInformation()
        {
            string result = $"Information about unit {Name}:\n";
            result += $"Hit Points: {HitPoints}\n";
            result += $"Attack: {Attack}\n";
            result += $"Defence: {Defence}\n";
            result += $"Damage: {Damage.Item1} - {Damage.Item2}\n";
            result += $"Initiative: {Initiative}\n";
            return result;
        }

        protected List<TypeOfMagic> accessibleMagic;

        public List<TypeOfMagic> AccessibleMagic
        {
            get
            {
                var newMagicList = new List<TypeOfMagic>();
                accessibleMagic.ForEach((magic) => newMagicList.Add(magic));
                return newMagicList;
            }
        }

        protected List<TypeOfEffect> congenitalEffects;

        public List<TypeOfEffect> CongenitalEffects
        {
            get
            {
                var newEffectList = new List<TypeOfEffect>();
                congenitalEffects.ForEach((effect) => newEffectList.Add(effect));
                return newEffectList;
            }
        }

        public Unit(string name, uint hitPoints, uint attack, uint defence, (uint, uint) damage,
            double initiative)
        {
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage.Item1 <= damage.Item2 ? damage : (damage.Item2, damage.Item1);
            this.Initiative = initiative;
            accessibleMagic = new List<TypeOfMagic>();
            congenitalEffects = new List<TypeOfEffect>();
        }

        public Unit Clone()
        {
            Unit currentUnit = new Unit(this.Name, this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
            foreach (var magic in AccessibleMagic)
            {
                currentUnit.accessibleMagic.Add(magic);
            }
            foreach (var effect in CongenitalEffects)
            {
                currentUnit.congenitalEffects.Add(effect);
            }
            return currentUnit;
        }
    }
}
