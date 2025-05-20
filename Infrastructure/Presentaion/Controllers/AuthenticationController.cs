using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Login
        [HttpPost("Login")] // Post : BaseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }

        // Register
        [HttpPost("Register")] // Post : BaseUrl/api/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }

        // Check Email
        [HttpGet("CheckEmail")] // Get : BaseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }

        // Get Current User
        [Authorize]
        [HttpGet("CurrentUser")] // Get : BaseUrl/api/Authentication
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken());
            return Ok(user);
        }

        // Get Current User Address
        [Authorize]
        [HttpGet("Address")] // Get : BaseUrl/api/Authentication/CurrentUserAddress
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken());
            return Ok(address);
        }

        // Update Current User Address
        [Authorize]
        [HttpPut("Address")] // Put : BaseUrl/api/Authentication/CurrentUserAddress
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var updatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(GetEmailFromToken(), addressDto);
            return Ok(updatedAddress);

        }
    }
}
