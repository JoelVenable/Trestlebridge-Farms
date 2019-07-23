using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Processors
{
    public class MeatProcessor
    {
        private readonly int _capacity = 7;
        double ChickenMeat { get; set; } = 0;
        double CowMeat { get; set; } = 0;
        double PigMeat { get; set; } = 0;
        double SheepMeat { get; set; } = 0;
        double OstrichMeat { get; set; } = 0;

        double DuckMeat { get; set; } = 0;



        double TotalMeat
        {
            get
            {
                return ChickenMeat + CowMeat + PigMeat + SheepMeat + OstrichMeat + DuckMeat;
            }
        }


        private List<IMeatProducing> _hopper = new List<IMeatProducing>();

        public int Capacity
        {
            get
            {
                return _capacity - _hopper.Count;
            }
        }  // rows of plants



        public void AddToHopper(IMeatProducing resourceToAdd)
        {
            _hopper.Add(resourceToAdd);
        }


        public void Process()
        {
            double chickenMeat = 0;
            double cowMeat = 0;
            double pigMeat = 0;
            double sheepMeat = 0;
            double ostrichMeat = 0;

            double duckMeat = 0;
            //  Display message about resources processed.

            _hopper.ForEach(item =>
            {
                if (item is Chicken chicken) chickenMeat += chicken.Butcher();
                if (item is Cow cow) cowMeat += cow.Butcher();
                if (item is Pig pig) pigMeat += pig.Butcher();
                if (item is Sheep sheep) sheepMeat += sheep.Butcher();
                if (item is Ostrich ostrich) ostrichMeat += ostrich.Butcher();
                if (item is Duck duck) duckMeat += duck.Butcher();
            });

            string message = "Butchering Animals...\n";


            if (chickenMeat > 0) message += $"{chickenMeat} Kg of Chicken meat was processed.\n";
            if (cowMeat > 0) message += $"{cowMeat} Kg of Cow meat was processed.\n";
            if (pigMeat > 0) message += $"{pigMeat} Kg of Pig meat was processed.\n";
            if (sheepMeat > 0) message += $"{sheepMeat} Kg of Sheep meat was processed.\n";
            if (ostrichMeat > 0) message += $"{ostrichMeat} Kg of Ostrich meat was processed.\n";
            if (duckMeat > 0) message += $"{duckMeat} Kg of Duck meat was processed.\n";


            Program.ShowMessage(message);

            ChickenMeat += chickenMeat;
            CowMeat += cowMeat;
            PigMeat += pigMeat;
            SheepMeat += sheepMeat;
            OstrichMeat += ostrichMeat;
            DuckMeat += duckMeat;
            _hopper.Clear();
        }





    }
}
