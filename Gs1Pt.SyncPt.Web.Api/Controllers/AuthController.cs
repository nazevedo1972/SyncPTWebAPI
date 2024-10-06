using Gs1Pt.SyncPt.Component.Auth.Models.Dtos;
using Gs1Pt.SyncPt.Component.Auth.Models.Identity;
using Gs1Pt.SyncPt.Component.Auth.Services;
using Gs1Pt.SyncPt.Web.Api.Extensions;
using Gs1Pt.SyncPt.Web.Api.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Gs1Pt.SyncPt.Web.Api.Controllers
{
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v3/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<TradeItemsController> _logger;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IAuthService _authService;

        public AuthController(ILogger<TradeItemsController> logger,
                                    UserManager<AuthUser> userManager,      
                                    IAuthService authService)
        {
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("token", Name = "token")]
        public async Task<string> GetTokenAsync(TokenRequest tokenRequest)
        {
            var authResponse = await _authService.Login(_userManager, new Component.Auth.Models.Dtos.AuthRequest
            {
                Email = tokenRequest.Email,
                Password = tokenRequest.Password,
                RememberMe = true
            });

            return authResponse.Token;
        }

        [HttpPost("fulltoken", Name = "fulltoken")]
        public async Task<TokenResponse> GetFullTokenAsync(TokenRequest tokenRequest)
        {
            var authResponse = await _authService.Login(_userManager, new Component.Auth.Models.Dtos.AuthRequest
            {
                Email = tokenRequest.Email,
                Password = tokenRequest.Password,
                RememberMe = true
            });

            return new TokenResponse
            {
                AccessToken = authResponse.Token,
                ExpiresIn = (int)(authResponse.ValidTo - DateTime.UtcNow).TotalSeconds
            };
        }

        [HttpGet("username", Name = "GetUserName")]
        public async Task<string> GetUserName()
        {
            var ts = User.Identity.Name;
            return ts;
        }


        [Authorize]
        [HttpGet("userinfo", Name = "userinfo")]
        public async Task<string> GetUserInfo()
        {
            return User.GetAllClaims();
        }
    }
}
