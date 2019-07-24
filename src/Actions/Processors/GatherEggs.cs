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
        public static void CollectInput(Farm farm)
        {
            do
            {

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

                var houses = new List<IGathering>();
                houses.AddRange(farm.ChickenHouses);
                houses.AddRange(farm.DuckHouses);
                houses.AddRange(farm.GrazingFields);

                for (var i = 0; i < houses.Count; i++)
                {
                    if (houses[i] is ChickenHouse && farm.EggGatherer.Capacity >= 7)
                    {
                        Console.WriteLine($"{i + 1}. {houses[i].Name} ({houses[i].NumAnimals} animals)");
                    }
                    else if (houses[i] is DuckHouse && farm.EggGatherer.Capacity >= 6)
                    {
                        Console.WriteLine($"{i + 1}. {houses[i].Name} ({houses[i].NumAnimals} animals)");
                    }
                    else if (houses[i] is GrazingField && farm.EggGatherer.Capacity >= 3)
                    {
                        Console.WriteLine($"{i + 1}. {houses[i].Name} ({houses[i].NumAnimals} animals)");
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
                    var house = houses[choice - 1];
                    return house;
                }
                catch (Exception ex)
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

            double maxAvailable = capacity;
            Program.DisplayBanner();
            Console.WriteLine($"Selected {house.Name} with {house.NumAnimals} animals available to gather eggs from.");
            Console.WriteLine($"Egg Gatherer has {capacity} eggs of available capacity.");

            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                Console.WriteLine($"How many should be gathered, maximum of {house.NumAnimals}?");

                Console.Write("> ");
                string input = Console.ReadLine();
                int quantity;
                try
                {
                    quantity = Int32.Parse(input);
                    if (quantity <= maxAvailable)
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
            if (capacity == 0) return false;
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
                        break;
                    case "y":
                        return true;
                        break;
                    case "N":
                        return false;
                        break;
                    case "n":
                        return false;
                        break;
                    default:
                        Program.ShowMessage("Invalid input.  Please try again.");
                        doOver = true;
                        break;
                }

            } while (doOver);

            // Never runs.
            return false;

        }
    }

}