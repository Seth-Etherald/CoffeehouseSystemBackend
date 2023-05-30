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
        private readonly ICustomerRepository _customer;

        public AccountController(CoffeehouseSystemContext context)
        {
            _account = new AccountRepository(context);
            _customer = new CustomerRepository(context);
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

        [HttpPost]
        public IActionResult Register(Register registerInfo)
        {
            if (_account.GetAccountId(registerInfo.Username) != null)
            {
                return BadRequest("Account already existed!");
            }

            Account newAccount = new()
            {
                Username = registerInfo.Username,
                Password = registerInfo.Password,
                IsBanned = false,
            };

            try
            {
                _account.InsertAccount(newAccount);
                _account.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            int? newlyAdded = _account.GetAccountId(registerInfo.Username);

            if (newlyAdded == null)
            {
                return BadRequest();
            }

            Customer newCustomer = new()
            {
                Name = registerInfo.Fullname,
                Phone = registerInfo.Phone,
                Address = "",
                Email = registerInfo.Email,
                AccountId = newlyAdded.Value,
            };

            try
            {
                _customer.InsertCustomer(newCustomer);
                _customer.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return Ok();
        }
    }
}