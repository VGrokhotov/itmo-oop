using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using game;
using game.MarchingArmy;

namespace FinalProject
{
    public class ArmyBuilder
    {
        private List<Unit> _units;
        private List<UnitsStack> _currentUnitsStacks;

        public ArmyBuilder(List<Unit> units)
        {
            var newUnitList = new List<Unit>();
            units.ForEach((unit) => newUnitList.Add(unit.Clone()));
            _units = newUnitList;
            _currentUnitsStacks = new List<UnitsStack>();
        }

        public Army MakeArmy(string playerName)
        {
            Console.Clear();
            _currentUnitsStacks.Clear();
            Console.WriteLine($"{playerName}, please, choose up to {Config.MAX_ARMY_NUMBER} stacks from this list:\n");
            
            int i = 0;
            _units.Select(x => (x.Name)).ToList().ForEach(x => Console.Write($"[{++i}]" + x + "\n"));
            Console.WriteLine("\n*To choose the stack just enter \"Add [unit_number] in quantity [amount_of_this_kind_of_unit]\"");
            Console.WriteLine("For example, command \"Add 1 in quantity 6\" will add 6 units of first type to your army.");
            Console.WriteLine($"Just remember that you can not have more than {Config.MAX_STACK_NUMBER} units in one stack");
            Console.WriteLine("If you wanna end adding, enter \"Stop\"");
            while (_currentUnitsStacks.Count < Config.MAX_ARMY_NUMBER)
            {
                Console.WriteLine("\nEnter next command:");
                bool flag = false;
                string command = Console.ReadLine();
                Console.WriteLine();
                switch (command)
                {
                    case var someVal when new Regex( @"^Add(\s)(\d+)(\s)in(\s)quantity(\s)(\d+)(\s*)").IsMatch(someVal):
                        
                        int indexOfUnit = int.Parse(command.Split(" ")[1]);//распарсить строчку
                        int amountOfUnits = int.Parse(command.Split(" ")[4]);//распарсить строчку
                        if (indexOfUnit > _units.Count || indexOfUnit < 1)
                        {
                            Console.WriteLine("Incorrect index of unit type, try again");
                            break;
                        }

                        if (amountOfUnits > Config.MAX_STACK_NUMBER || amountOfUnits < 1)
                        {
                            Console.WriteLine("Incorrect amount of units, try again");
                            break;
                        }
                        _currentUnitsStacks.Add(new UnitsStack(_units[indexOfUnit-1], amountOfUnits));
                        Console.WriteLine($"{amountOfUnits} {_units[indexOfUnit - 1].Name} was added to your army, {playerName}.\n");
                        break;
                    case "Stop":
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Incorrect input, try again");
                        break;

                }
                if (flag)
                    break;
            }

            Console.WriteLine($"Your army is equipped, {playerName}. Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return new Army(_currentUnitsStacks);
        }

        
    }
}
