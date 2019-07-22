namespace Trestlebridge.Models.Processors
{
    public class SeedHarvester
    {
        int SeasameSeeds { get; set; } = 0;
        int SunflowerSeeds { get; set; } = 0;

        int TotalSeeds
        {
            get
            {
                return SeasameSeeds + SunflowerSeeds;
            }
        }



    }
}
