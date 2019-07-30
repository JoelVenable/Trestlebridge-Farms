using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Animals
{
    public class Duck : IResource, IEggProducing, IFeatherProducing, IMeatProducing
    {
        private readonly int _eggsProduced = 6;

        private double _meatProduced = 1.2;

        private double _feathersProduced = 0.75;


        public double GrassPerDay { get; set; } = 0.8;

        public int EggsProduced
        {
            get
            {
                return _eggsProduced;
            }
        }

        public string Type { get; } = "Duck";


        public double Butcher()
        {
            return _meatProduced;
        }

        public double Pluck()
        {
            return _feathersProduced;
        }

        public int Gather()
        {
            return EggsProduced;
        }


        double IFeatherProducing.Pluck()
        {
            return 0.75;
        }

        public override string ToString()
        {
            return "duck";
        }

    }
}