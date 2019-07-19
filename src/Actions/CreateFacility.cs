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
            //Chicken House
            Console.WriteLine("3. Chicken House");
            //Duck House
            Console.WriteLine("4. Duck House");


            Console.WriteLine();
            Console.WriteLine("Choose what you want to create");

            Console.Write("> ");
            string input = Console.ReadLine();

            switch (Int32.Parse(input))
            {
                case 1:
                    string GrazingFieldName = Program.GetInput(
                        "Adding a new Grazing Field to the farm.  What would you like to call it?"
                        );
                    farm.AddGrazingField(new GrazingField()
                    {
                        Name = GrazingFieldName,
                    });
                    Program.ShowMessage($"Successfully added Grazing Field: {GrazingFieldName}.");
                    break;
                case 2:
                    // farm.AddPlowedField(new PlowedField());
                    break;

                //Chicken House
                case 3:
                    string ChickenHouseName = Program.GetInput(
                        "Adding a new Chicken House to the farm.  What would you like to call it?"
                        );
                    farm.AddChickenHouse(new ChickenHouse()
                    {
                        Name = ChickenHouseName
                    });
                    Program.ShowMessage($"Successfully added Chicken House: {ChickenHouseName}.");
                    break;

                //Duck House
                case 4:
                    var DuckHouseName = Program.GetInput("Adding a new Duck House to the farm.  What would you like to call it?");
                    farm.AddDuckHouse(new DuckHouse()
                    {
                        Name = DuckHouseName
                    });
                    Program.ShowMessage($"Successfully added Duck House: {DuckHouseName}.");

                    break;
                default:
                    break;
            }
        }
    }
}