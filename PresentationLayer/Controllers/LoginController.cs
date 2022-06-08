using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Resources.TokenResources;
using BusinessLogicLayer.Security.Tokens;
using BusinessLogicLayer.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] EmployeeCredentials employeeCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TokenResponse response = await _authenticationService
                .CreateAccessTokenAsync(employeeCredentials.Email, employeeCredentials.Password);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            AccessTokenResource accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(accessTokenResource);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TokenResponse response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.EmployeeEmail);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
           
            AccessTokenResource tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(tokenResource);
        }

        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}