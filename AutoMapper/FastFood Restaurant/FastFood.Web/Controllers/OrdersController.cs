namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using AutoMapper.QueryableExtensions;
    using FastFood.Models;

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
                Items = this.context.Items.ProjectTo<CreateOrderItemVeiwModel>(mapper.ConfigurationProvider).ToList(),
                Employees = this.context.Employees.ProjectTo<CreateOrderEmployeeViewModel>(mapper.ConfigurationProvider).ToList()

            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Error", "Home");

            //}

            var order = mapper.Map<Order>(model);

            var orderItem = mapper.Map<OrderItem>(model);

            orderItem.Order = order;

            context.Orders.Add(order);
            context.OrderItems.Add(orderItem);

            context.SaveChanges();

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var orders = context.Orders.ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider).ToList();

            return View(orders);
        }
    }
}
