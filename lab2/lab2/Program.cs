﻿using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit lol = new Unit(1, "lol", 30,5, 1, new Pair<int, int>(3, 3), 1);
            UnitsStack first = new UnitsStack(lol, 3);
            Console.WriteLine(first.UnitType);
        }
    }
}