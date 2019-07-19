using System;
using System.Collections.Generic;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility<Duck>
    {
        private int _capacity = 200;
        private Guid _id = Guid.NewGuid();
        private List<Duck> _animals = new List<Duck>();

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
    }
}