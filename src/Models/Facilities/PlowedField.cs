using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Plants;

namespace Trestlebridge.Models.Facilities
{
    public class PlowedField : IFacility
    {
        private int _rows = 13;

        private int _plantsPerRow = 5;

        public string Type { get; } = "Plowed Field";

        public PlowedField() { }

        public PlowedField(string name, string data)
        {
            Name = name;
            List<string> plantsToAdd = data.Split(",").ToList();
            plantsToAdd.ForEach(plant =>
            {
                switch (plant)
                {
                    case "Sunflower":
                        _plants.Add(new Sunflower());
                        break;
                    case "Sesame":
                        _plants.Add(new Sesame());
                        break;
                    default:
                        break;
                }

            });
        }


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


        public void AddResource(IResource resource)
        {
            if (resource is ISeedProducing plant) _plants.Add(plant);
            else throw new Exception("Invalid resource");
        }
        public void AddResource(List<IResource> resources)
        {
            resources.ForEach(resource =>
            {
                if (resource is ISeedProducing plant) _plants.Add(plant);
                else throw new Exception("Invalid resource");
            });
        }

        public void ListByType()
        {
            var groupedPlants = _plants.GroupBy(plant => plant.Type);
            foreach (IGrouping<string, ISeedProducing> plant in groupedPlants)
            {
                Console.WriteLine($"{plant.Key} {plant.Count() * _plantsPerRow}");
            }

        }




        public List<IGrouping<string, ISeedProducing>> CreateGroup()
        {
            return _plants.GroupBy(animal => animal.Type).ToList();
        }

        public void SendToHopper(int numToProcess, string type, Farm farm)
        {
            for (int i = 0; i < numToProcess; i++)
            {
                var selectedPlant = _plants.Find(plant => plant.Type == type);
                farm.SeedHarvester.AddToHopper(selectedPlant);
                _plants.Remove(selectedPlant);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append($"Plowed field: {Name}");
            var plantGroups = CreateGroup();
            if (_plants.Count > 0)
            {
                output.Append(" (");
                for (int i = 0; i < plantGroups.Count; i++)
                {
                    int count = plantGroups[i].Count();
                    string s = (count > 1) ? "s" : "";
                    output.Append($"{count} {plantGroups[i].Key}{s}");
                    if (i + 1 < plantGroups.Count)
                    {
                        output.Append(", ");
                    }
                    else
                    {
                        output.Append(")\n");
                    }
                }

            }
            else output.Append("\n");

            return output.ToString();
        }

        public string Export()
        {
            string output = $"{Type}:{Name}:";
            _plants.ForEach(plant => output += $"{plant.Type},");
            return output;
        }
    }
}