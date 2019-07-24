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
                
                // Select a field
                ICompostProducing selectedField = SelectField(farm);

                // Select a resource type

                var groups = selectedField.CreateCompostList();

                IGrouping<string, IComposting> selectedGroup = SelectResourceType(groups);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.Composter.Capacity, selectedGroup);

                // Add selected resources to hopper
                selectedField.SendToComposter(quantity, selectedGroup.Key, farm);


            } while (AddMore(farm.Composter.Capacity));

            farm.Composter.Process();

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
                Console.WriteLine("Which facility has the compost you want to process?");

                Console.Write("> ");
                string fieldChoice = Console.ReadLine();
                int choice;
                try
                {
                    choice = Int32.Parse(fieldChoice);
                    var field = fields[choice - 1];
                    return field;




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
                catch (Exception)
                {
                    doOver = true;
                }
            } while (doOver);

            // This line will never run
            return null;

        }

        private static int SelectQuantity(int capacity, IGrouping<string, IComposting> group)
        {
            int[] plantNumbers = { capacity, group.Count() };
            int[] goatNumbers = { (capacity / 2), group.Count() };


            int maxAvailablePlants = plantNumbers.Min();
            int maxAvailableGoats = goatNumbers.Min();
            Program.DisplayBanner();
            if (group.Key == "Goat" && group.Count() > 0)
            {
                Console.WriteLine($"Selected {group.Key}, There are {group.Count()} goat's with compost available to process.");
                Console.WriteLine($"Composter has {capacity / 2} of available capacity.");
            }
            else
            {
                Console.WriteLine($"Selected {group.Key} with {group.Count()} rows of plants available to process.");
                Console.WriteLine($"Composter has an available capacity of  {capacity}.");
            }

            bool doOver;

            do
            {
                doOver = false;
                Console.WriteLine();
                if (group.Key == "Goat" && group.Count() > 0)
                {
                    Console.WriteLine($"How many should be processed, maximum of {maxAvailableGoats}?");

                }
                else
                {
                    Console.WriteLine($"How many should be processed, maximum of {maxAvailablePlants}?");

                }

                Console.Write("> ");
                string input = Console.ReadLine();
                int quantity;
                try
                {
                    quantity = Int32.Parse(input);
                    if (group.Key == "Goat" && group.Count() > 0)
                    {
                        if (quantity <= maxAvailableGoats)
                        {
                            return quantity;
                        }
                        else throw new Exception();
                    }
                    else
                    {
                        if (quantity <= maxAvailablePlants)
                        {
                            return quantity;
                        }
                        else throw new Exception();
                    }
                }
                catch (Exception)
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
                Console.WriteLine($"Composter has space {capacity} plants and {capacity / 2} goat compost.");
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