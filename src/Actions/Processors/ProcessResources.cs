using System;
using Trestlebridge.Models;

namespace Trestlebridge.Actions
{
    public class ProcessResources
    {
        public static void CollectInput(Farm farm)
        {
            Console.WriteLine("1. Seed Harvester");
            Console.WriteLine("2. Meat Processor");
            Console.WriteLine("3. Egg Gatherer");
            Console.WriteLine("4. Composter");
            Console.WriteLine("5. Feather Harvester");
            Console.WriteLine();
            Console.WriteLine("Choose equipment to use");

            Console.Write("> ");
            string choice = Console.ReadLine();
            bool doOver;
            do
            {
                doOver = false;
                switch (Int32.Parse(choice))
                {
                    case 1:
                        HarvestSeeds.CollectInput(farm);
                        break;
                    case 2:
                        //ProcessMeat.CollectInput(farm);
                        break;
                    case 3:
                       // GatherEggs.CollectInput(farm);
                        break;
                    case 4:
                        //Compost.CollectInput(farm);
                        break;
                    case 5:
                        //GatherFeathers.CollectInput(farm);
                        break;
                    default:
                        Program.ShowMessage("Invalid selection.  Please choose again.");
                        doOver = true;
                        break;
                }
            } while (doOver);

        }
    }
}