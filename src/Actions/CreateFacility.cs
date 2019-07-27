using System;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    static public class CreateFacility
    {
        static private List<string> _options = new List<string>()
            {
                "Grazing Field",
                "Plowed Field",
                "Natural Field",
                "Chicken House",
                "Duck House"
            };
        static public void CollectInput(Farm farm)
        {


            int response = StandardMessages.ShowMenu(_options, "Choose what facility to create...");
            if (response == 0) return;  // Going back...
            else
            {
                IFacility newFacility = GetNewFacility(response);
                string facilityType = _options[response - 1];

                newFacility.Name = GetName(facilityType);

                farm.AddFacility(newFacility);

                Console.WriteLine($"Successfully added new {facilityType}: {newFacility.Name}");

            }


        }

        static private IFacility GetNewFacility(int selection)
        {
            switch (selection)
            {
                case 1:
                    return new GrazingField();
                case 2:
                    return new PlowedField();
                case 3:
                    return new NaturalField();
                case 4:
                    return new ChickenHouse();
                case 5:
                    return new DuckHouse();
                default:
                    return null;
            }
        }

        static private string GetName(string type)
        {
            return StandardMessages.GetInput(
                        $"Adding a new {type} to the farm.  What would you like to call it?"
                        );
        }
    }
}