using System.Collections.Generic;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Plants;

namespace Trestlebridge.Models.Processors
{
    public class SeedHarvester
    {
        private int _sesameSeeds = 0;
        private int _sunflowerSeeds = 0;

        public SeedHarvester(FileHandler fh)
        {
            _sesameSeeds = fh.GetData("sesameSeeds", 0);
            _sunflowerSeeds = fh.GetData("sunflowerSeeds", 0);
        }
        private readonly int _capacity = 5;

        private List<ISeedProducing> _hopper = new List<ISeedProducing>();

        public int Capacity
        {
            get
            {
                return _capacity - _hopper.Count;
            }
        }  // rows of plants

        int TotalSeeds
        {
            get
            {
                return _sesameSeeds + _sunflowerSeeds;
            }
        }

        public void AddToHopper(ISeedProducing resourceToAdd)
        {
            _hopper.Add(resourceToAdd);
        }


        public void Process()
        {
            int sunflowerSeeds = 0;
            int sesameSeeds = 0;
            //  Display message about resources processed.

            _hopper.ForEach(item =>
            {
                if (item is Sesame sesame) sesameSeeds += sesame.Harvest();
                if (item is Sunflower sunflower) sunflowerSeeds += sunflower.Harvest();
            });

            string message = "Harvesting resources...\n";


            if (sesameSeeds > 0) message += $"{sesameSeeds} Sesame seeds were harvested.\n";
            if (sunflowerSeeds > 0) message += $"{sunflowerSeeds} Sunflower seeds were harvested.\n";

            StandardMessages.ShowMessage(message);

            _sesameSeeds += sesameSeeds;
            _sunflowerSeeds += sunflowerSeeds;
            _hopper.Clear();
        }


        public override string ToString()
        {
            string output = "Seed Processor has processed: \n";
            output += BuildString(_sesameSeeds, "sesame");
            output += BuildString(_sunflowerSeeds, "sunflower");
            output += "\n";
            return output;
        }

        static private string BuildString(double number, string type)
        {
            string s = number > 1 ? "s" : "";
            return $"    ({number} {type} seed{s}) \n";
        }

        public List<string> Export()
        {
            return new List<string>()
            {
                $"sesameSeeds,{_sesameSeeds}",
                $"sunflowerSeeds,{_sunflowerSeeds}",
            };
        }
    }
}
