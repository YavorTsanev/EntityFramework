namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;
    using System.Linq;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>();

            //Catalog
            this.CreateMap<CreateCategoryInputModel, Category>().ForMember(c => c.Name, opt => opt.MapFrom(m => m.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            //Emmployees
            this.CreateMap<Position, RegisterEmployeeViewModel>().ForMember(x => x.PositionName, opt => opt.MapFrom(p => p.Name)).ForMember(x => x.PositionId, opt => opt.MapFrom(y => y.Id));

            this.CreateMap<RegisterEmployeeInputModel, Employee>().ForMember(x => x.Position, o => o.MapFrom(y => y.PositionName));

            this.CreateMap<Employee, EmployeesAllViewModel>().ForMember(x => x.Position, opt => opt.MapFrom(y => y.Position.Name));

            //Items

            this.CreateMap<Category, CreateItemViewModel>().ForMember(x => x.CategoryId, o => o.MapFrom(y =>y.Id)).ForMember(x => x.CategoryName, o => o.MapFrom(y => y.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>().ForMember(x => x.Category, o => o.MapFrom(y => y.Category.Name));

            //Orders

            this.CreateMap<Item, CreateOrderItemVewModel>().ForMember(x => x.ItemId, o => o.MapFrom(y => y.Id)).ForMember(x => x.ItemName, o => o.MapFrom(y => y.Name));

            this.CreateMap<Employee, CreateOrderEmplooyeeViewModel>().ForMember(x => x.EmployeeId, o => o.MapFrom(y => y.Id)).ForMember(x => x.EmployeeName, o => o.MapFrom(y => y.Name));

        }
    } 
}
