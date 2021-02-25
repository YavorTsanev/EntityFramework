namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using Models;

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
        }
    }
}
