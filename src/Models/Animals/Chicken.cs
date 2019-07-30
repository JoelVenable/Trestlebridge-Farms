using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Animals
{
    public class Chicken : IResource, IMeatProducing, IFeatherProducing, IEggProducing
    {
        private double _meatProduced = 1.7;

        private double _feathersProduced = .5;

        private int _eggsProduced = 7;

        public string Type { get; } = "Chicken";

        public double Butcher()
        {
            return _meatProduced;
        }

        public double Pluck()
        {
            return _feathersProduced;
        }
        public int EggsProduced { get; } = 7;


        public int Gather()
        {
            return EggsProduced;
        }

        public override string ToString()
        {
            return "chicken";
        }
    }

}
