using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Data;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Models.Processors;

namespace Trestlebridge.Models
{
    public class Farm
    {
        public FileHandler FileHandler { get; } = null;
        public SeedHarvester SeedHarvester { get; } = null;
        public Composter Composter { get; } = null;
        public EggGatherer EggGatherer { get; } = null;
        public MeatProcessor MeatProcessor { get; } = null;
        public FeatherGatherer FeatherGatherer { get; } = null;


        public List<IFacility> Facilities { get; } = new List<IFacility>();

        public List<IAnimalFacility> AnimalFacilities
        {
            get
            {
                return Facilities.Where(fac =>
                {
                    return fac is IAnimalFacility;
                }).Cast<IAnimalFacility>().ToList();
            }
        }

        public Farm()
        {
            FileHandler = new FileHandler();
            SeedHarvester = new SeedHarvester(FileHandler);
            Composter = new Composter(FileHandler);
            MeatProcessor = new MeatProcessor(FileHandler);
            FeatherGatherer = new FeatherGatherer(FileHandler);
            EggGatherer = new EggGatherer(FileHandler);

            FileHandler.Facilities.ForEach(fac =>
            {
                IFacility newFacility = null;
                string[] facilityData = fac.Split(":");
                string type = facilityData[0];
                string name = facilityData[1];
                string data = facilityData[2];

                switch (type)
                {
                    case "Chicken House":
                        newFacility = new ChickenHouse(name, data);
                        break;
                    case "Duck House":
                        newFacility = new DuckHouse(name, data);
                        break;
                    case "Grazing Field":
                        newFacility = new GrazingField(name, data);
                        break;
                    case "Plowed Field":
                        newFacility = new PlowedField(name, data);
                        break;
                    case "Natural Field":
                        newFacility = new NaturalField(name, data);
                        break;
                    default:
                        throw new Exception("Invalid data");
                }
                Facilities.Add(newFacility);


            });

        }


        //public List<IAnimalFacility> AnimalFacilities
        //{
        //    get
        //    {
        //        return Facilities.Where(fac => {
        //            return fac is IAnimalFacility;
        //        }).Cast<IAnimalFacility>().ToList();
        //    }
        //}


        //public int NumberOfAnimalFacilities {
        //    get
        //    {
        //        return ChickenHouses.Count + DuckHouses.Count + GrazingFields.Count;
        //    } }

        //public List<IAnimalFacility> AnimalFacilities { get
        //    {
        //        List<IAnimalFacility> output = new List<IAnimalFacility>();
        //        output.AddRange();
        //    }
        //}
        //public int NumberOfPlantFacilities
        //{
        //    get
        //    {
        //        return NaturalFields.Count + PlowedFields.Count;
        //    }
        //}


        /*
            This method must specify the correct product interface of the
            resource being purchased.
         */
        //public void PurchaseResource<T>(IResource resource, int index)
        //{
        //    Console.WriteLine(typeof(T).ToString());
        //    switch (typeof(T).ToString())
        //    {
        //        case "Cow":
        //            GrazingFields[index].AddResource(resource);
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public void AddFacility(IFacility facility)
        {

            Facilities.Add(facility);

        }


        public override string ToString()
        {
            StringBuilder report = new StringBuilder();

            Facilities.ForEach(fac => report.Append(fac));
            report.Append("\n");
            report.Append(SeedHarvester);
            report.Append(MeatProcessor);
            report.Append(FeatherGatherer);
            report.Append(EggGatherer);
            report.Append(Composter);


            return report.ToString();
        }


        public void Save()
        {
            List<string> output = new List<string>();
            output.AddRange(SeedHarvester.Export());
            output.AddRange(Composter.Export());
            output.AddRange(EggGatherer.Export());
            output.AddRange(MeatProcessor.Export());
            output.AddRange(FeatherGatherer.Export());

            Facilities.ForEach(fac => output.Add(fac.Export()));

            FileHandler.SaveData(output);

        }

    }
}