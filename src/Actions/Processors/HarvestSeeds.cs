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
        private static List<PlowedField> _facilities = new List<PlowedField>();


        public static void CollectInput(Farm farm)
    {

            if (_facilities.Count == 0)
            {
                StandardMessages.ShowMessage("No available fields to process.");
                return;
            }
      do
      {

        // Select a field
        PlowedField selectedField = SelectField(farm);

        // Select a resource type
        var groups = selectedField.CreateGroup();
        IGrouping<string, ISeedProducing> selectedGroup = SelectResourceType(groups);

        // Select quantity of resources to process
        int quantity = SelectQuantity(farm.SeedHarvester.Capacity, selectedGroup);

        // Add selected resources to hopper
        selectedField.SendToHopper(quantity, selectedGroup.Key, farm);


      } while (AddMore(farm.SeedHarvester.Capacity));

      farm.SeedHarvester.Process();

      // if (selectedGroup.Key == "Sunflower")
      // {
      //   farm.SeedHarvester.SunflowerSeeds += ProcessedSeeds;
      // }
      // else if (selectedGroup.Key == "Sesame")
      // {
      //   farm.SeedHarvester.SesameSeeds += ProcessedSeeds;

      // }
    }


    private static PlowedField SelectField(Farm farm)
    {
      bool doOver;
      do
      {
        doOver = false;
        StandardMessages.DisplayBanner();

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
          return field;




        }
        catch (Exception)
        {
          StandardMessages.ShowMessage("Invalid Input");
        }
      }
      while (doOver);

      //  Should never get here.
      return null;
    }


    private static IGrouping<string, ISeedProducing> SelectResourceType(List<IGrouping<string, ISeedProducing>> groups)
    {
      StandardMessages.DisplayBanner();

      for (int i = 0; i < groups.Count; i++)
      {
        string s = groups.Count > 1 ? "s" : "";
        System.Console.WriteLine($"{i + 1}. {groups[i].Key}s ({groups[i].Count()} row{s} available)");
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

    private static int SelectQuantity(int capacity, IGrouping<string, ISeedProducing> group)
    {
      int[] numbers = { capacity, group.Count() };

      int maxAvailable = numbers.Min();
      StandardMessages.DisplayBanner();
      Console.WriteLine($"Selected {group.Key} with {group.Count()} rows of plants available to process.");
      Console.WriteLine($"Seed Harvester has {capacity} rows of available capacity.");

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
        catch (Exception)
        {
          StandardMessages.ShowMessage("Invalid entry");
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
        StandardMessages.DisplayBanner();
        Console.WriteLine($"Seed Harvester has {capacity} rows of plants available capacity.");
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
            StandardMessages.ShowMessage("Invalid input.  Please try again.");
            doOver = true;
            break;
        }

      } while (doOver);

      // Never runs.
      return false;

    }

        static private void UpdateFacilities(Farm farm)
        {
            _facilities = farm.PlowedFields.Where(field => field.currentPlants > 0).ToList();
        }
    }

}