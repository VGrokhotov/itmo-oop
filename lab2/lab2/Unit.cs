namespace lab2
{
    public class Unit
    {
        public uint Type { get; }
        public uint HitPoints { get; }
        public uint Attack { get; }
        public uint Defence { get; }
        public (uint, uint) Damage { get; }
        public double Initiative { get; }

        public Unit(uint type, uint hitPoints, uint attack, uint defence, (uint, uint) damage,
            double initiative)
        {
            this.Type = type;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage;
            this.Initiative = initiative;
        }

        public Unit Clone()
        {
            return new Unit(this.Type,  this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
        }
    }
}
