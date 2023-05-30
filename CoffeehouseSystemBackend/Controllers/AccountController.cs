using Microsoft.AspNetCore.Mvc;
using CoffeehouseLibrary.DAL;
using CoffeehouseLibrary.Models;
using CoffeehouseLibrary.DTO;

namespace CoffeehouseSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _account;

        public AccountController(CoffeehouseSystemContext context)
        {
            _account = new AccountRepository(context);
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            int result = _account.Login(login);
            if (result == 0)
            {
                AccountStatus? accountStatus = _account.GetAccountStatus(login);
                if (accountStatus != null)
                {
                    return Ok(accountStatus);
                }
                else
                {
                    return BadRequest("Something weird happened, check the database");
                }
            }

            return BadRequest(result);
        }
    }
}