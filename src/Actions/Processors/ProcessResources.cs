using System.Collections.Generic;
using Trestlebridge.Models;

namespace Trestlebridge.Actions
{
    public class ProcessResources
    {
        public static void CollectInput(Farm farm)
        {
            List<string> options = new List<string>();


            //  TODO: Check if facilites can process resources before showing the processor.
            options.Add("Seed Harvester");
            options.Add("Meat Processor");
            options.Add("Egg Gatherer");
            options.Add("Composter");

            int choice = StandardMessages.ShowMenu(options, "Choose Equipment to use...");
            switch (choice)
            {
                case 1:
                    HarvestSeeds.CollectInput(farm);
                    break;
                case 2:
                    ProcessMeat.CollectInput(farm);
                    break;
                case 3:
                    GatherEggs.CollectInput(farm);
                    break;
                case 4:
                    Compost.CollectInput(farm);
                    break;
                default:
                    break;
            }

        }
    }
}