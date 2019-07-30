using System.Collections.Generic;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Processors
{
    public class FeatherGatherer
    {
        private double _chickenFeathers = 0;
        private double _duckFeathers = 0;

        public FeatherGatherer(FileHandler fh)
        {
            _chickenFeathers = fh.GetData("chickenFeathers", 0);
            _duckFeathers = fh.GetData("duckFeathers", 0);
        }

        public int Capacity { get; } = 5;



        public string Process(List<IFeatherProducing> birds)
        {

            double chickenFeathers = 0;
            double duckFeathers = 0;
            birds.ForEach(bird =>
            {
                if (bird is Chicken chicken)
                {
                    chickenFeathers += chicken.Pluck();
                }
                if (bird is Duck duck)
                {
                    duckFeathers += duck.Pluck();
                }
            });

            _chickenFeathers += chickenFeathers;
            _duckFeathers += duckFeathers;

            string message = "Plucking birds...\n\n";

            if (chickenFeathers > 0) message += $"{chickenFeathers} Kg of Chicken feathers were plucked.\n";
            if (duckFeathers > 0) message += $"{duckFeathers} Kg of Duck feathers were plucked.\n\n";

            return message;
        }



        public override string ToString()
        {
            string output = "Feather Gatherer has processed: \n";
            output += BuildString(_chickenFeathers, "Chicken");
            output += BuildString(_duckFeathers, "Duck");
            output += "\n";
            return output;
        }

        static private string BuildString(double number, string type)
        {

            return $"    ({number} Kgs of {type} feathers) \n";
        }


        public List<string> Export()
        {
            return new List<string>()
            {
                $"chickenFeathers,{_chickenFeathers}",
                $"duckFeathers,{_duckFeathers}"

            };
        }


    }
}
