namespace lab2
{
    public class UnitsStack
    {
        public Unit UnitType { get; }

        public int Amount { get;}

        //public bool IsStackAlive => Amount > 0;


        public UnitsStack(Unit unitType, int amount)
        {
            this.Amount = amount;
            this.UnitType = unitType;
        }

        public override string ToString()
        {
            return ($"{this.UnitType.Type}: {this.Amount}\n");
        }

        public UnitsStack Clone()
        {
            return new UnitsStack(this.UnitType.Clone(), this.Amount);
        }
    }
}
