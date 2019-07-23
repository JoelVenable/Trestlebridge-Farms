using System;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Plants
{
    public class Sunflower : IResource, ISeedProducing, IComposting
    {
        private int _seedsProduced = 50;

        private double _kgComposted = 1.66;
        public string Type { get; } = "Sunflower";

        public double Compost()
        {
            return _kgComposted;
        }

        public int Harvest()
        {
            return _seedsProduced;
        }

        public override string ToString()
        {
            return $"Sunflower. Yum!";
        }
    }
}