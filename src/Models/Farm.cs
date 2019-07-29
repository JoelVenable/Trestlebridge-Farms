using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Facilities;
using Trestlebridge.Models.Processors;

namespace Trestlebridge.Models
{
    public class Farm
    {
        public SeedHarvester SeedHarvester { get; } = new SeedHarvester();
        public Composter Composter { get; } = new Composter();
        public EggGatherer EggGatherer { get; } = new EggGatherer();
        public MeatProcessor MeatProcessor { get; } = new MeatProcessor();
        public FeatherGatherer FeatherGatherer { get; } = new FeatherGatherer();

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


    }
}