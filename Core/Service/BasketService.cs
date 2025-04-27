using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreatedOrUpdateBasketAsync(BasketDto Basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(Basket);

            var IsCreatedOrUpdated =  await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);

            if (IsCreatedOrUpdated is not null)
                return await GetBasketAsync(Basket.Id);
            else
                throw new Exception("Basket Not Created or Updated, Try Again Later");
        }
        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetCustomerBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeleteBasketAsync(Key);

    }
}
