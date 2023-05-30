﻿using CoffeehouseLibrary.DTO;
using CoffeehouseLibrary.Models;

namespace CoffeehouseLibrary.DAL
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private CoffeehouseSystemContext _context;
        private bool _disposed = false;

        public AccountRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public int? GetAccountId(string username)
        {
            Account? account = _context.Accounts.FirstOrDefault(account => account.Username.Equals(username));
            if (account != null)
            {
                return account.AccountId;
            }
            return null;
        }

        public AccountStatus? GetAccountStatus(Login login)
        {
            Account? account = _context.Accounts.FirstOrDefault(account => account.Username.Equals(login.Username) && account.Password.Equals(login.Password));

            if (account != null)
            {
                return new AccountStatus
                {
                    AccountId = account.AccountId,
                    IsBanned = account.IsBanned,
                };
            }
            return null;
        }

        /// <summary>
        /// Return integer status to check login
        /// </summary>
        /// <param name="login">Login object, contains username and password</param>
        /// <returns>0: Account Exist, Correct Password;
        /// 1: Account Exist, Incorrect Password;
        /// 2: Account does not exist
        /// </returns>

        public int Login(Login login)
        {
            Account? checkExist = _context.Accounts.FirstOrDefault(account => account.Username.Equals(login.Username));

            if (checkExist != null)
            {
                return checkExist.Password!.Equals(login.Password) ? 0 : 1;
            }

            return 2;
        }

        public void InsertAccount(Account account)
        {
            _context.Accounts.Add(account);
        }

        public void UpdateAccount(Account account)
        {
            _context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteAccount(int id)
        {
            Account? toDelete = _context.Accounts.FirstOrDefault(account => account.AccountId == id);
            if (toDelete != null)
            {
                _context.Accounts.Remove(toDelete);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}