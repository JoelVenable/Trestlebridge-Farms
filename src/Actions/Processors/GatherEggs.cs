using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Models;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Processors;

namespace Trestlebridge.Actions
{
    public class GatherEggs
    {

        private static List<IGathering> _facilities = new List<IGathering>();

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
                // Select a house
                IGathering selectedHouse = SelectHouse(farm.EggGatherer);

                // Select quantity of resources to process
                int quantity = SelectQuantity(farm.EggGatherer.Capacity, selectedHouse);

                // // Add selected resources to hopper
                selectedHouse.SendToBasket(quantity, farm);


            } while (AddMore(farm.EggGatherer.Capacity));

            farm.EggGatherer.Gather();
        }


        private static IGathering SelectHouse(EggGatherer eg)
        {

            List<string> options = new List<string>();
            _facilities.ForEach(fac => {
                if (fac is ChickenHouse && eg.Capacity >= 7)
                {
                    options.Add($"{fac.Name} ({fac.NumAnimals} chickens)");
                }
                else if (fac is DuckHouse && eg.Capacity >= 6)
                {
                    options.Add($"{fac.Name} ({fac.NumAnimals} ducks)");
                }
                else if (fac is GrazingField gf && eg.Capacity >= 3)
                {
                    options.Add($"{fac.Name} ({gf.NumOstriches} ostriches)");
                }
                else
                {
                    eg.Gather();
                }
            });

            int choice = StandardMessages.ShowMenu(
                options, 
                "Which facility has the animals you want to collect eggs from?"
                );
            if (choice == 0) return null;
            else return _facilities[choice - 1];
        }

        private static int SelectQuantity(int capacity, IGathering house)
        {
            // int[] numbers = { capacity, group.Count() };

            int numAnimals = house.NumAnimals;
            int eggsPerAnimal = 0;

            if (house is GrazingField field)
            {
                numAnimals = field.NumOstriches;
                eggsPerAnimal = 3;
            }
            if (house is ChickenHouse)
            {
                eggsPerAnimal = 7;
            }
            if (house is DuckHouse)
            {
                eggsPerAnimal = 6;
            }

            double maxAvailable = Math.Floor((double)capacity / eggsPerAnimal);
            int[] animalArray = { (int)maxAvailable, numAnimals };
            int maxAnimals = animalArray.Min();

            return StandardMessages.GetNumber(
                $"Egg Gatherer can gather eggs from {maxAnimals} animals.\n\n" +
                "How many animals should be selected?",
                maxAnimals
                );
        }

        private static bool AddMore(int capacity)
        {
            if (_facilities.Count == 0) return false;
            if (capacity < 3) return false;

            return StandardMessages.GetYesOrNo(
                $"Egg Gatherer has {capacity} eggs available in capacity.\n\n" + 
                "Would you like to add more resources?"
                );
        }

        static private void UpdateFacilities(Farm farm)
        {
            _facilities = farm.Facilities.Where(fac =>
            {
                if (fac is IGathering gather && gather.NumAnimals > 0) return true;
                else return false;
            }).Cast<IGathering>().ToList();
        }
    }

}