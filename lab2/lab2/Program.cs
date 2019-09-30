using System;
using System.Xml;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit lol = new Unit(1, "lol", 30,5, 1, damage: (3, 3), 1);
            UnitsStack first = new UnitsStack(lol, 3);
            Console.WriteLine(first);
            Army gg = new Army();
            Console.WriteLine(gg.Amount);
            gg.AppendStack(first);
            Console.WriteLine(gg.Amount);
            Console.WriteLine(gg);
        }
    }
}
