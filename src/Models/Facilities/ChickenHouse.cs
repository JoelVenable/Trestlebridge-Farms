using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;


namespace Trestlebridge.Models.Facilities
{
    public class ChickenHouse : IFacility<Chicken>, IMeatFacility, IGathering
    {
        public int _capacity = 15;

        private Guid _id = Guid.NewGuid();

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

        public void AddResource(Chicken chicken)
        {
            _chickens.Add(chicken);
        }

        public void AddResource(List<Chicken> chickens)
        {
            _chickens.AddRange(chickens);
        }

        public void SendToBasket(int numToProcess, Farm farm)
        {
            // var chicken = new Chicken();
            for (int i = 0; i < numToProcess; i++)
            {
                farm.EggGatherer.AddToBasket(_chickens[i].EggsProduced);
                // farm.EggGatherer.GatheredAnimals(chicken);
            }
        }



        // private List<Chicken> _animals = new List<Chicken>();



        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            string shortId = $"{this._id.ToString().Substring(this._id.ToString().Length - 6)}";
            string s = _chickens.Count > 1 ? "s" : "";
            string count = _chickens.Count > 0 ? $"({ this._chickens.Count} chickens{ s})" : "";
            output.Append($"Chicken House {Name} {count}\n");

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
    }
}
