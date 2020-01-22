using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.MarchingArmy;

namespace game
{
    public class InformationManager
    {
        private List<Unit> availableUnits;
        public InformationManager(List<Unit> AvailableUnits)
        {
            var newUnitList = new List<Unit>();
            AvailableUnits.ForEach((unit) => newUnitList.Add(unit.Clone()));
            availableUnits = newUnitList;
        }

        public void getAllInfo()
        {
            Console.Clear();
            Console.WriteLine("Here you can know all information about available units.");
            Console.WriteLine("Enter the key to learn more about unit you chose.\n");
            int i = 0;
            availableUnits.Select(x => (x.Name)).ToList().ForEach(x => Console.Write($"[{++i}]" + x + "\n"));
            Console.WriteLine("\nEnter your command. To skip all information Enter \"Next\"");
            while (true)
            {

                string command = Console.ReadLine();

                if (int.TryParse(command, out int index))
                {
                    if (index > 0 && index <= availableUnits.Count)
                    {
                        Console.Clear();
                        Console.WriteLine(availableUnits[index - 1].GetAllInformation());
                        Console.WriteLine("Press Enter to return...");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Here you can know all information about available units.");
                        Console.WriteLine("Enter the key to learn more about unit you chose.\n");
                        i = 0;
                        availableUnits.Select(x => (x.Name)).ToList().ForEach(x => Console.Write($"[{++i}]" + x + "\n"));
                        Console.WriteLine("\nEnter your command. To skip all information Enter \"Next\"");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect index, try again");
                    }
                }
                else
                {
                    if (command == "Next")
                    {
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input, try again");
                    }
                }
            }


        }


    }

}
