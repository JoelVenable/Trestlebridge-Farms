using System;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class CreateFacility
    {
        public static void CollectInput(Farm farm)
        {
            Console.WriteLine("1. Grazing field");
            Console.WriteLine("2. Plowed field");

            //Duck House
            Console.WriteLine("4. Duck House");


            Console.WriteLine();
            Console.WriteLine("Choose what you want to create");

            Console.Write("> ");
            string input = Console.ReadLine();

            switch (Int32.Parse(input))
            {
                case 1:
                    farm.AddGrazingField(new GrazingField());
                    break;
                case 2:
                    farm.AddPlowedField(new PlowedField());
                    break;

                //Duck House 
                case 4:
                    Console.WriteLine("Adding a new Duck House to the farm. What would you like to call it?");
                    Console.WriteLine();
                    Console.Write(">");


                    farm.AddDuckHouse(new DuckHouse()
                    {
                        Name = name;
            });
            break;
            default:
                    break;
        }
    }
}
}