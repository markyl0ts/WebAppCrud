using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Services.Interface
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();

        Task<Customer> GetById(int id);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int Id);
    }
}
