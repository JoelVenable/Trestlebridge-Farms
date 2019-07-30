using System;
using System.Collections.Generic;
using Trestlebridge.Models;

namespace Trestlebridge.Actions
{
    public static class MainMenu
    {
        private static void FarmReport(Farm farm)
        {
            StandardMessages.DisplayBanner();
            Console.WriteLine(farm);
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Press return key to go back to main menu.");
            Console.ReadLine();
        }
        public static void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Farm Trestlebridge = new Farm();



            while (true)
            {
                List<string> menuOptions = new List<string>()
                {
                    "Create Facility"
                };

                if (Trestlebridge.Facilities.Count > 0)
                {
                    menuOptions.Add("Purchase Resources");
                    menuOptions.Add("Process Resources");

                }

                menuOptions.Add("Display Farm Status Report");



                int response = StandardMessages.ShowMenu(menuOptions, "Choose a FARMS option...", "Quit Program.");

                if (response == 0)
                {
                    //  Quit program.
                    Console.WriteLine("Today is a great day for farming");
                    Trestlebridge.Save();

                    return;
                }

                switch (menuOptions[response - 1])
                {
                    case "Create Facility":
                        CreateFacility.CollectInput(Trestlebridge);
                        break;
                    case "Purchase Resources":
                        PurchaseResource.CollectInput(Trestlebridge);
                        break;
                    case "Display Farm Status Report":
                        FarmReport(Trestlebridge);
                        break;
                    case "Process Resources":
                        ProcessResources.CollectInput(Trestlebridge);
                        break;
                    default:
                        break;

                };

            }
        }
    }
}
