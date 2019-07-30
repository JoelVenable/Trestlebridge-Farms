using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;


namespace Trestlebridge.Models.Facilities
{
    public class ChickenHouse : IFacility, IMeatFacility, IGathering, IAnimalFacility
    {
        public int _capacity = 15;

        public string Type { get; } = "Chicken House";

        public string Name { get; set; }

        public int NumAnimals
        {
            get
            {
                return _chickens.Count;
            }
        }

        public int NumMeatAnimals
        {
            get
            {
                return _chickens.Count;
            }
        }

        public ChickenHouse() { }

        public ChickenHouse(string name, string data)
        {
            Name = name;
            List<string> chickensToAdd = data.Split(",").ToList();
            chickensToAdd.ForEach(chk =>
            {
                if (chk == "Chicken") _chickens.Add(new Chicken());
            });
        }


        public int AvailableSpots
        {
            get
            {
                return _capacity - _chickens.Count;
            }
        }

        private List<Chicken> _chickens = new List<Chicken>();

        public double Capacity
        {
            get
            {
                return _capacity;
            }
        }


        public void AddResource(IResource resource)
        {
            if (resource is Chicken chicken) _chickens.Add(chicken);
            else throw new Exception("Invalid resource.");
        }

        public void AddResource(List<IResource> resources)
        {
            resources.ForEach(resource =>
            {
                if (resource is Chicken chicken) _chickens.Add(chicken);
                else throw new Exception("Invalid resource.");
            });
        }

        public void SendToBasket(int numToProcess, Farm farm)
        {
            // var chicken = new Chicken();
            for (int i = 0; i < numToProcess; i++)
            {
                // farm.EggGatherer.AddToBasket(_chickens[i].EggsProduced);
                farm.EggGatherer.GatheredAnimals(_chickens[i]);
            }
        }



        // private List<Chicken> _animals = new List<Chicken>();



        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string s = _chickens.Count > 1 ? "s" : "";
            string count = _chickens.Count > 0 ? $"({ this._chickens.Count} chicken{ s})" : "";
            output.Append($"Chicken House: {Name} {count}\n");

            return output.ToString();
        }

        public List<IGrouping<string, IMeatProducing>> CreateMeatGroup()
        {
            return _chickens
            .ConvertAll(chicken => (IMeatProducing)chicken)
            .GroupBy(chicken => chicken.Type).ToList();
            // return new List<IGrouping<string, IMeatProducing>> (){
            //   new IGrouping<string, IMeatProducing>(){

            //   }
            // };
        }

        public void SendToHopper(int numToProcess, string type, Farm farm)
        {
            for (int i = 0; i < numToProcess; i++)
            {
                var selectedChicken = _chickens.Find(chicken => chicken.Type == type);
                farm.MeatProcessor.AddToHopper((IMeatProducing)selectedChicken);
                _chickens.Remove(selectedChicken);
            }
        }

        public string Export()
        {
            string output = $"{Type}:{Name}:";
            _chickens.ForEach(chicken => output += $"{chicken.Type},");
            return output;
        }

    }
}
