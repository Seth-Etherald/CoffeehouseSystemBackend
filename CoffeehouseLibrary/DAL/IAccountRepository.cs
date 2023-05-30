using CoffeehouseLibrary.DTO;
using CoffeehouseLibrary.Models;

namespace CoffeehouseLibrary.DAL
{
    public interface IAccountRepository : IDisposable
    {
        List<Account> GetAccounts();

        AccountStatus? GetAccountStatus(Login login);

        int Login(Login login);

        void InsertAccount(Account account);

        void UpdateAccount(Account account);

        void DeleteAccount(int id);

        void Save();
    }
}