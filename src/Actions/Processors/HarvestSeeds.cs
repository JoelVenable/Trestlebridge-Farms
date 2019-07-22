using System;
using Trestlebridge.Models;

namespace Trestlebridge.Actions
{
    public class HarvestSeeds
    {
        public static void CollectInput(Farm farm)
        {
            for (var i = 0; i < farm.PlowedFields.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {farm.PlowedFields[i].Name} ({farm.PlowedFields[i].currentPlants})");
            }
            Console.WriteLine();
            Console.WriteLine("Which facility has the animals you want to process?");

            Console.Write("> ");
            string fieldChoice = Console.ReadLine();

            Program.DisplayBanner();



        }
    }
}