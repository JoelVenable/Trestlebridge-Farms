using System;
using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
  public class ChooseGrazingField
  {
    public static void CollectInput(List<GrazingField> field, IGrazing animal)
    {
      Console.Clear();

      for (int i = 0; i < field.Count; i++)
      {
        Console.WriteLine($"{i + 1}. Grazing Field: {houses[i].Name} ({field[i].NumAnimals} animals)");
      }

      Console.WriteLine();

      // How can I output the type of animal chosen here?
      Console.WriteLine($"Place the animal where?");

      Console.Write("> ");
      try
      {
        int choice = Int32.Parse(Console.ReadLine());

        field[choice - 1].AddResource(animal);

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