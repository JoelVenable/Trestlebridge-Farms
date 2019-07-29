using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Plants;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class PurchaseResource
    {
        public static void CollectInput(Farm farm)
        {
            IFacility selectedFacility = SelectFacility(farm);

            if (selectedFacility == null) return;  // Back to main menu...

            List<string> options = GetOptions(selectedFacility);

            int choice;
            if (options.Count > 1)
            {
                choice = StandardMessages.ShowMenu(options, "What type of resource are you buying today?");
            }
            else choice = 1;  //  Auto selects chickens or ducks.

            if (choice == 0) return; // Go back...
            IResource newResource = GetResourceType(options[choice - 1]);

            StandardMessages.ShowMessage($"Adding a {newResource.Type} to the {selectedFacility.Type}: {selectedFacility.Name}");


            //  TODO: Add multiple resources at the same time...

            selectedFacility.AddResource(newResource);


        }

        static private IResource GetResourceType(string resourceType)
        {
            switch (resourceType)
            {
                case "Chicken":
                    return new Chicken();
                case "Duck":
                    return new Duck();
                case "Cow":
                    return new Cow();
                case "Goat":
                    return new Goat();
                case "Ostrich":
                    return new Ostrich();
                case "Pig":
                    return new Pig();
                case "Sheep":
                    return new Sheep();
                case "Sesame":
                    return new Sesame();
                case "Wildflower":
                    return new Wildflower();
                case "Sunflower":
                    return new Sunflower();
                default:
                    return null;
            }
        }

        static private IFacility SelectFacility(Farm farm)
        {
            var options = new List<string>();

            farm.Facilities
                .Where(fac => fac.AvailableSpots > 0)
                .ToList()
                .ForEach(fac => options.Add(fac.ToString()));

            int selection = StandardMessages.ShowMenu(options, "Please select a facility to add resources to:");

            if (selection == 0) return null;

            return farm.Facilities[selection - 1];
        }

        static private List<string> GetOptions(IFacility fac)
        {
            var output = new List<string>();
            if (fac is ChickenHouse)
            {
                output.Add("Chicken");
            }
            if (fac is DuckHouse)
            {
                output.Add("Duck");
            }
            if (fac is GrazingField)
            {
                output.Add("Cow");
                output.Add("Goat");
                output.Add("Ostrich");
                output.Add("Pig");
                output.Add("Sheep");
            }
            if (fac is NaturalField)
            {
                output.Add("Sunflower");
                output.Add("Wildflower");
            }
            if (fac is PlowedField)
            {
                output.Add("Sesame");
                output.Add("Wildflower");
            }
            return output;
        }

    }
}