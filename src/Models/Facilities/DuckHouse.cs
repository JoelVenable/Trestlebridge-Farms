using System;
using System.Collections.Generic;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility<Duck>
    {
        private int _capacity = 12;
        private Guid _id = Guid.NewGuid();
        private List<Duck> _animals = new List<Duck>();

        public int AvailableSpots
        {
            get
            {
                return _capacity - _animals.Count;
            }
        }

        public string Name { get; set; }

        public int NumAnimals
        {
            get
            {
                return _animals.Count;
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
            _animals.Add(duck);
        }

        public void AddResource(List<Duck> ducks)
        {
            _animals.AddRange(ducks);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";
            string s = _animals.Count > 1 ? "s" : "";

            output.Append($"Duck House {Name} ({this._animals.Count} duck{s})\n");

            return output.ToString();
        }
    }
}