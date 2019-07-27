using System;
using System.Collections.Generic;
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

        public List<GrazingField> GrazingFields { get; } = new List<GrazingField>();
        public List<DuckHouse> DuckHouses { get; } = new List<DuckHouse>();
        public List<NaturalField> NaturalFields { get; } = new List<NaturalField>();
        public List<PlowedField> PlowedFields { get; } = new List<PlowedField>();
        public List<ChickenHouse> ChickenHouses { get; } = new List<ChickenHouse>();
        public int NumberOfAnimalFacilities {
            get
            {
                return ChickenHouses.Count + DuckHouses.Count + GrazingFields.Count;
            } }

        public int NumberOfPlantFacilities
        {
            get
            {
                return NaturalFields.Count + PlowedFields.Count;
            }
        }


        /*
            This method must specify the correct product interface of the
            resource being purchased.
         */
        public void PurchaseResource<T>(IResource resource, int index)
        {
            Console.WriteLine(typeof(T).ToString());
            switch (typeof(T).ToString())
            {
                case "Cow":
                    GrazingFields[index].AddResource(resource);
                    break;
                default:
                    break;
            }
        }
        public void AddFacility(IFacility facility)
        {
            
            if (facility is GrazingField gf) GrazingFields.Add(gf);
            if (facility is PlowedField pf) PlowedFields.Add(pf);
            if (facility is NaturalField nf) NaturalFields.Add(nf);
            if (facility is ChickenHouse ch) ChickenHouses.Add(ch);
            if (facility is DuckHouse dh) DuckHouses.Add(dh);

        }


        public override string ToString()
        {
            StringBuilder report = new StringBuilder();

            GrazingFields.ForEach(gf => report.Append(gf));
            PlowedFields.ForEach(pf => report.Append(pf));
            NaturalFields.ForEach(nf => report.Append(nf));
            DuckHouses.ForEach(dh => report.Append(dh));
            ChickenHouses.ForEach(ch => report.Append(ch));
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