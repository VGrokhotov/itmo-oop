namespace lab2
{
    class UnitsStack
    {
        public Unit UnitType { get; }

        public int Amount { get; set; }

        public bool IsStackAlive => Amount > 0;


        public UnitsStack(Unit unitType, int amount)
        {
            this.Amount = amount;
            this.UnitType = unitType;
        }

        public override string ToString()
        {
            return ($"{this.UnitType.Name}: {this.Amount}\n");
        }

        public UnitsStack Clone()
        {
            return new UnitsStack(this.UnitType.Clone(), this.Amount);
        }
    }
}
