using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.Services.Interfaces;

namespace PetShop.Web.Controllers
{
    public class ProductControler : Controller
    {
        private readonly IProductService _productService;

        public ProductControler(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var products = _productService.GetAll();

            return View(products);
        }
    }
}
