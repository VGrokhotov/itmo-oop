using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using game;
using game.MarchingArmy;


namespace FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string PATH = @"C:\Users\Lenonvo\Documents\itmo-oop\FinalProject\FinalProject\Mods";
            List<Unit> lol = new List<Unit>();
            foreach (var path in Directory.GetFiles(PATH))
            {
                Assembly.LoadFile(path).GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Unit)))
                    .Select(x => (Unit) Activator.CreateInstance(x)).ToList().ForEach(x => lol.Add(x));
            }
            lol = lol.Select(x => x).OrderBy(x => x.Name).ToList();

            //lol.Select(x => (x.Name)).ToList().ForEach(x => Console.Write(x+"\n"));

            Console.WriteLine("Welcome to the greatest game of the greatest!");
            Console.WriteLine("Just let's start!");
            Console.WriteLine("First player, please, enter your name:");
            string firstPlayerName = Console.ReadLine();
            Console.WriteLine("Second player, please, enter your name:");
            string secondPlayerName = Console.ReadLine();

            Console.WriteLine("Let's make your armies! Press Enter to continue...");
            Console.ReadLine();
            ArmyBuilder armyBuilder = new ArmyBuilder(lol);
            Army firstArmy = armyBuilder.MakeArmy(firstPlayerName);
            Army secondArmy = armyBuilder.MakeArmy(secondPlayerName);

            Battle game = new Battle(firstArmy, firstPlayerName, secondArmy, secondPlayerName);
            Console.WriteLine("Armies are equipped.\nPress Enter to start battle!");
            Console.ReadLine();
            game.StartBattle();
            /*
             *Add 1 in quantity 3
             *Add 7 in quantity 10
             *Add 11 in quantity 8
             *
             *Add 5 in quantity 3
             *Add 8 in quantity 5
             *Add 9 in quantity 8
             */
        }
    }
}
