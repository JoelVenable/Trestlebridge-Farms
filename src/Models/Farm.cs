using System;
using System.Collections.Generic;
using System.Text;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Facilities;

namespace Trestlebridge.Models
{
  public class Farm
  {
    public List<GrazingField> GrazingFields { get; } = new List<GrazingField>();
    public List<DuckHouse> DuckHouses { get; } = new List<DuckHouse>();

    public List<NaturalField> NaturalFields { get; } = new List<NaturalField>();

    public List<PlowedField> PlowedFields { get; } = new List<PlowedField>();



    public List<ChickenHouse> ChickenHouses { get; } = new List<ChickenHouse>();

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
          GrazingFields[index].AddResource((IGrazing)resource);
          break;
        default:
          break;
      }
    }

    public void AddGrazingField(GrazingField field)
    {
      GrazingFields.Add(field);
    }

    public void AddPlowedField(PlowedField field)
    {
      PlowedFields.Add(field);
    }

    public void AddNaturalField(NaturalField field)
    {
      NaturalFields.Add(field);
    }

    public void AddChickenHouse(ChickenHouse house)
    {
      ChickenHouses.Add(house);
    }
    public void AddDuckHouse(DuckHouse house)
    {
      DuckHouses.Add(house);
    }

    public override string ToString()
    {
      StringBuilder report = new StringBuilder();

      GrazingFields.ForEach(gf => report.Append(gf));
      PlowedFields.ForEach(pf => report.Append(pf));
      NaturalFields.ForEach(nf => report.Append(nf));
      DuckHouses.ForEach(dh => report.Append(dh));
      ChickenHouses.ForEach(ch => report.Append(ch));

      return report.ToString();
    }
  }
}