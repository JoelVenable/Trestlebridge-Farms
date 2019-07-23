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


                // Select a resource type
                // var groups = selectedHouse.CreateGroup();
                // IGrouping<string, IEggProducing> selectedGroup = SelectResourceType(groups);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.EggGatherer.Capacity, selectedHouse);

                // // Add selected resources to hopper
                selectedHouse.SendToBasket(quantity, farm);


            } while (AddMore(farm.EggGatherer.Capacity));

            farm.EggGatherer.Gather();

            // if (selectedGroup.Key == "Sunflower")
            // {
            //   farm.SeedHarvester.SunflowerSeeds += ProcessedSeeds;
            // }
            // else if (selectedGroup.Key == "Sesame")
            // {
            //   farm.SeedHarvester.SesameSeeds += ProcessedSeeds;

            // }
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

                for (var i = 0; i < houses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {houses[i].Name} ({houses[i].NumAnimals} animals)");
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


        // private static IGrouping<string, IEggProducing> SelectResourceType(List<IGrouping<string, IEggProducing>> groups)
        // {
        //     Program.DisplayBanner();

        //     for (int i = 0; i < groups.Count; i++)
        //     {
        //         string s = groups.Count > 1 ? "s" : "";
        //         System.Console.WriteLine($"{i + 1}. {groups[i].Key}s ({groups[i].Count()} animals{s} available)");
        //     }
        //     bool doOver;

        //     do
        //     {
        //         doOver = false;
        //         Console.WriteLine();
        //         Console.WriteLine("What resource should be processed?");

        //         Console.Write("> ");
        //         string groupType = Console.ReadLine();
        //         int choice;
        //         try
        //         {
        //             choice = Int32.Parse(groupType);
        //             return groups[choice - 1];
        //         }
        //         catch (Exception ex)
        //         {
        //             doOver = true;
        //         }
        //     } while (doOver);

        //     // This line will never run
        //     return null;

        // }

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