using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class HarvestSeeds
    {
        private static List<PlowedField> _facilities = new List<PlowedField>();


        public static void CollectInput(Farm farm)
        {

            if (_facilities.Count == 0)
            {
                StandardMessages.ShowMessage("No available fields to process.");
                return;
            }
            do
            {

                // Select a field
                PlowedField selectedField = SelectField();

                // Select a resource type
                var groups = selectedField.CreateGroup();
                IGrouping<string, ISeedProducing> selectedGroup = SelectResourceType(groups);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.SeedHarvester.Capacity, selectedGroup);

                // Add selected resources to hopper
                selectedField.SendToHopper(quantity, selectedGroup.Key, farm);


            } while (AddMore(farm.SeedHarvester.Capacity));

            farm.SeedHarvester.Process();

            // if (selectedGroup.Key == "Sunflower")
            // {
            //   farm.SeedHarvester.SunflowerSeeds += ProcessedSeeds;
            // }
            // else if (selectedGroup.Key == "Sesame")
            // {
            //   farm.SeedHarvester.SesameSeeds += ProcessedSeeds;

            // }
        }


        private static PlowedField SelectField()
        {
            List<string> options = new List<string>();
            _facilities.ForEach(fac => options.Add($"{fac.Name} ({fac.currentPlants} plants)"));

            int selection = StandardMessages.ShowMenu(options, "Which facility has the plants to be processed?");
            if (selection == 0) return null;  // Go back.
            else return _facilities[selection - 1];


        }


        private static IGrouping<string, ISeedProducing> SelectResourceType(
            List<IGrouping<string, ISeedProducing>> groups
            )
        {
            List<string> options = new List<string>();
            string s = groups.Count > 1 ? "s" : "";

            groups.ForEach(group => options.Add($"{group.Key}s ({group.Count()} row{s} available)"));
            int choice = StandardMessages.ShowMenu(options, "What resource should be processed?");
            if (choice == 0) return null;
            return groups[choice - 1];

        }

        private static int SelectQuantity(int capacity, IGrouping<string, ISeedProducing> group)
        {
            int maxAvailable = new int[] { capacity, group.Count() }.Min();

            int response = StandardMessages.GetNumber(
                $"Selected {group.Key} with {group.Count()} rows of plants available to process.\n" +
                $"Seed Harvester has {capacity} rows of available capacity.\n\n" +
                $"How many should be processed, maximum of {maxAvailable}?",
                maxAvailable
                );
            return response;
        }

        private static bool AddMore(int capacity)
        {
            if (capacity == 0) return false;

            return StandardMessages.GetYesOrNo(
                $"Seed Harvester has {capacity} rows of plants available capacity.\n\n" +
                "Would you like to add more resources?"
                );
        }

        static private void UpdateFacilities(Farm farm)
        {
            _facilities = farm.Facilities.Where(fac =>
            {
                if (fac is PlowedField pf && pf.currentPlants > 0) return true;
                else return false;
            }).Cast<PlowedField>().ToList();
        }
    }

}