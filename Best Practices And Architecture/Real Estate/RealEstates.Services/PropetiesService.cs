using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstates.Services
{
    public class PropetiesService : IPropertiesService
    {
        private RealEstateDbContext db;

        public PropetiesService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors)
        {
            var property = new RealEstateProperty
            {
                Size = size,
                Year = year < 1800 ? null : year,
                Price = price,
                Floor = floor <= 0 ? null : floor,
                TotalNumberOfFloors = maxFloors <= 0 ? null : maxFloors,
            };

            //District
            var districtEntity = db.Districts.Where(x => x.Name.Trim() == district.Trim()).FirstOrDefault();

            property.District = districtEntity?? new District { Name = district};
            //PropertyType
            var propertyTypeEntity = db.PropertyTypes.FirstOrDefault(x => x.Name.Trim() == propertyType.Trim());

            property.PropertyType = propertyTypeEntity ?? new PropertyType { Name = propertyType };
            //BuildingType
            var buldingTypeEntity = db.BuildingTypes.FirstOrDefault(x => x.Name.Trim() == buildingType.Trim());


            property.BuildingType = buldingTypeEntity?? new BuildingType { Name = buildingType};


            db.RealEstateProperties.Add(property);
            db.SaveChanges();

            UpdateTags(property.Id);
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return db.RealEstateProperties.Where(x => x.Year >= minYear && x.Year <= maxYear && x.Size >= minSize && x.Size <= maxSize).Select(MapToPropertyViewModel()).OrderByDescending(x => x.Price).ToList();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return db.RealEstateProperties.Where(x => x.Price >= minPrice && x.Price <= maxPrice).Select(MapToPropertyViewModel()).OrderByDescending(x => x.Price).ToList();
        }

        public void UpdateTags(int propertyId)
        {
            var property = db.RealEstateProperties.FirstOrDefault(x => x.Id == propertyId);

            property.Tags.Clear();

            if (property.Year.HasValue && property.Year < 1990)
            {
                property.Tags.Add(new RealEstatePropertyTag { Tag = GetOrCreateTag("Old Building")});
            }

            if (property.Year.HasValue && property.Year > 2018 && property. TotalNumberOfFloors > 5)
            {
                property.Tags.Add(new RealEstatePropertyTag { Tag = GetOrCreateTag("Has Parking") });
            }

            if (property.Size > 120)
            {
                property.Tags.Add(new RealEstatePropertyTag { Tag = GetOrCreateTag("Huge Building") });
            }

            if (property.Floor == property.TotalNumberOfFloors)
            {
                property.Tags.Add(new RealEstatePropertyTag { Tag = GetOrCreateTag("Last Floor") });
            }

            db.SaveChanges();
        }

        private Tag GetOrCreateTag(string tagName)
        {
            var tag = db.Tags.FirstOrDefault(x => x.Name.Trim() == tagName.Trim());
            return tag ?? new Tag { Name = tagName };
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return x => new PropertyViewModel { BuildingType = x.BuildingType.Name, PropertyType = x.PropertyType.Name, Price = x.Price, District = x.District.Name, Floor = (x.Floor ?? 0) + "/" + (x.TotalNumberOfFloors ?? 0), Size = x.Size, Year = x.Year };
        }
    }
}
