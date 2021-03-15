using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RealEstates.Services
{
    public class DistrictService : IDistrictsService
    {
        private RealEstateDbContext db;

        public DistrictService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            return db.Districts.OrderByDescending(x => x.Properties.Select(p => p.Price).Average()).Take(count).Select(MapToDistrictViewModel());
        }


        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            return db.Districts.OrderByDescending(x => x.Properties.Count).Select(MapToDistrictViewModel());
        }

        private static Expression<Func<District, DistrictViewModel>> MapToDistrictViewModel()
        {
            return x => new DistrictViewModel { Name = x.Name, AveragePrice = x.Properties.Select(p => p.Price).Average(), MinPrice = x.Properties.Select(p => p.Price).Min(), MaxPrice = x.Properties.Select(p => p.Price).Max(), PropertiesCount = x.Properties.Count };
        }
    }
} 
