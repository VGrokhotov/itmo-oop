using System.Collections.Generic;

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

        public Unit(string name, uint hitPoints, uint attack, uint defence, (uint, uint) damage,
            double initiative)
        {
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage;
            this.Initiative = initiative;
            accessibleMagic = new List<TypeOfMagic>();
        }

        public Unit Clone()
        {
            Unit currentUnit = new Unit(this.Name, this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
            foreach (var magic in AccessibleMagic)
            {
                currentUnit.accessibleMagic.Add(magic);
            }
            return currentUnit;
        }
    }
}
