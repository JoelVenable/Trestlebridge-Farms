using System;
using System.Collections.Generic;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility<Duck>
    {
        private int _capacity = 200;
        private Guid _id = Guid.NewGuid();
        private List<Duck> _animals = new List<Duck>();

        public string Name { get; set; }

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

            output.Append($"Duck House {shortId} has {this._animals.Count} animals\n");
            this._animals.ForEach(a => output.Append($"   {a}\n"));

            return output.ToString();
        }
    }
}