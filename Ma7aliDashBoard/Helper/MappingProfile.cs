using AutoMapper;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Api.Dtos;

namespace Ma7aliDashBoard.Api.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(d=>d.CategoryName,o=>o.MapFrom(s=>s.Category.Name))
                 .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(d => d.Images, o => o.MapFrom(s => s.Images))
                .ReverseMap();
            //CreateMap<Product,ProductDto>()
            //    .ForMember(d=>d.BrandName,o=>o.MapFrom(s=>s.Brand.Name)).ReverseMap();
            CreateMap<ProductCreationDto, Product>()
     .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();


            CreateMap<UpdateProductDto, Product>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));  //; //This configuration ensures that only non-null properties from the ProductUpdateDto are mapped to the Product entity.


            //CreateMap<Product, UpdateProductDto>();

            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<StoreCreateDto, Store>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
       
            



        }
    }
}
