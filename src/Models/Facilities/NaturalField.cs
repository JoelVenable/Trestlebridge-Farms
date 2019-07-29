using System;
using System.Text;
using System.Collections.Generic;
using Trestlebridge.Interfaces;
using System.Linq;

namespace Trestlebridge.Models.Facilities
{
  public class NaturalField : IFacility, ICompostProducing
  {
    private int _rows = 10;

    private int _plantsPerRow = 6;

        public string Type { get; } = "Natural Field";

    private Guid _id = Guid.NewGuid();

    public int currentPlants
    {
      get
      {
        return _plants.Count * _plantsPerRow;
      }
    }

    public int CompostAmount
    {
      get
      {
        return currentPlants;
      }
    }


    public string Name { get; set; }

    public int AvailableSpots
    {
      get
      {
        return _rows - _plants.Count;
      }
    }

    private List<IComposting> _plants = new List<IComposting>();


    public double Capacity
    {
      get
      {
        return _rows;
      }
    }

    public void AddResource(IResource resource)
    {
            if (resource is IComposting plant) _plants.Add(plant);
            else throw new Exception("Invalid Resource");
    }

    public void AddResource(List<IResource> resources)
    {
            resources.ForEach(resource =>
            {
                if (resource is IComposting plant) _plants.Add(plant);
                else throw new Exception("Invalid Resource");
            });
    }

    public void ListByType()
    {
      var groupedPlants = _plants.GroupBy(plant => plant.Type);
      foreach (IGrouping<string, IComposting> plant in groupedPlants)
      {
        System.Console.WriteLine($"{plant.Key} {plant.Count() * _plantsPerRow}");
      }

    }

    public void SendToComposter(int numToProcess, string type, Farm farm)
    {
      for (int i = 0; i < numToProcess; i++)
      {
        var selectedPlant = _plants.Find(plant => plant.Type == type);
        farm.Composter.AddToHopper(selectedPlant);
        _plants.Remove(selectedPlant);
      }
    }


    public List<IGrouping<string, IComposting>> CreateCompostList()
    {
      return _plants.GroupBy(animal => animal.Type).ToList();
    }

    public override string ToString()
    {
      StringBuilder output = new StringBuilder();

      output.Append($"Natural field {Name}");
      var plantGroups = CreateCompostList();
      if (_plants.Count > 0)
      {
        output.Append(" (");
        for (int i = 0; i < plantGroups.Count; i++)
        {
          int count = plantGroups[i].Count();
          string s = (count > 1) ? "s" : "";
          output.Append($"{count} {plantGroups[i].Key}{s}");
          if (i + 1 < plantGroups.Count)
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

  }
}