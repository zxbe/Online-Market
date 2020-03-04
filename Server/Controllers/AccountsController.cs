namespace OnlineMarket.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OnlineMarket.Server.Services;
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
        public async Task<IActionResult> Register()
        {
            return this.Ok();
        }

        [HttpPost(ControllersConstants.ActionRoute)]
        public async Task<IActionResult> Login()
        {
            return this.Ok();
        }

        [HttpGet(ProlifeRouteTemplate)]
        public async Task<IActionResult> GetProfile()
        {
            return this.Ok(this._securityService.GetConfigValues());
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
