using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }

        public Pair(T first, K second)
        {
            First = first;
            Second = second;
        }
    }
    class Unit
    {
        private uint tipe;
        public uint Tipe => tipe;

        private string name;
        public string Name => name;

        private int hitPoints;
        public int HitPoints => hitPoints;
        private int attack;
        public int Attack => attack;
        private int defence;
        public int Defence => defence;
        private Pair<int, int> damage;
        public Pair<int, int> Damage => damage;
        private double initiative;
        public double Initiative => initiative;

        public Unit(uint tipe, string name, int hitPoints, int attack, int defence, Pair<int, int> damage,
            double initiative)
        {
            this.tipe = tipe;
            this.name = name;
            this.hitPoints = hitPoints;
            this.attack = attack;
            this.defence = defence;
            this.damage = damage;
            this.initiative = initiative;
        }
    }

    class UnitsStack
    {
        private Unit unitType;
        private int amount;

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        public bool IsStackAlive
        {
            get
            {
                if (amount > 0)
                    return true;
                else
                    return false;
            }
        }

        public Unit UnitType => unitType;

        public UnitsStack(Unit unitType, int amount)
        {
            this.amount = amount;
            this.unitType = unitType;
        }
    }

    class Army
    {
        private List<UnitsStack> stacksList;

        public int Amount => stacksList.Count;

        public void AppendStack(UnitsStack currentStack)
        {
            if (this.Amount > 5)
            {
                //Console.WriteLine("Too much Unit Stacks");
                return;
            }
            else
                stacksList.Add(currentStack);
        }

        public bool DeleteStack(UnitsStack currentStack)
        {
            if (this.stacksList.Remove(currentStack))
                return true;
            else
                return false;
        }

        //public ??? get list of stacks

    }
}
