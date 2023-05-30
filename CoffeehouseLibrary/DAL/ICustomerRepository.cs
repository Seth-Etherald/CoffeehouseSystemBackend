using CoffeehouseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeehouseLibrary.DAL
{
    public interface ICustomerRepository : IDisposable
    {
        List<Customer> GetCustomers();

        Customer? GetCustomer(int id);

        Customer? GetCustomerByAccountId(int id);

        void InsertCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int id);

        void Save();
    }
}