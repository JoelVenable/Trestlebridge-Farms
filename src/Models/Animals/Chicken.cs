using System;
using System.Collections.Generic;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Animals
{
    public class Chicken : IResource, IMeatProducing, IFeatherProducing, IEggProducing
    {
        private Guid _id = Guid.NewGuid();
        private int _meatProduced = 7;

        private double _feathersProduced = .5;

        private double _eggsProduced = 1.7;
        public string Type
        { get; set; }

        public double Butcher()
        {
            throw new NotImplementedException();
        }

        public int Gather()
        {
            throw new NotImplementedException();
        }

        public double Pluck()
        {
            throw new NotImplementedException();
        }
    }

}
