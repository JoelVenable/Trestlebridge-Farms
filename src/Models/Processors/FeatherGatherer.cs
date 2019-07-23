using Trestlebridge.Interfaces;
using System.Collections.Generic;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Processors
{
  public class FeatherGatherer
  {
    double ChickenFeathers { get; set; } = 0;
    double DuckFeathers { get; set; } = 0;

    public int Capacity { get; } = 5;  // rows of plants

    double TotalFeathers
    {
      get
      {
        return ChickenFeathers + DuckFeathers;
      }
    }

    public string Process(List<IFeatherProducing> birds)
    {

      double chickenFeathers = 0;
      double duckFeathers = 0;
      birds.ForEach(bird =>
      {
        if (bird is Chicken chicken)
        {
          chickenFeathers += chicken.Pluck();
        }
        if (bird is Duck duck)
        {
          duckFeathers += duck.Pluck();
        }
      });

      ChickenFeathers += chickenFeathers;
      DuckFeathers += duckFeathers;

      string message = "Plucking birds...\n";

      if (chickenFeathers > 0) message += $"{chickenFeathers} Kg of Chicken feathers were plucked.\n";
      if (duckFeathers > 0) message += $"{duckFeathers} Kg of Duck feathers were plucked.\n";

      return message;
    }

  }
}
