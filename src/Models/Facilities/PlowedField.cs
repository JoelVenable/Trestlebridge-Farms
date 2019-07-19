using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;


namespace Trestlebridge.Models.Facilities
{
    public class PlowedField : IFacility<ISeedProducing>
    {
        private int _rows = 13;

        private int _plantsPerRow = 5;

        private Guid _id = Guid.NewGuid();

        public string Name { get; set; }

        public int AvailableSpots
        {
            get
            {
                return _rows - _plants.Count;
            }
        }

        private List<ISeedProducing> _plants = new List<ISeedProducing>();

        public double Capacity
        {
            get
            {
                return _rows;
            }
        }

        double IFacility<ISeedProducing>.Capacity => throw new NotImplementedException();

        public void AddResource(ISeedProducing plant)
        {
            _plants.Add(plant);
        }

        public void AddResource(List<ISeedProducing> plants)
        {
            _plants.AddRange(plants);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";

            output.Append($"Plowed field {Name} has {this._plants.Count} plants\n");
            this._plants.ForEach(a => output.Append($"   {a}\n"));

            return output.ToString();
        }
    }
}