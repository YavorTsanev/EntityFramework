namespace FastFood.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public List<CreateOrderItemVewModel> Items { get; set; }

        public List<CreateOrderEmplooyeeViewModel> Employees { get; set; }
    }
}
