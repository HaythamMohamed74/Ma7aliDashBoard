using AutoMapper;
using Ma7ali.DashBoard.Data.Entities.CartEntities;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Service.Dtos;

namespace Ma7aliDashBoard.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Images, o => o.MapFrom(s => s.Images))
                 .ForMember(dest => dest.AvgRateing,
                       opt => opt.MapFrom(src => src.reviews.Any() ?
                           Math.Round(src.reviews.Average(r => r.Rating), 2) : 0))
                .ReverseMap();
            CreateMap<Product, ProductDto>()
    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl.Replace("\\", "/")).ToList()));

            CreateMap<ProductCreationDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();

            CreateMap<UpdateProductDto, Product>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));  //; //This configuration ensures that only non-null properties from the ProductUpdateDto are mapped to the Product entity.

            //CreateMap<Product, UpdateProductDto>();

            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<StoreCreateDto, Store>().ReverseMap();
            CreateMap<StoreUpdateDto, Store>().ReverseMap();

            CreateMap<Category, CategoryToReturnDto>().ReverseMap();
            CreateMap<CategoryToCreateDto, Category>();
            CreateMap<Category, CategoryToEditDto>().ReverseMap();

            CreateMap<Cart, CartDto>()
                 .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.Items.Sum(i => i.Price * i.Quantity)));
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemToAddDto, CartItem>();

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()));
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
            CreateMap<DeliveyMethod, DeliveryMethodDto>();
        }
    }
}