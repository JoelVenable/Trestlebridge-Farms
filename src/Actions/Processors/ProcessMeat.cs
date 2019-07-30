using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;

namespace Trestlebridge.Actions
{
    public class ProcessMeat
    {
        private static List<IMeatFacility> _facilities = new List<IMeatFacility>();
        public static void CollectInput(Farm farm)
        {
            bool doOver = false;
            do
            {
                doOver = false;

                UpdateFacilities(farm);


                if (_facilities.Count > 0)
                {
                    IMeatFacility selectedFacility = SelectFacility(_facilities);

                    // Select a resource type
                    var groups = selectedFacility.CreateMeatGroup();
                    IGrouping<string, IMeatProducing> selectedGroup = SelectResourceType(groups);

                    // Select quantity of resources to process
                    int quantity = SelectQuantity(farm.MeatProcessor.Capacity, selectedGroup);

                    // Add selected resources to hopper
                    selectedFacility.SendToHopper(quantity, selectedGroup.Key, farm);

                    UpdateFacilities(farm);
                    if (_facilities.Count > 0) doOver = AddMore(farm.MeatProcessor.Capacity);
                }
                else
                {
                    StandardMessages.ShowMessage("No available facilities to process.");
                    return;
                }


            } while (doOver);

            farm.MeatProcessor.Process(farm);

        }


        private static IMeatFacility SelectFacility(List<IMeatFacility> facilities)
        {
            List<string> options = new List<string>();
            facilities.ForEach(fac => options.Add(fac.ToString()));

            int choice = StandardMessages.ShowMenu(options, "Which facility has the animals you want to process?");
            if (choice == 0) return null;
            return facilities[choice - 1];
        }


        private static IGrouping<string, IMeatProducing> SelectResourceType(List<IGrouping<string, IMeatProducing>> groups)
        {
            if (groups.Count == 1) return groups[0];

            List<string> options = groups.Select(group => $"{ group.Key}s: ({ group.Count()} available)").ToList();

            int choice = StandardMessages.ShowMenu(options, "What type of animal should be processed?");
            if (choice == 0) return null;
            return groups[choice - 1];
        }

        private static int SelectQuantity(int capacity, IGrouping<string, IMeatProducing> group)
        {
            int maxAvailable = new int[] { capacity, group.Count() }.Min();
            int desiredQuantity = StandardMessages.GetNumber(
                $"Selected {group.Key} with {group.Count()} animals available to process.\n" +
                $"Meat processor has {capacity} spaces of available capacity.\n\n" +
                $"How many should be processed, maximum of {maxAvailable}?",
                maxAvailable
                );
            return desiredQuantity;
        }

        private static bool AddMore(int capacity)
        {
            if (capacity == 0) return false;
            return StandardMessages.GetYesOrNo(
                $"Meat processor now has {capacity} spaces of available capacity.\n\n" +
                "Would you like to add more resources?"
                );
        }

        static private void UpdateFacilities(Farm farm)
        {
            _facilities = farm.Facilities
                .Where(fac =>
                {
                    if (fac is IMeatFacility mf && mf.NumMeatAnimals > 0) return true;
                    else return false;
                }).Cast<IMeatFacility>()
                .ToList();

        }
    }

}