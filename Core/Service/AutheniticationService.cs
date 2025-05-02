using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;

namespace Service
{
    public class AutheniticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
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
                    Token = await CreateTokenAsync(User) // Generate Token Here
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        }


        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
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
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                // Get Errors From Result
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            // Create Claims
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName!)
            };

            // Add Roles Claims
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create Key
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Create Credentials
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Create Token
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
