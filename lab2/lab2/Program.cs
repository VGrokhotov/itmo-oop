using System;
using System.Collections.Generic;
using lab2.Units;

namespace lab2
{
    class Program
    {
        static void Main()
        {
            Angel angel1 = new Angel();
            UnitsStack stack1 = new UnitsStack(angel1, 3);

            Army gg = new Army(new List<UnitsStack>() {stack1, new UnitsStack(angel1, 8)});
            var ex = gg.StacksList;
            ex.Add(new UnitsStack(angel1, 3));
            Console.WriteLine(gg);
        }
    }
}
