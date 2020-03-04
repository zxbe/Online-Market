namespace OnlineMarket.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route(ControllersConstants.ControllerRoute)]
    public class AccountsController : ControllerBase
    {
        private const string ProlifeRouteTemplate = "Profile";

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
