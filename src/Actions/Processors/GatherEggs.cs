using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Actions
{
    public class GatherEggs
    {

        private static List<IGathering> _facilities = new List<IGathering>();

        public static void CollectInput(Farm farm)
        {
            do
            {
                UpdateFacilities(farm);
                if (_facilities.Count == 0)
                {
                    Program.ShowMessage("No available facilities to process.");
                    return;
                }
                // Select a house
                IGathering selectedHouse = SelectHouse(farm);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.EggGatherer.Capacity, selectedHouse);

                // // Add selected resources to hopper
                selectedHouse.SendToBasket(quantity, farm);


            } while (AddMore(farm.EggGatherer.Capacity));

            farm.EggGatherer.Gather();
        }


        private static IGathering SelectHouse(Farm farm)
        {
            bool doOver;
            do
            {

                doOver = false;
                Program.DisplayBanner();
                UpdateFacilities(farm);

                for (var i = 0; i < _facilities.Count; i++)
                {
                    var facility = _facilities[i];
                    if (facility is ChickenHouse && farm.EggGatherer.Capacity >= 7)
                    {
                        Console.WriteLine($"{i + 1}. {facility.Name} ({facility.NumAnimals} chickens)");
                    }
                    else if (facility is DuckHouse && farm.EggGatherer.Capacity >= 6)
                    {
                        Console.WriteLine($"{i + 1}. {facility.Name} ({facility.NumAnimals} ducks)");
                    }
                    else if (facility is GrazingField && farm.EggGatherer.Capacity >= 3)
                    {
                        Console.WriteLine($"{i + 1}. {facility.Name} ({facility.NumAnimals} ostriches)");
                    }
                    else
                    {
                        farm.EggGatherer.Gather();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Which facility has the animals you want to collect eggs from?");

                Console.Write("> ");
                string fieldChoice = Console.ReadLine();
                int choice;
                try
                {
                    choice = Int32.Parse(fieldChoice);
                    var house = _facilities[choice - 1];
                    return house;
                }
                catch (Exception)
                {
                    Program.ShowMessage("Invalid Input");
                }
            }
            while (doOver);

            //  Should never get here.
            return null;
        }

        private static int SelectQuantity(int capacity, IGathering house)
        {
            // int[] numbers = { capacity, group.Count() };

            int numAnimals = house.NumAnimals;
            int eggsPerAnimal = 0;

            if (house is GrazingField field)
            {
                numAnimals = field.NumOstriches;
                eggsPerAnimal = 3;
            }
            if (house is ChickenHouse)
            {
                eggsPerAnimal = 7;
            }
            if (house is DuckHouse)
            {
                eggsPerAnimal = 6;
            }

            double maxAvailable = Math.Floor((double)capacity / eggsPerAnimal);
            int[] animalArray = { (int)maxAvailable, numAnimals };
            int maxAnimals = animalArray.Min();

            Program.DisplayBanner();
            Console.WriteLine($"Egg Gatherer can gather eggs from {maxAnimals} animals.");

            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                Console.WriteLine($"How many animals should be selected?");

                Console.Write("> ");
                string input = Console.ReadLine();
                int quantity;
                try
                {
                    quantity = Int32.Parse(input);
                    if (quantity <= maxAnimals)
                    {
                        return quantity;
                    }
                    else throw new Exception();
                }
                catch (Exception ex)
                {
                    Program.ShowMessage("Invalid entry");
                    doOver = true;
                }
            } while (doOver);

            // This line will never run
            return 0;
        }

        private static bool AddMore(int capacity)
        {
            if (_facilities.Count == 0) return false;
            if (capacity < 3) return false;
            bool doOver = false;

            do
            {
                doOver = false;
                Program.DisplayBanner();
                Console.WriteLine($"Egg Gatherer has {capacity} eggs available in capacity.");
                Console.WriteLine();
                Console.WriteLine("Would you like to add more resources?");
                Console.WriteLine();
                Console.WriteLine("Please press (Y/y) or (N/n).");
                Console.Write("> ");
                string response = Console.ReadLine();
                switch (response)
                {
                    case "Y":
                        return true;
                    case "y":
                        return true;
                    case "N":
                        return false;
                    case "n":
                        return false;
                    default:
                        Program.ShowMessage("Invalid input.  Please try again.");
                        doOver = true;
                        break;
                }

            } while (doOver);

            // Never runs.
            return false;

        }

        static private void UpdateFacilities(Farm farm)
        {
            List<IGathering> output = new List<IGathering>();
            output.AddRange(farm.ChickenHouses);
            output.AddRange(farm.DuckHouses);
            output.AddRange(farm.GrazingFields);

            _facilities = output
                .Where(facility =>
                {
                    if (facility is GrazingField gf)
                    {
                        return gf.NumOstriches > 0;
                    }
                    return facility.NumAnimals > 0;
                })
                .ToList();

        }
    }

}