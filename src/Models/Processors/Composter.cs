using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;
using Trestlebridge.Models.Plants;

namespace Trestlebridge.Models.Processors
{
    public class Composter
    {
        public Composter(FileHandler fh)
        {
            _kgCompost = fh.GetData("kgCompost", 0.0);
        }

        private double _kgCompost = 0;

        public int GoatCapacity { get; } = 4;

        public int PlantCapacity { get; } = 8;  // rows of plants

        private List<IComposting> _hopper = new List<IComposting>();

        public int Capacity
        {
            get
            {
                var goats = 0;
                var plants = 0;
                var grouped = _hopper.GroupBy(resource => resource.Type);
                foreach (var resource in grouped)
                {
                    if (resource.Key == "Goat")
                    {
                        var ammount = resource.Count();
                        goats = ammount * 2;

                    }
                    else
                    {
                        var ammount = resource.Count();
                        plants = ammount;
                    }
                };
                return 8 - (goats + plants);
            }
        }

        public void AddToHopper(IComposting resourceToAdd)
        {
            _hopper.Add(resourceToAdd);
        }

        public void Process()
        {
            double compost = 0;
            //  Display message about resources processed.

            _hopper.ForEach(item =>
            {
                if (item is Goat goat) compost += goat.Compost();
                if (item is Sunflower sunflower) compost += sunflower.Compost();
                if (item is Wildflower wildflower) compost += wildflower.Compost();
            });


            string message = "Harvesting resources...\n";
            message += $"{compost} Kilograms of compost were produced.\n";

            StandardMessages.ShowMessage(message);

            _kgCompost += compost;
            _hopper.Clear();

        }

        public override string ToString()
        {
            return $"Composter has processed {_kgCompost} Kgs of compost.\n";
        }

        public List<string> Export()
        {
            return new List<string>()
            {
                $"kgCompost,{_kgCompost}"
            };
        }
    }
}
