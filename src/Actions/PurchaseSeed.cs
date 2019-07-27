using System;
using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Models.Plants;

namespace Trestlebridge.Actions
{
    public class PurchaseSeed
    {
        public static void CollectInput(Farm farm)
        {

            bool doOver;

            do
            {
                Console.WriteLine("1. Sesame");
                Console.WriteLine("2. Wildflower");
                Console.WriteLine("3. Sunflower");

                Console.WriteLine();
                Console.WriteLine("What seeds are you buying today?");

                Console.Write("> ");
                string choice = Console.ReadLine();

                doOver = false;
                List<PlowedField> availablePlowedFields = FilteredPlowedFields(farm);
                List<NaturalField> availableNaturalFields = FilteredNaturalFields(farm);
                int parsedChoice;
                try
                {

                    parsedChoice = Int32.Parse(choice);
                }
                catch (Exception)
                {
                    parsedChoice = 0;
                }

                switch (parsedChoice)
                {
                    case 1:
                        if (availablePlowedFields.Count == 0)
                        {
                            ChoosePlowedField.CollectInput(farm, new Sesame());
                        }
                        else StandardMessages.ShowMessage("No available facilities for this animal.");
                        break;
                    case 2:
                        if (availableNaturalFields.Count == 0)
                        {
                            ChooseNaturalField.CollectInput(farm, new Wildflower());
                        }
                        else StandardMessages.ShowMessage("No available facilities for this animal.");
                        break;
                    case 3:
                        if (availablePlowedFields.Count == 0 && availableNaturalFields.Count == 0)
                        {
                            StandardMessages.ShowMessage("No available facilities for this animal.");
                            break;
                        }
                        else
                        {

                            {
                                Console.Clear();
                                StandardMessages.DisplayBanner();
                                if (availableNaturalFields.Count > 0)
                                {
                                    Console.WriteLine("1. Natural Field");
                                }
                                if (availablePlowedFields.Count > 0 && availableNaturalFields.Count > 0)
                                {
                                    Console.WriteLine("2. Plowed Field");
                                }
                                if (availablePlowedFields.Count > 0 && availableNaturalFields.Count == 0)
                                {
                                    Console.WriteLine("1. Plowed Field");
                                }
                                Console.WriteLine();
                                Console.WriteLine("Choose What type of Field to plant your Sunflowers in:");
                                Console.Write("> ");
                                string fieldType = Console.ReadLine();

                                switch (Int32.Parse(fieldType))
                                {
                                    case 1:
                                        ChooseNaturalField.CollectInput(farm, new Sunflower());
                                        break;
                                    case 2:
                                        ChoosePlowedField.CollectInput(farm, new Sunflower());
                                        break;
                                    default:
                                        break;

                                }
                                break;
                            }
                        }
                    default:
                        StandardMessages.ShowMessage("Invalid selection.  Please choose again.");
                        doOver = true;
                        break;
                }

            } while (doOver);
        }

        static private List<PlowedField> FilteredPlowedFields(Farm farm)
        {
            if (farm.PlowedFields.Count == 0) return new List<PlowedField>();
            return farm.PlowedFields.Where(field => field.AvailableSpots > 0).ToList();
        }
        static private List<NaturalField> FilteredNaturalFields(Farm farm)
        {
            if (farm.NaturalFields.Count == 0) return new List<NaturalField>();
            return farm.NaturalFields.Where(field => field.AvailableSpots > 0).ToList();
        }
    }
}