using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;

namespace Service
{
    public class AutheniticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            // Check If Email Is Exsist
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            // Check If Password Is Correct
            var isPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (isPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email ?? "",
                    Token = CreateTokenAsync(User) // Generate Token Here
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        }


        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            // Mapping RegisterDto To ApplicationUser
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };

            // Create User [Application User]
            var result = await _userManager.CreateAsync(User, registerDto.Password);
            if (result.Succeeded)
            {
                // Generate Token Here
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email ?? "",
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
                // Get Errors From Result
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }
        private string CreateTokenAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
