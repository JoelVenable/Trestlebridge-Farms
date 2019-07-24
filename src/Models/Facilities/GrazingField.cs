using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Facilities
{
    public class GrazingField : IFacility<IGrazing>, IMeatFacility, ICompostProducing, IGathering
    {
        private int _capacity = 20;
        private Guid _id = Guid.NewGuid();

        public string Name { get; set; }

        public int NumAnimals
        {
            get
            {
                return _animals.Count;
            }
        }

        public int NumOstriches
        {
            get
            {
                return _animals.Where(animal => animal.Type == "Ostrich").Count();
            }
        }

        public int NumMeatAnimals
        {
            get
            {
                return _animals.Where(animal => animal.Type != "Goat").Count();
            }
        }


        public int CompostAmmount
        {
            get
            {
                var goats = _animals.FindAll(animal => animal.Type == "Goat");
                return goats.Count;
            }
        }


        public int AvailableSpots
        {
            get
            {
                return _capacity - _animals.Count;
            }
        }


        private List<IGrazing> _animals = new List<IGrazing>();

        public double Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public void AddResource(IGrazing animal)
        {
            // TODO: implement this...
            _animals.Add(animal);
        }

        public void AddResource(List<IGrazing> animals)
        {
            // TODO: implement this...
            _animals.AddRange(animals);
        }

        public List<IGrouping<string, IGrazing>> CreateGroup()
        {
            return _animals.GroupBy(animal => animal.Type).ToList();
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append($"Grazing field {Name}");
            var animalGroups = CreateGroup();
            if (_animals.Count > 0)
            {
                output.Append(" (");
                for (int i = 0; i < animalGroups.Count; i++)
                {
                    int count = animalGroups[i].Count();
                    string s = (count > 1) ? "s" : "";
                    output.Append($"{count} {animalGroups[i].Key}{s}");
                    if (i + 1 < animalGroups.Count)
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

        public void SendToBasket(int numToProcess, Farm farm)
        {
            var ostrich = new Ostrich();
            for (int i = 0; i < numToProcess; i++)
            {
                farm.EggGatherer.AddToBasket(ostrich.EggsProduced);
                farm.EggGatherer.GatheredAnimals(ostrich);
            }
        }


        public List<IGrouping<string, IComposting>> CreateCompostList()
        {
            var convertedGoats = new List<IComposting>();
            var goats = _animals.FindAll(animal => animal.Type == "Goat").ToList();
            foreach (var goat in goats)
            {
                convertedGoats.Add((IComposting)goat);
            }
            return convertedGoats.GroupBy(animal => animal.Type).ToList();
        }




        // public override string ToString()
        // {
        //   StringBuilder output = new StringBuilder();

        //   output.Append($"Grazing field {Name}");
        //   var animalGroups = CreateGroup();
        //   if (_animals.Count > 0)
        //   {
        //     output.Append(" (");
        //     for (int i = 0; i < animalGroups.Count; i++)
        //     {
        //       int count = animalGroups[i].Count();
        //       string s = (count > 1) ? "s" : "";
        //       output.Append($"{count} {animalGroups[i].Key}{s}");
        //       if (i + 1 < animalGroups.Count) output.Append(", ");
        //       else output.Append(")\n");

        //     }

        //   }
        //   else output.Append("\n");

        //   return output.ToString();
        // }

        public List<IGrouping<string, IMeatProducing>> CreateMeatGroup()
        {
            return _animals
            .Where(animal => animal.Type != "Goat")
            .ToList()
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



        public void SendToComposter(int numToProcess, string type, Farm farm)
        {
            var selectedAnimals = _animals.FindAll(animal => animal.Type == "Goat").Take(numToProcess).ToList();
            foreach (var animal in selectedAnimals)
            {
                farm.Composter.AddToHopper((IComposting)animal);
            }
        }
    }
}