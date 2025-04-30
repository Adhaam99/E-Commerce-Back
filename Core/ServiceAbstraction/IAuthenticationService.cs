using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        // Take Email and Password
        // Then Return Token ,  Email and DisplayName 
        Task<UserDto> Login(LoginDto loginDto);

        // Register
        // Take Email , Password  , UserName , Display Name And Phone Number
        // Then Return Token , Email and Display Name
        Task<UserDto> Register(RegisterDto registerDto);
    }
}
