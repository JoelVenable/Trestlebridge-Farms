using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class Compost
    {
        private static List<ICompostProducing> _facilities = null;
        public static void CollectInput(Farm farm)
        {
            do
            {
                UpdateFacilities(farm);
                if (_facilities.Count == 0)
                {
                    StandardMessages.ShowMessage("No available facilities to process.");
                    return;
                }

                // Select a field
                ICompostProducing selectedField = SelectField();

                // Select a resource type

                var groups = selectedField.CreateCompostList();

                IGrouping<string, IComposting> selectedGroup = SelectResourceType(groups);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.Composter.Capacity, selectedGroup);

                // Add selected resources to hopper
                selectedField.SendToComposter(quantity, selectedGroup.Key, farm);

                UpdateFacilities(farm);


            } while (AddMore(farm.Composter.Capacity));

            farm.Composter.Process();

        }


        private static ICompostProducing SelectField()
        {
            var options = new List<string>();
            _facilities.ForEach(fac =>
            {
                string type = "";
                if (fac is GrazingField gf) type = "goats";
                if (fac is NaturalField nf) type = "plants";
                options.Add($"{fac.Type}: {fac.Name} ({fac.CompostAmount} {type})");
            });

            int selection = StandardMessages.ShowMenu(options, "Select a facility to process compost from...");

            if (selection == 0) return null;
            return _facilities[selection - 1];
        }


        private static IGrouping<string, IComposting> SelectResourceType(List<IGrouping<string, IComposting>> groups)
        {
            List<string> options = new List<string>();

            groups.ForEach(group =>
            {
                string s = group.Count() > 1 ? "s" : "";
                string type = group.Key;
                if (type == "Goat") options.Add($"{type}s ({group.Count()} goat{s} available)");
                else options.Add($"{type}s ({group.Count()} row{s} available)");
            });

            int choice = StandardMessages.ShowMenu(options, "What resource should be processed?");
            if (choice == 0) return null;
            else return groups[choice - 1];
        }

        private static int SelectQuantity(int capacity, IGrouping<string, IComposting> group)
        {
            int quantityAvailable;
            string type;
            if (group.Key == "Goat")
            {
                type = "goats";
                quantityAvailable = new int[] { (capacity / 2), group.Count() }.Min();
            }
            else
            {
                quantityAvailable = new int[] { capacity, group.Count() }.Min();
                type = "rows of plants";
            }

            int desiredQuantity = StandardMessages.GetNumber(
                $"Selected {group.Key}, with {quantityAvailable} {type} available to process.\n\n" +
                "How many should be processed?",
                quantityAvailable
                );

            return desiredQuantity;
        }

        private static bool AddMore(int capacity)
        {
            if (capacity == 0) return false;
            if (_facilities.Count == 0) return false;

            return StandardMessages.GetYesOrNo(
                $"Composter has space {capacity} plants and {capacity / 2} goat compost.\n\n" +
                "Would you like to add more resources?"
                );
        }

        static private void UpdateFacilities(Farm farm)
        {
            _facilities = farm.Facilities.Where(fac =>
            {
                if (fac is ICompostProducing cp && cp.CompostAmount > 0) return true;
                else return false;
            }).Cast<ICompostProducing>().ToList();


        }
    }

}