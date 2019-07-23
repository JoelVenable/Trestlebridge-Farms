using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;



namespace Trestlebridge.Models.Processors
{
    public class EggGatherer
    {
        int ChickenEggs { get; set; } = 0;
        int DuckEggs { get; set; } = 0;
        int OstrichEggs { get; set; } = 0;

        private readonly int _capacity = 15; //eggs

        private List<IEggProducing> _basket = new List<IEggProducing>();

        public int Capacity
        {
            get
            {
                return _capacity - _basket.Count;
            }
        }  // eggs

        double TotalEggs
        {
            get
            {
                return ChickenEggs + DuckEggs + OstrichEggs;
            }
        }

        public void AddToBasket(IEggProducing resourceToAdd)
        {
            _basket.Add(resourceToAdd);
        }


        public void Gather()
        {
            int chickenEggs = 0;
            int duckEggs = 0;
            int ostrichEggs = 0;
            //  Display message about resources processed.

            _basket.ForEach(item =>
            {
                if (item is Chicken chicken) chickenEggs += chicken.Gather();
                if (item is Duck duck) duckEggs += duck.Gather();
                if (item is Ostrich ostrich) ostrichEggs += ostrich.Gather();
            });

            string message = "Gathering resources...\n";


            if (chickenEggs > 0) message += $"{chickenEggs} Sesame seeds were harvested.\n";
            if (duckEggs > 0) message += $"{duckEggs} Sunflower seeds were harvested.\n";
            if (ostrichEggs > 0) message += $"{ostrichEggs} Sunflower seeds were harvested.\n";

            Program.ShowMessage(message);

            ChickenEggs += chickenEggs;
            DuckEggs += duckEggs;
            OstrichEggs += ostrichEggs;
            _basket.Clear();
        }



    }
}
