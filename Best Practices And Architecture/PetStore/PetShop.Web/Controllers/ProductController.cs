using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PetStore.Services.Interfaces;
using PetStore.ViewModels.Product;

namespace PetShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var products = _productService.GetAll().ToList();
            var viewModels = _mapper.Map<List<ListAllProductsViewModel>>(products);
            return View(viewModels);
        }
    }
}
