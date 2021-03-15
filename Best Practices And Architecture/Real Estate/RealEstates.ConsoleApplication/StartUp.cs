using RealEstates.Data;
using RealEstates.Services;
using System;

namespace RealEstates.ConsoleApplication
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();

            var propertyService = new PropetiesService(db);

            propertyService.Create("Трявна",130, 2019, 100000, "Апартамент", "Тухла", 6, 6);

        }
    }
}
