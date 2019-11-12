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

        public Unit(string name, uint hitPoints, uint attack, uint defence, (uint, uint) damage,
            double initiative)
        {
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage;
            this.Initiative = initiative;
        }

        public Unit Clone()
        {
            return new Unit(this.Name,  this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
        }
    }
}
