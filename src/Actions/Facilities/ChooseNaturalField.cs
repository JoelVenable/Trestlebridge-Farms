using System;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Actions
{
  public class ChooseNaturalField
  {
    public static void CollectInput(Farm farm, IComposting seed)
    {
      Console.Clear();

      for (int i = 0; i < farm.NaturalFields.Count; i++)
      {
        Console.WriteLine($"{i + 1}. Natural Field: {farm.NaturalFields[i].Name}");
      }

      Console.WriteLine();

      // How can I output the type of animal chosen here?
      Console.WriteLine($"Place the seed where?");

      Console.Write("> ");

      try
      {
        int choice = Int32.Parse(Console.ReadLine()) - 1;

        farm.NaturalFields[choice].AddResource(seed);

      }
      catch (Exception ex)
      {
        Program.ShowMessage("Invalid Input");
      }



      /*
          Couldn't get this to work. Can you?
          Stretch goal. Only if the app is fully functional.
       */
      // farm.PurchaseResource<IGrazing>(animal, choice);

    }
  }
}