using System;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Plants
{
    public class Wildflower : IResource, IComposting
    {
        private double _kgComposted = 2.33;
        public string Type { get; } = "Wildflower";

        public double Compost()
        {
            return _kgComposted;
        }

        public override string ToString()
        {
            return $"Sesame. Yum!";
        }
    }
}