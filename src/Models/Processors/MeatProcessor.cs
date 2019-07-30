using System.Collections.Generic;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Processors
{
    public class MeatProcessor
    {
        private readonly int _capacity = 7;
        private double _chickenMeat = 0;
        private double _cowMeat = 0;
        private double _pigMeat = 0;
        private double _sheepMeat = 0;
        private double _ostrichMeat = 0;
        private double _duckMeat = 0;


        public MeatProcessor(FileHandler fh)
        {
            _chickenMeat = fh.GetData("chickenMeat", 0.0);
            _cowMeat = fh.GetData("cowMeat", 0.0);
            _pigMeat = fh.GetData("pigMeat", 0.0);
            _sheepMeat = fh.GetData("sheepMeat", 0.0);
            _ostrichMeat = fh.GetData("ostrichMeat", 0.0);
            _duckMeat = fh.GetData("duckMeat", 0.0);
        }


        private List<IMeatProducing> _hopper = new List<IMeatProducing>();

        public int Capacity
        {
            get
            {
                return _capacity - _hopper.Count;
            }
        }



        public void AddToHopper(IMeatProducing resourceToAdd)
        {
            _hopper.Add(resourceToAdd);
        }


        public void Process(Farm farm)
        {
            bool hasBirds = false;
            List<IFeatherProducing> birds = new List<IFeatherProducing>();
            double chickenMeat = 0;
            double cowMeat = 0;
            double pigMeat = 0;
            double sheepMeat = 0;
            double ostrichMeat = 0;

            double duckMeat = 0;
            //  Display message about resources processed.

            _hopper.ForEach(item =>
            {
                if (item is Chicken chicken)
                {
                    birds.Add(chicken);
                    chickenMeat += chicken.Butcher();
                }
                if (item is Cow cow) cowMeat += cow.Butcher();
                if (item is Pig pig) pigMeat += pig.Butcher();
                if (item is Sheep sheep) sheepMeat += sheep.Butcher();
                if (item is Ostrich ostrich) ostrichMeat += ostrich.Butcher();
                if (item is Duck duck)
                {
                    birds.Add(duck);
                    duckMeat += duck.Butcher();
                }
            });
            string message = "";
            if (birds.Count > 0)
            {
                message = farm.FeatherGatherer.Process(birds);
            }



            message += "Butchering Animals...\n\n";


            if (chickenMeat > 0) message += $"{chickenMeat} Kgs of chicken was processed.\n";
            if (cowMeat > 0) message += $"{cowMeat} Kgs of beef was processed.\n";
            if (pigMeat > 0) message += $"{pigMeat} Kgs of pork was processed.\n";
            if (sheepMeat > 0) message += $"{sheepMeat} Kgs of mutton was processed.\n";
            if (ostrichMeat > 0) message += $"{ostrichMeat} Kgs of ostrich meat was processed.\n";
            if (duckMeat > 0) message += $"{duckMeat} Kgs of duck meat was processed.\n";


            StandardMessages.ShowMessage(message);

            _chickenMeat += chickenMeat;
            _cowMeat += cowMeat;
            _pigMeat += pigMeat;
            _sheepMeat += sheepMeat;
            _ostrichMeat += ostrichMeat;
            _duckMeat += duckMeat;
            _hopper.Clear();
        }



        public override string ToString()
        {
            string output = "Meat Processor has processed: \n";
            output += BuildString(_chickenMeat, "chicken");
            output += BuildString(_cowMeat, "beef");
            output += BuildString(_pigMeat, "pork");
            output += BuildString(_sheepMeat, "mutton");
            output += BuildString(_ostrichMeat, "ostrich meat");
            output += BuildString(_duckMeat, "duck meat");
            output += "\n";
            return output;
        }

        static private string BuildString(double number, string type)
        {

            return $"    ({number} Kgs of {type}) \n";
        }


        public List<string> Export()
        {
            return new List<string>()
            {
                $"chickenMeat,{_chickenMeat}",
                $"duckMeat,{_duckMeat}",
                $"cowMeat,{_cowMeat}",
                $"pigMeat,{_pigMeat}",
                $"sheepMeat,{_sheepMeat}",
                $"ostrichMeat,{_ostrichMeat}",
            };
        }
    }
}
