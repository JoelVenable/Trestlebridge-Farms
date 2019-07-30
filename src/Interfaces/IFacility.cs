using System.Collections.Generic;

namespace Trestlebridge.Interfaces
{
    public interface IFacility
    {
        double Capacity { get; }

        string Name { get; set; }

        string Type { get; }

        int AvailableSpots { get; }

        //string Export();
        void AddResource(IResource resource);
        void AddResource(List<IResource> resources);

        string Export();
    }
}