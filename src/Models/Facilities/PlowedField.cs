using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using System.Linq;

namespace Trestlebridge.Models.Facilities
{
    public class PlowedField : IFacility<ISeedProducing>
    {
        private int _rows = 13;

        private int _plantsPerRow = 5;

        private Guid _id = Guid.NewGuid();

        public string Name { get; set; }

        public int currentPlants
        {
            get
            {
                return _plants.Count * _plantsPerRow;
            }
        }

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

        public void ListByType()
        {
            var groupedPlants = _plants.GroupBy(plant => plant.Type);
            foreach (IGrouping<string, ISeedProducing> plant in groupedPlants)
            {
                Console.WriteLine($"{plant.Key} {plant.Count() * _plantsPerRow}");
            }

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