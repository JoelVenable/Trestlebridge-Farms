using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Trestlebridge.Interfaces;


namespace Trestlebridge.Models.Facilities
{
  public class GrazingField : IFacility<IGrazing>
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
                    } else
                    {
                        output.Append(")\n");
                    }
                }

            }
            else output.Append("\n");

            return output.ToString();
    }
  }
}