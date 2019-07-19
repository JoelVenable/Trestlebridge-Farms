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
            Console.WriteLine("3. Natural field");
            Console.WriteLine("4. Chicken House");
            Console.WriteLine("5. Duck House");


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
                    string PlowedFieldName = Program.GetInput(
                        "Adding a new Plowed Field to the farm.  What would you like to call it?"
                        );
                    farm.AddPlowedField(new PlowedField()
                    {
                        Name = PlowedFieldName,
                    });
                    Program.ShowMessage($"Successfully added Plowed Field: {PlowedFieldName}."); break;
                case 3:
                    string NaturalFieldName = Program.GetInput(
                          "Adding a new Natural Field to the farm.  What would you like to call it?"
                          );
                    farm.AddNaturalField(new NaturalField()
                    {
                        Name = NaturalFieldName,
                    });
                    Program.ShowMessage($"Successfully added Natural Field: {NaturalFieldName}.");
                    break;
                case 4:
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
                case 5:
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