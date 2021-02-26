namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using AutoMapper.QueryableExtensions;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.ProjectTo<CreateOrderItemVewModel>(mapper.ConfigurationProvider).ToList(),
                Employees = this.context.Employees.ProjectTo<CreateOrderEmplooyeeViewModel>(mapper.ConfigurationProvider).ToList()

            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        { 
            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            throw new NotImplementedException();
        }
    }
}
