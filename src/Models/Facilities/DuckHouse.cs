using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility, IMeatFacility, IGathering
    {
        private int _capacity = 12;
        private Guid _id = Guid.NewGuid();
        private List<Duck> _ducks = new List<Duck>();

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

        public int NumMeatAnimals
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

        public void AddResource(IResource resource)
        {
            if (resource is Duck duck) _ducks.Add(duck);
            else throw new Exception("Invalid resource.");
        }

        public void AddResource(List<IResource> resources)
        {
            resources.ForEach(resource =>
            {
                if (resource is Duck duck) _ducks.Add(duck);
                else throw new Exception("Invalid resource.");
            });
        }

        public void SendToBasket(int numToProcess, Farm farm)
        {
            // var duck = new Duck();
            for (int i = 0; i < numToProcess; i++)
            {
                // farm.EggGatherer.AddToBasket(_ducks[i].EggsProduced);
                farm.EggGatherer.GatheredAnimals(_ducks[i]);
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

        public List<IGrouping<string, IMeatProducing>> CreateMeatGroup()
        {
            return _ducks
            .ConvertAll(animal => (IMeatProducing)animal)
            .GroupBy(animal => animal.Type).ToList();
            // return new List<IGrouping<string, IMeatProducing>> (){
            //   new IGrouping<string, IMeatProducing>(){

            //   }
            // };
        }

        public void SendToHopper(int numToProcess, string type, Farm farm)
        {
            for (int i = 0; i < numToProcess; i++)
            {
                var selectedAnimal = _ducks.Find(animal => animal.Type == type);
                farm.MeatProcessor.AddToHopper((IMeatProducing)selectedAnimal);
                _ducks.Remove(selectedAnimal);
            }
        }
    }
}