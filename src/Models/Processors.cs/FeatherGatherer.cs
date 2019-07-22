namespace Trestlebridge.Models.Processors
{
    public class FeatherGatherer
    {
        double ChickenFeathers { get; set; } = 0;
        double DuckFeathers { get; set; } = 0;
        double TotalFeathers
        {
            get
            {
                return ChickenFeathers + DuckFeathers;
            }
        }



    }
}
