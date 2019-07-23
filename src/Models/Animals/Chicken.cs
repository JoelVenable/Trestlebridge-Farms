using System;
using System.Collections.Generic;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Animals
{
    public class Chicken : IResource, IMeatProducing, IFeatherProducing, IEggProducing
    {
        private Guid _id = Guid.NewGuid();
        private double _meatProduced = 1.7;

        private double _feathersProduced = .5;

        private int _eggsProduced = 7;

        private string _shortId
        {
            get
            {
                return this._id.ToString().Substring(this._id.ToString().Length - 6);
            }
        }
        public string Type { get; } = "Chicken";

        public double Butcher()
        {
            return _meatProduced;
        }

        public int Gather()
        {
            throw new NotImplementedException();
        }

        public double Pluck()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "The Chicken goes: Cluck!";
        }
    }

}
