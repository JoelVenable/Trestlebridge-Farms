using System;
using System.Collections.Generic;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility<Duck>, IGathering
    {
        private int _capacity = 12;
        private Guid _id = Guid.NewGuid();
        private List<IEggProducing> _ducks = new List<IEggProducing>();

        public int AvailableSpots
        {
            get
            {
                return _capacity - _ducks.Count;
            }
        }

        public string Name { get; set; }

        public int NumAnimals
        {
            get
            {
                return _ducks.Count;
            }
        }


        public double Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public void AddResource(Duck duck)
        {
            _ducks.Add(duck);
        }

        public void AddResource(List<Duck> ducks)
        {
            _ducks.AddRange(ducks);
        }

        public void SendToBasket(int numToProcess, Farm farm)
        {
            var duck = new Duck();
            for (int i = 0; i < numToProcess; i++)
            {
                farm.EggGatherer.AddToBasket(duck.EggsProduced);
                farm.EggGatherer.GatheredAnimals(duck);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";
            string s = _ducks.Count > 1 ? "s" : "";
            string count = _ducks.Count > 0 ? $"({ this._ducks.Count} duck{ s})" : "";

            output.Append($"Duck House {Name} {count}\n");

            return output.ToString();
        }
    }
}