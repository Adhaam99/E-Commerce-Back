using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Service
{
    internal class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Products = await Repo.GetAllAsync(Specifications);
            var ProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductsCount = ProductsDto.Count();
            var TotalCountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(TotalCountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductsCount, TotalCount, ProductsDto);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Product = await Repo.GetByIdAsync(Specifications);
            if(Product is not null)
            {
                var ProductDto = _mapper.Map<Product, ProductDto>(Product);
                return ProductDto;
            }
            else
            {
                throw new Exception($"Product with id {id} not found");
            }

        }

        public async Task<IEnumerable<BrandDto>> GetAllProductBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAllProductTypesAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductType, int>();
            var ProductTypes = await Repo.GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(ProductTypes);
            return TypesDto;
        }

    }
}
