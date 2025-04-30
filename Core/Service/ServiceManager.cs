using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository, UserManager<ApplicationUser> _userManager) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService => _LazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AutheniticationService(_userManager));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}
