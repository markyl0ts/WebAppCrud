using WebApp.Core.Models;
using WebApp.Core.Repository.Interface;
using WebApp.Core.Services.Interface;

namespace WebApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) 
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            return await _customerRepository.Create(customer);
        }

        public async Task<bool> DeleteCustomer(int Id)
        {
            return await _customerRepository.Delete(Id);
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _customerRepository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.Update(customer);
        }
    }
}
