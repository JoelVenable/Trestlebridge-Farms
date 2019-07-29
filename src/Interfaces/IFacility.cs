using System.Collections.Generic;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Interfaces
{
    public interface IFacility
    {
        double Capacity { get; }

        string Name { get; set; }

        string Type { get; }

        int AvailableSpots { get; }


        void AddResource(IResource resource);
        void AddResource(List<IResource> resources);
    }
}