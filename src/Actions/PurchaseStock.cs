using System;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Actions
{
    public class PurchaseStock
    {
        public static void CollectInput(Farm farm)
        {
            Console.WriteLine("1. Chicken");
            Console.WriteLine("2. Cow");
            Console.WriteLine("3. Duck");
            Console.WriteLine("4. Goat");
            Console.WriteLine("5. Ostrich");
            Console.WriteLine("6. Pig");
            Console.WriteLine("7. Sheep");
            Console.WriteLine();
            Console.WriteLine("What are you buying today?");

            Console.Write("> ");
            string choice = Console.ReadLine();
            bool doOver;
            do
            {
                doOver = false;
                List<ChickenHouse> availableChickenHouses = FilterChickenHouses(farm);
                List<DuckHouse> availableDuckHouses = FilterDuckHouses(farm);
                List<GrazingField> availableGrazingFields = FilterGrazingFields(farm);

                switch (Int32.Parse(choice))
                {
                    case 1:
                        // Chicken
                        if (availableChickenHouses.Count > 0)
                        {
                            ChooseChickenHouse.CollectInput(availableChickenHouses, new Chicken());
                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 2:
                        // cow
                        if (availableGrazingFields.Count > 0)
                        {
                            ChooseGrazingField.CollectInput(availableGrazingFields, new Cow());
                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 3:
                        // duck
                        if (availableDuckHouses.Count > 0)
                        {
                            chooseDuckHouse.CollectInput(availableDuckHouses, new Duck());

                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 4:
                        // goat
                        if (availableGrazingFields.Count > 0)
                        {
                        ChooseGrazingField.CollectInput(availableGrazingFields, new Goat());

                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 5:
                        // ostrich
                        if (availableGrazingFields.Count > 0)
                        {
                            ChooseGrazingField.CollectInput(availableGrazingFields, new Ostrich());

                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 6:
                        // pig
                        if (availableGrazingFields.Count > 0)
                        {
                            ChooseGrazingField.CollectInput(availableGrazingFields, new Pig());

                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    case 7:
                        // sheep
                        if (availableGrazingFields.Count > 0)
                        {
                            ChooseGrazingField.CollectInput(availableGrazingFields, new Sheep());
                        
                        }
                        else Program.ShowMessage("No available facilities for this animal.");
                        break;
                    default:
                        Program.ShowMessage("Invalid selection.  Please choose again.");
                        doOver = true;
                        break;
                }
            } while (doOver);


        }
            static private List<ChickenHouse> FilterChickenHouses(Farm farm)
            {
                if (farm.ChickenHouses.Count == 0) return new List<ChickenHouse>();
            return farm.ChickenHouses.Where(house => house.AvailableSpots > 0).ToList();
            }

        static private List<DuckHouse> FilterDuckHouses(Farm farm)
        {
            if (farm.DuckHouses.Count == 0) return new List<DuckHouse>();
            return farm.DuckHouses.Where(house => house.AvailableSpots > 0).ToList();
        }

        static private List<GrazingField> FilterGrazingFields(Farm farm)
        {
            if (farm.GrazingFields.Count == 0) return new List<GrazingField>();
            return farm.GrazingFields.Where(field => field.AvailableSpots > 0).ToList();
        }
    }
}