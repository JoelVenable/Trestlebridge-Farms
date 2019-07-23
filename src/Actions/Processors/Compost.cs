using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Actions
{
    public class Compost
    {
        public static void CollectInput(Farm farm)
        {
            do
            {

                // Select a field
                ICompostProducing selectedField = SelectField(farm);

                // Select a resource type

                var groups = selectedField.CreateCompostList();

                IGrouping<string, IComposting> selectedGroup = SelectResourceType(groups);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.Composter.Capacity, selectedGroup);

                // Add selected resources to hopper
                selectedField.SendToHopper(quantity, selectedGroup.Key, farm);


            } while (AddMore(farm.Composter.Capacity));

            farm.Composter.Process();

            // if (selectedGroup.Key == "Sunflower")
            // {
            //   farm.SeedHarvester.SunflowerSeeds += ProcessedSeeds;
            // }
            // else if (selectedGroup.Key == "Sesame")
            // {
            //   farm.SeedHarvester.SesameSeeds += ProcessedSeeds;

            // }
        }


        private static ICompostProducing SelectField(Farm farm)
        {
            bool doOver;
            do
            {
                doOver = false;
                Program.DisplayBanner();
                var fields = new List<ICompostProducing>();
                fields.AddRange(farm.GrazingFields);
                fields.AddRange(farm.NaturalFields);
                for (var i = 0; i < fields.Count; i++)
                {
                    if (fields[i].GetType().Name == "GrazingField")
                    {
                        Console.WriteLine($"{i + 1}. {fields[i].Name} ({fields[i].CompostAmmount} goats)");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {fields[i].Name} ({fields[i].CompostAmmount} plants)");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Which facility has the plants you want to process?");

                Console.Write("> ");
                string fieldChoice = Console.ReadLine();
                int choice;
                try
                {
                    choice = Int32.Parse(fieldChoice);
                    var field = fields[choice - 1];
                    return field;




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


        private static IGrouping<string, IComposting> SelectResourceType(List<IGrouping<string, IComposting>> groups)
        {
            Program.DisplayBanner();

            for (int i = 0; i < groups.Count; i++)
            {
                string s = groups[i].Count() > 1 ? "s" : "";

                if (groups[i].Key == "Goat")
                {
                    System.Console.WriteLine($"{i + 1}. {groups[i].Key}s ({groups[i].Count()} goat{s} available)");
                }
                else
                {
                    System.Console.WriteLine($"{i + 1}. {groups[i].Key}s ({groups[i].Count()} row{s} available)");
                }
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

        private static int SelectQuantity(int capacity, IGrouping<string, IComposting> group)
        {
            int[] numbers = { capacity, group.Count() };

            int maxAvailable = numbers.Min();
            Program.DisplayBanner();
            if (group.Key == "Goat" && group.Count() > 0)
            {
                Console.WriteLine($"Selected {group.Key} with {group.Count()} goat compost available to process.");
                Console.WriteLine($"Composter has {capacity} of available capacity.");
            }
            else
            {
                Console.WriteLine($"Selected {group.Key} with {group.Count()} rows of plants available to process.");
                Console.WriteLine($"Composter has {capacity} of available capacity.");
            }

            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                Console.WriteLine($"How many should be processed, maximum of {maxAvailable}?");

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
                Console.WriteLine($"Composter has {capacity} available capacity for plants and {capacity / 2} for goat compost.");
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
    }

}