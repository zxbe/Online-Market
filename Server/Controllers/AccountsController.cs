namespace OnlineMarket.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OnlineMarket.Server.Services;
    using OnlineMarket.Shared.BindingModels.Accounts;
    using OnlineMarket.Shared.Results;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [ApiController]
    [Route(ControllersConstants.ControllerRoute)]
    public class AccountsController : ControllerBase
    {
        private const string ProlifeRouteTemplate = "Profile";
        private readonly SecurityService _securityService;

        public AccountsController(SecurityService securityService)
        {
            this._securityService = securityService;
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

            //User Registration Logic

            return this.Ok(new CreateResult<ClaimsIdentity>()
            {
                IsSuccessful = true,
                //CreatedObject = this._securityService.GetClaims()
            });
        }

        [HttpPost(ControllersConstants.ActionRoute)]
        public async Task<IActionResult> Login()
        {
            return this.Ok();
        }

        [HttpGet(ProlifeRouteTemplate)]
        public async Task<IActionResult> GetProfile()
        {
            return this.Ok();
        }

        [HttpPut(ProlifeRouteTemplate)]
        public async Task<IActionResult> UpdateProfile()
        {
            return this.Ok();
        }

        [HttpDelete(ProlifeRouteTemplate)]
        public async Task<IActionResult> DeleteProfile()
        {
            return this.Ok();
        }
    }
}
