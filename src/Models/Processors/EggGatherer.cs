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

        private List<IEggProducing> _animalsUsed = new List<IEggProducing>();

        private int _eggsInBasket = 0;

        public int Capacity
        {
            get
            {
                var capacity = _capacity;
                foreach (IEggProducing animal in _animalsUsed)
                {
                    capacity = capacity - animal.EggsProduced;
                }
                return capacity;
            }
        }  // eggs

        double TotalEggs
        {
            get
            {
                return ChickenEggs + DuckEggs + OstrichEggs;
            }
        }

        public void AddToBasket(int resourceToAdd)
        {
            _eggsInBasket += resourceToAdd;
        }

        public void GatheredAnimals(IEggProducing animal)
        {
            _animalsUsed.Add(animal);
        }


        public void Gather()
        {

            // Program.ShowMessage($"Proccessed {_eggsInBasket} eggs");


            int chickenEggs = 0;
            int duckEggs = 0;
            int ostrichEggs = 0;
            //  Display message about resources processed.

            _animalsUsed.ForEach(item =>
            {
                if (item is Chicken chicken) chickenEggs += chicken.Gather();
                if (item is Duck duck) duckEggs += duck.Gather();
                if (item is Ostrich ostrich) ostrichEggs += ostrich.Gather();
            });

            string message = "Gathering resources...\n";


            if (chickenEggs > 0) message += $"{chickenEggs} Chicken eggs were harvested.\n";
            if (duckEggs > 0) message += $"{duckEggs} Duck eggs were harvested.\n";
            if (ostrichEggs > 0) message += $"{ostrichEggs} Ostrich eggs were harvested.\n";

            Program.ShowMessage(message);

            ChickenEggs += chickenEggs;
            DuckEggs += duckEggs;
            OstrichEggs += ostrichEggs;
            _animalsUsed.Clear();
        }



    }
}
