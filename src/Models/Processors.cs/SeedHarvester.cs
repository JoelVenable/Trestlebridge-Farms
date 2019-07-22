namespace Trestlebridge.Models.Processors
{
    public class SeedHarvester
    {
        public int SesameSeeds { get; set; } = 0;
        public int SunflowerSeeds { get; set; } = 0;

        public int Capacity { get; } = 5;  // rows of plants

        int TotalSeeds
        {
            get
            {
                return SesameSeeds + SunflowerSeeds;
            }
        }



    }
}
