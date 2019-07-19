using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;


namespace Trestlebridge.Models.Facilities
{
    public class ChickenHouse : IFacility<Chicken>
    {
        public int _capacity = 15;

        private Guid _id = Guid.NewGuid();

        public string Name { get; set; }

        public int AvailableSpots { get
            {
                return _capacity - _animals.Count;
            } }


        private List<Chicken> _animals = new List<Chicken>();

        public double Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public void AddResource(Chicken chicken)
        {
            _animals.Add(chicken);
        }

        public void AddResource(List<Chicken> chickens)
        {
            _animals.AddRange(chickens);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";

            output.Append($"Chicken House {Name} has {this._animals.Count} animals\n");
            this._animals.ForEach(a => output.Append($"   {a}\n"));

            return output.ToString();
        }
    }
}
