using System.Collections.Generic;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;



namespace Trestlebridge.Models.Processors
{
    public class EggGatherer
    {
        private int _chickenEggs = 0;
        private int _duckEggs = 0;
        private int _ostrichEggs = 0;

        public EggGatherer(FileHandler fh)
        {
            _chickenEggs = fh.GetData("chickenEggs", 0);
            _duckEggs = fh.GetData("duckEggs", 0);
            _ostrichEggs = fh.GetData("ostrichEggs", 0);
        }

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
                    capacity -= animal.EggsProduced;
                }
                return capacity;
            }
        }  // eggs

        double TotalEggs
        {
            get
            {
                return _chickenEggs + _duckEggs + _ostrichEggs;
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

            StandardMessages.ShowMessage(message);

            _chickenEggs += chickenEggs;
            _duckEggs += duckEggs;
            _ostrichEggs += ostrichEggs;
            _animalsUsed.Clear();
        }


        public override string ToString()
        {
            string output = "Egg gatherer has processed: \n";
            output += BuildString(_chickenEggs, "Chicken");
            output += BuildString(_duckEggs, "Duck");
            output += BuildString(_ostrichEggs, "Ostrich");
            output += "\n";
            return output;
        }

        static private string BuildString(int number, string type)
        {
            if (number == 0) return null;
            string s = number > 1 ? "s" : "";

            return $"    ({number} {type} egg{s}) \n";
        }
        public List<string> Export()
        {
            return new List<string>()
            {
                $"chickenEggs,{_chickenEggs}",
                $"duckEggs,{_duckEggs}",
                $"ostrichEggs,{_ostrichEggs}"

            };
        }
    }
}
