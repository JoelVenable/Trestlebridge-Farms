using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trestlebridge.Interfaces;

namespace Trestlebridge.Actions
{
    public class PlaceResource
    {
        static public void ChooseAnimalFacility(List<IAnimalFacility> facilities, IResource resource)
        {
            //  This method assumes the list of Ifacilities is already filtered to produce the 
            //  appropriate options for the resource passed in.

            List<string> options = facilities.Select(facility =>
            {
                return $"{facility.Type}: { facility.Name} ({ facility.NumAnimals} animals)";
            }).ToList();

            int choice = StandardMessages.ShowMenu(options,
                $"Place the {resource.Type} where?");
            if (choice == 0) return;

            facilities[choice - 1].AddResource(resource);

        }
    }

}
