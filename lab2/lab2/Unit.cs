namespace lab2
{
    class Unit
    {
        public uint Type { get; }
        public string Name { get; }
        public int HitPoints { get; }
        public int Attack { get; }
        public int Defence { get; }
        public (int, int) Damage { get; }
        public double Initiative { get; }

        public Unit(uint type, string name, int hitPoints, int attack, int defence, (int, int) damage,
            double initiative)
        {
            this.Type = type;
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Attack = attack;
            this.Defence = defence;
            this.Damage = damage;
            this.Initiative = initiative;
        }

        public Unit Clone()
        {
            return new Unit(this.Type, this.Name, this.HitPoints, this.Attack, this.Defence, this.Damage, this.Initiative);
        }
    }
}
