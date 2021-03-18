using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstates.Services;

namespace RealEstates.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult DoSearch(int minPrice, int maxPrice)
        {
            var properties = _propertiesService.SearchByPrice(minPrice, maxPrice);
            return View(properties);
        }
    }
}
