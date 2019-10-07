using System;
using System.Collections.Generic;
using lab2.Units;

namespace lab2
{
    class Program
    {
        static void Main()
        {
            try
            {
                Angel angel = new Angel();
                Devil devil = new Devil();
                Arbalester arbalester = new Arbalester();
                Lich lich = new Lich();

                UnitsStack stack1 = new UnitsStack(angel, 3);
                UnitsStack stack2 = new UnitsStack(devil, 3);
                UnitsStack stack3 = new UnitsStack(arbalester, 3);
                UnitsStack stack4 = new UnitsStack(lich, 3);
                List<UnitsStack> unitsStacks = new List<UnitsStack>() {stack1, stack2, stack3, stack4};

                Console.WriteLine("list before editing:");
                foreach (var stack in unitsStacks)
                {
                    Console.WriteLine(stack);
                }
                Console.WriteLine("\n\n");
                

                Army gg = new Army(unitsStacks);
                Console.WriteLine(gg);
                Console.WriteLine("\n\n");

                unitsStacks.Add(new UnitsStack(angel, 9));
                Console.WriteLine("list after editing:");
                foreach (var stack in unitsStacks)
                {
                    Console.WriteLine(stack);
                }
                Console.WriteLine("\n\n");

                Console.WriteLine("army after editing list:");
                Console.WriteLine(gg);

                var ex = gg.StacksList;
                ex.Add(new UnitsStack(angel, 3));
                Console.WriteLine("army after editing got list:");
                Console.WriteLine(gg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
