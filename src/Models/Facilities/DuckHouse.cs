using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class DuckHouse : IFacility<Duck>, IMeatFacility
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

          public int NumMeatAnimals
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
            string count = _animals.Count > 0 ? $"({ this._animals.Count} duck{ s})" : "";

            output.Append($"Duck House {Name} {count}\n");

            return output.ToString();
        }

        public List<IGrouping<string, IMeatProducing>> CreateMeatGroup()
        {
            return _animals
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
                var selectedAnimal = _animals.Find(animal => animal.Type == type);
                farm.MeatProcessor.AddToHopper((IMeatProducing)selectedAnimal);
                _animals.Remove(selectedAnimal);
            }
        }
    }
}