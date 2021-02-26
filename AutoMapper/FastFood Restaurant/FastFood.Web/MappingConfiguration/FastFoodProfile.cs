namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
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

        }
    } 
}
