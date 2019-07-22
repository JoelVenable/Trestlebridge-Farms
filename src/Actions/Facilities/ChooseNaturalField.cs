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

            var fieldsWithSpace = farm.NaturalFields.FindAll(field => field.AvailableSpots > 0);
            for (int i = 0; i < fieldsWithSpace.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {fieldsWithSpace[i].Name} - Current Plants {fieldsWithSpace[i].currentPlants} | Available Rows: {fieldsWithSpace[i].AvailableSpots}");
                fieldsWithSpace[i].ListByType();

            }

            Console.WriteLine();

            Console.WriteLine($"Place the row of seeds where?");

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

        }
    }
}