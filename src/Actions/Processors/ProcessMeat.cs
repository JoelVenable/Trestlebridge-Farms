using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Actions
{
  public class ProcessMeat
  {
    private static List<IMeatFacility> _facilities = new List<IMeatFacility>();
    public static void CollectInput(Farm farm)
    {
      bool doOver = false;
      do
      {
        doOver = false;

        UpdateFacilities(farm);


        if (_facilities.Count > 0)
        {
          IMeatFacility selectedFacility = SelectFacility(_facilities);

          // Select a resource type
          var groups = selectedFacility.CreateMeatGroup();
          IGrouping<string, IMeatProducing> selectedGroup = SelectResourceType(groups);

          // Select quantity of resources to process
          int quantity = SelectQuantity(farm.MeatProcessor.Capacity, selectedGroup);

          // Add selected resources to hopper
          selectedFacility.SendToHopper(quantity, selectedGroup.Key, farm);

          UpdateFacilities(farm);
          if (_facilities.Count > 0) doOver = AddMore(farm.MeatProcessor.Capacity);
        }
        else
        {
          StandardMessages.ShowMessage("No available facilities to process.");
          return;
        }


      } while (doOver);

      farm.MeatProcessor.Process(farm);

    }


    private static IMeatFacility SelectFacility(List<IMeatFacility> facilities)
    {
      bool doOver;
      do
      {
        doOver = false;
        StandardMessages.DisplayBanner();

        for (int i = 0; i < facilities.Count; i++)
        {
          Console.Write($"{i + 1}. ");
          if (facilities[i] is ChickenHouse ch)
          {

            Console.WriteLine(ch);
          };
          if (facilities[i] is DuckHouse dh)
          {

            Console.WriteLine(dh);
          };
          if (facilities[i] is GrazingField gf)
          {

            Console.WriteLine(gf);
          };
        }
        Console.WriteLine();
        Console.WriteLine("Which facility has the animals you want to process?");

        Console.Write("> ");
        string fieldChoice = Console.ReadLine();
        int choice;
        try
        {
          choice = Int32.Parse(fieldChoice);
          var field = facilities[choice - 1];
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


    private static IGrouping<string, IMeatProducing> SelectResourceType(List<IGrouping<string, IMeatProducing>> groups)
    {
      if (groups.Count == 1) return groups[0];

      StandardMessages.DisplayBanner();
      for (int i = 0; i < groups.Count; i++)
      {
        System.Console.WriteLine($"{i + 1}. {groups[i].Key}s: ({groups[i].Count()} available)");
      }
      bool doOver;

      do
      {
        doOver = false;
        Console.WriteLine();
        Console.WriteLine("What type of animal should be processed?");

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

    private static int SelectQuantity(int capacity, IGrouping<string, IMeatProducing> group)
    {
      int[] numbers = { capacity, group.Count() };

      int maxAvailable = numbers.Min();
      StandardMessages.DisplayBanner();
      Console.WriteLine($"Selected {group.Key} with {group.Count()} animals available to process.");
      Console.WriteLine($"Meat processor has {capacity} spaces of available capacity.");

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
        Console.WriteLine($"Meat processor now has {capacity} spaces of available capacity.");
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
      _facilities = farm.Facilities
          .Where(fac =>
          {
              if (fac is IMeatFacility mf && mf.NumMeatAnimals > 0) return true;
              else return false;
          }).Cast<IMeatFacility>()
          .ToList();

    }
  }

}