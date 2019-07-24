using System.Collections.Generic;
using Trestlebridge.Interfaces;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Models.Processors
{
  public class MeatProcessor
  {
    private readonly int _capacity = 7;
    double ChickenMeat { get; set; } = 0;
    double CowMeat { get; set; } = 0;
    double PigMeat { get; set; } = 0;
    double SheepMeat { get; set; } = 0;
    double OstrichMeat { get; set; } = 0;

    double DuckMeat { get; set; } = 0;



    double TotalMeat
    {
      get
      {
        return ChickenMeat + CowMeat + PigMeat + SheepMeat + OstrichMeat + DuckMeat;
      }
    }


    private List<IMeatProducing> _hopper = new List<IMeatProducing>();

    public int Capacity
    {
      get
      {
        return _capacity - _hopper.Count;
      }
    }  // rows of plants



    public void AddToHopper(IMeatProducing resourceToAdd)
    {
      _hopper.Add(resourceToAdd);
    }


    public void Process(Farm farm)
    {
      bool hasBirds = false;
      List<IFeatherProducing> birds = new List<IFeatherProducing>();
      double chickenMeat = 0;
      double cowMeat = 0;
      double pigMeat = 0;
      double sheepMeat = 0;
      double ostrichMeat = 0;

      double duckMeat = 0;
      //  Display message about resources processed.

      _hopper.ForEach(item =>
      {
        if (item is Chicken chicken)
        {
          birds.Add(chicken);
          chickenMeat += chicken.Butcher();
        }
        if (item is Cow cow) cowMeat += cow.Butcher();
        if (item is Pig pig) pigMeat += pig.Butcher();
        if (item is Sheep sheep) sheepMeat += sheep.Butcher();
        if (item is Ostrich ostrich) ostrichMeat += ostrich.Butcher();
        if (item is Duck duck)
        {
          birds.Add(duck);
          duckMeat += duck.Butcher();
        }
      });
      string message = "";
      if (birds.Count > 0)
      {
        message = farm.FeatherGatherer.Process(birds);
      }



      message += "Butchering Animals...\n\n";


      if (chickenMeat > 0) message += $"{chickenMeat} Kgs of chicken was processed.\n";
      if (cowMeat > 0) message += $"{cowMeat} Kgs of beef was processed.\n";
      if (pigMeat > 0) message += $"{pigMeat} Kgs of pork was processed.\n";
      if (sheepMeat > 0) message += $"{sheepMeat} Kgs of mutton was processed.\n";
      if (ostrichMeat > 0) message += $"{ostrichMeat} Kgs of ostrich meat was processed.\n";
      if (duckMeat > 0) message += $"{duckMeat} Kgs of duck meat was processed.\n";


      Program.ShowMessage(message);

      ChickenMeat += chickenMeat;
      CowMeat += cowMeat;
      PigMeat += pigMeat;
      SheepMeat += sheepMeat;
      OstrichMeat += ostrichMeat;
      DuckMeat += duckMeat;
      _hopper.Clear();
    }



        public override string ToString()
        {
            string output = "Meat Processor has processed: \n";
            output += BuildString(ChickenMeat, "chicken");
            output += BuildString(CowMeat, "beef");
            output += BuildString(PigMeat, "pork");
            output += BuildString(SheepMeat, "mutton");
            output += BuildString(OstrichMeat, "ostrich meat");
            output += BuildString(DuckMeat, "duck meat");
            output += "\n";
            return output;
        }

        static private string BuildString(double number, string type)
        {

            return $"    ({number} Kgs of {type}) \n";
        }

    }
}
