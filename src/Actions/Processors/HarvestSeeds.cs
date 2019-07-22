using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Actions
{
    public class HarvestSeeds
    {
        public static void CollectInput(Farm farm)
        {

            bool doOver;
            do
            {
                doOver = false;
                Program.DisplayBanner();

                for (var i = 0; i < farm.PlowedFields.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {farm.PlowedFields[i].Name} ({farm.PlowedFields[i].currentPlants} plants)");
                }
                Console.WriteLine();
                Console.WriteLine("Which facility has the plants you want to process?");

                Console.Write("> ");
                string fieldChoice = Console.ReadLine();
                int choice;
                try
                {
                    choice = Int32.Parse(fieldChoice);
                    var field = farm.PlowedFields[choice - 1];


                    var groups = field.CreateGroup();
                    var selectedGroup = SelectType(groups); 
                    int quantity = SelectQuantity(farm, selectedGroup);
                    int ProcessedSeeds = field.Process(quantity, selectedGroup.Key);
                    if (selectedGroup.Key == "Sunflower")
                    {
                        farm.SeedHarvester.SunflowerSeeds += ProcessedSeeds;
                    }
                    else if (selectedGroup.Key == "Sesame")
                    {
                        farm.SeedHarvester.SesameSeeds += ProcessedSeeds;

                    }


                }
                catch (Exception ex)
                {
                    Program.ShowMessage("Invalid Input");
                }        
            }
            while (doOver);

        }

        private static IGrouping<string, ISeedProducing> SelectType(List<IGrouping<string, ISeedProducing>> groups)
        {
            Program.DisplayBanner();
            
            for (int i = 0; i < groups.Count; i++)
            {
                string s = groups.Count > 1 ? "s" : "";
                System.Console.WriteLine($"{i+1}. {groups[i].Key}s ({groups[i].Count()} row{s} available)");
            }
            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                Console.WriteLine("What resource should be processed?");

                Console.Write("> ");
                string groupType = Console.ReadLine();
                int choice;
                try
                {
                    choice = Int32.Parse(groupType);
                    return groups[choice - 1];
                }
                catch (Exception ex)
                {
                    doOver = true;
                }
            } while (doOver);

            // This line will never run
            return null;

        }

        private static int SelectQuantity(Farm farm, IGrouping<string, ISeedProducing> group)
        {
            int capacity = farm.SeedHarvester.Capacity;
            Program.DisplayBanner();
            Console.WriteLine($"Selected {group.Key} with {group.Count()} plants available");

            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                Console.WriteLine("How many should be processed?");

                Console.Write("> ");
                string input = Console.ReadLine();
                int quantity;
                try
                {
                    quantity = Int32.Parse(input);
                    if (quantity <= capacity)
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
    }
}