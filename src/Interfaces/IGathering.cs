using Trestlebridge.Models;

namespace Trestlebridge.Interfaces
{
    public interface IGathering
    {
        string Name { get; }
        double Capacity { get; }

        int NumAnimals { get; }

        void SendToBasket(int num, Farm farm);
    }
}