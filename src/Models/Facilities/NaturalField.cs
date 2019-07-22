using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;


namespace Trestlebridge.Models.Facilities
{
    public class NaturalField : IFacility<IComposting>
    {
        private int _rows = 10;

        private int _plantsPerRow = 6;

        private Guid _id = Guid.NewGuid();

        public string Name { get; set; }

        public int AvailableSpots
        {
            get
            {
                return _rows - _plants.Count;
            }
        }

        private List<IComposting> _plants = new List<IComposting>();

        public double Capacity
        {
            get
            {
                return _rows;
            }
        }

        public void AddResource(IComposting plant)
        {
            _plants.Add(plant);
        }

        public void AddResource(List<IComposting> plants)
        {
            _plants.AddRange(plants);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";

            output.Append($"Natural field {Name} has {this._plants.Count} plants\n");
            this._plants.ForEach(a => output.Append($"   {a}\n"));

            return output.ToString();
        }
    }
}