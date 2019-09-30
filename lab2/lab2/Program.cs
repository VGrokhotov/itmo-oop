using System;

namespace lab2
{
    class Program
    {
        static void Main()
        {
            Unit lol = new Unit(1, "lol", 30, 5, 1, damage: (3, 3), 1);
            Unit kek = new Unit(1, "kek", 30, 5, 1, damage: (3, 3), 1);
            UnitsStack first = new UnitsStack(lol, 3);
            Console.WriteLine(first.IsStackAlive);
            Army gg = new Army();
            gg.AppendStack(first);
            gg.AppendStack(new UnitsStack(kek, 8));
            gg.DeleteStack(first);
            var ex = gg.StacksList;
            ex.Add(new UnitsStack(kek, 3));
            Console.WriteLine(gg);
        }
    }
}
