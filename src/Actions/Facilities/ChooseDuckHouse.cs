using System;
using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class chooseDuckHouse
    {
        public static void CollectInput(List<DuckHouse> houses, Duck duck)
        {
            Console.Clear();

            for (int i = 0; i < houses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Duck House: {houses[i].Name} ({houses[i].NumAnimals} animals)");
            }

            Console.WriteLine();

            // How can I output the type of animal chosen here?
            Console.WriteLine($"Place the duck where?");

            Console.Write("> ");
            try
            {
                int choice = Int32.Parse(Console.ReadLine());

                houses[choice - 1].AddResource(duck);

            }
            catch (Exception)
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