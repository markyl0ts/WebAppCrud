using WebApp.Core.Models;

namespace WebApp.Core.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<bool> Create(Customer customer);
        Task<bool> Update(Customer customer);
        Task<bool> Delete(int Id);
    }
}
