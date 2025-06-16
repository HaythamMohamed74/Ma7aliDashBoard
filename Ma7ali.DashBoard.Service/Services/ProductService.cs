using AutoMapper;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Repository.Interfaces;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Service.Dtos;
using Ma7aliDashBoard.Shared.Exceptions;


namespace Ma7ali.DashBoard.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IImageService imageService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ProductDto> AddProduct(ProductCreationDto productCreationDto)
        {
            if (productCreationDto == null)
                throw new ApiException(nameof(productCreationDto));
            var product = _mapper.Map<Product>(productCreationDto);


            if (productCreationDto.Images != null && productCreationDto.Images.Count > 0)
            {
                var urls = await _imageService.uploadImagesAsync(productCreationDto.Images, "products");

                foreach (var url in urls)
                {
                    product.Images.Add(new ProductImage { ImageUrl = url });
                }
            }

            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(product);

        }

        public async Task<bool> DeleteProduct(int id)
        {
           var product = await _productRepository.GetProductByIdAsync(id);
            
           
            if (product == null)
                throw new ApiException("Product not found");

            foreach (var image in product.Images)
            {
                _imageService.DeleteImage(image.ImageUrl);
            }

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public async Task<ICollection<ProductDto>> GetAllProduct()
        {
             throw new NotImplementedException();
            //return await  _productRepository.GetAllAsync();
        }

        public async Task<ICollection<ProductDto>> GetBestSallerProducts()
        {
            var bestSellers = await _productRepository.GetBestSallerProductsAsync();
            var bestSellersDto = _mapper.Map<ICollection<ProductDto>>(bestSellers);
            return bestSellersDto;       
            //return await _productRepository.GetBestSallerProductsAsync();   
        }

        public async Task<ICollection<ProductDto>> GetTopRatedProducts()
        {
            var topRatedProducts = await _productRepository.GetTopRatedProductsAsync();
            var topRatedProductsDto = _mapper.Map<ICollection<ProductDto>>(topRatedProducts);
            return topRatedProductsDto;
            //return await _productRepository.GetTopRatedProductsAsync();
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            
          var product= await _productRepository.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public Task<PagedResult<ProductDto>> GetSortedFilteredPagedAsync(string search, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> UpdateProduct(ProductToUpdateDto productToUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}