using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class ChooseChickenHouse
    {
        public static void CollectInput(List<ChickenHouse> houses, Chicken chicken)
        {
            Console.Clear();

            for (int i = 0; i < houses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ChickenHouse ({houses[i].NumAnimals} animals)");
            }

            Console.WriteLine();

            // How can I output the type of animal chosen here?
            Console.WriteLine("Place the chicken where?");

            Console.Write("> ");
            int choice = Int32.Parse(Console.ReadLine());

            houses[choice - 1].AddResource(chicken);

            /*
                Couldn't get this to work. Can you?
                Stretch goal. Only if the app is fully functional.
             */
            // farm.PurchaseResource<IGrazing>(animal, choice);

        }
    }
}