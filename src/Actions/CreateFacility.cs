using System;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions {
    public class CreateFacility {
        public static void CollectInput (Farm farm) {
            Console.WriteLine ("1. Grazing field");
            Console.WriteLine ("2. Plowed field");

            Console.WriteLine ();
            Console.WriteLine ("Choose what you want to create");

            Console.Write ("> ");
            string input = Console.ReadLine ();

            switch (Int32.Parse(input))
            {
                case 1:
                    string name = PromptForFacilityName("Grazing Field");
                    farm.AddGrazingField(new GrazingField()
                    {
                        Name = name,
                    });
                    Program.ShowMessage($"Successfully added Grazing Field: {name}.");
                    break;
                default:
                    break;
            }
        }

        static string PromptForFacilityName(string facilityType)
        {
            Console.Clear();
            Program.DisplayBanner();
            Console.WriteLine($"Adding a new {facilityType} to the farm.  What would you like to call it?");
            Console.WriteLine();
            Console.Write("> ");
            return Console.ReadLine();
        }


    }
}