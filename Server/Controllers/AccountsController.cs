namespace OnlineMarket.Server.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using OnlineMarket.Server.Services;
    using OnlineMarket.Shared.BindingModels.Accounts;
    using OnlineMarket.Shared.Results;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;


    [ApiController]
    [Route(ControllersConstants.ControllerRoute)]
    public class AccountsController : ControllerBase
    {
        private const string ProlifeRouteTemplate = "Profile";
        private readonly SecurityService _securityService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountsController(SecurityService securityService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this._securityService = securityService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost(ControllersConstants.ActionRoute)]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new CreateResult<Claim[]>()
                {
                    IsSuccessful = false,
                    Errors = this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var user = new IdentityUser()
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            var res = await this._userManager.CreateAsync(user, registerModel.Password);

            if (!res.Succeeded)
            {
                return this.BadRequest(new CreateResult<Claim[]>()
                {
                    IsSuccessful = false,
                    Errors = res.Errors.Select(e => $"{e.Code} - {e.Description}")
                });
            }

            return this.Ok(new CreateResult<IEnumerable<Claim>>
            {
                IsSuccessful = true,
                CreatedObject = this._securityService.GetClaims(user)
            });
        }

        [HttpPost(ControllersConstants.ActionRoute)]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new LoginResult()
                {
                    IsSuccessful = false,
                    Errors = this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var user = await this._userManager.FindByNameAsync(loginModel.Username);

            if (user == null)
            {
                user = await this._userManager.FindByEmailAsync(loginModel.Username);
            }

            if (user == null)
            {
                return this.BadRequest(new LoginResult()
                {
                    IsSuccessful = false,
                    Errors = new string[]
                    {
                        string.Format(ControllersConstants.NoSuchUser, loginModel.Username)
                    }
                });
            }

            var res = await this._signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (!res.Succeeded)
            {
                return this.BadRequest(new LoginResult()
                {
                    IsSuccessful = false,
                    Errors = new string[]
                    {
                        ControllersConstants.InvalidCredentials
                    }
                });
            }

            var token = _securityService.GenerateJWT(user);

            return this.Ok(new LoginResult()
            {
                IsSuccessful = true,
                Token = _securityService.GetTokenString(user)
            });
        }

        [Authorize]
        [HttpGet(ProlifeRouteTemplate)]
        public async Task<IActionResult> GetProfile()
        {
            var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);
            var roleClaimsStrings = await this._userManager.GetRolesAsync(user);
            var roleClaims = roleClaimsStrings
                .Select(s =>
                    new Claim(ClaimTypes.Role, s));

            var claims = this._securityService
                .GetClaims(user, roleClaims)
                .Select(c => $"{c.Type} - {c.Value}");

            return this.Ok(claims);
        }

        [Authorize]
        [HttpPut(ProlifeRouteTemplate)]
        public async Task<IActionResult> UpdateProfile()
        {
            var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);

            return this.Ok();
        }


        [Authorize]
        [HttpDelete(ProlifeRouteTemplate)]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await this._userManager.FindByNameAsync(this.User.Identity.Name);

            return this.Ok();
        }
    }
}
