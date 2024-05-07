using WebApp.Core.Models;
using WebApp.Core.Repository.Interface;
using WebApp.Core.Helpers;

namespace WebApp.Core.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string cacheKey = "customer";

        public CustomerRepository() 
        {
            CreateInitialCustomerObjects();
        }

        public Task<bool> Create(Customer customer)
        {
            try
            {
                var customerCache = CacheHelper.GetFromCache<List<Customer>>(cacheKey);
                customer.Id = GetCurrendIdIdentity(customerCache.Data);
                customerCache.Data.Add(customer);
                OverrideCustomerCacheData(customerCache.Data);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Delete(int Id)
        {
            try
            {
                var customerCache = CacheHelper.GetFromCache<List<Customer>>(cacheKey);
                var customerList = customerCache.Data;
                customerList.Remove(customerList.FirstOrDefault(x => x.Id == Id)); //-- remove from list
                OverrideCustomerCacheData(customerList);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<List<Customer>> GetAll()
        {
            var customerCache = CacheHelper.GetFromCache<List<Customer>>(cacheKey);
            return Task.FromResult(customerCache.Data);
        }

        public Task<bool> Update(Customer customer)
        {
            try
            {
                var customerCache = CacheHelper.GetFromCache<List<Customer>>(cacheKey);
                var customerList = customerCache.Data;
                Customer customerToUpdate = customerList.FirstOrDefault(x => x.Id == customer.Id);
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.EmailAddress = customer.EmailAddress;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;

                customerList.Remove(customerToUpdate); //-- remove from list
                customerList.Add(customerToUpdate); //-- add it back to list
                OverrideCustomerCacheData(customerList);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<Customer> GetById(int id)
        {
            var customerCache = CacheHelper.GetFromCache<List<Customer>>(cacheKey);
            var customerList = customerCache.Data;

            return Task.FromResult(customerList.FirstOrDefault(x => x.Id == id));
        }

        private int GetCurrendIdIdentity(List<Customer> customerList)
        {
            return customerList.Max(x => x.Id) + 1;
        }

        private void OverrideCustomerCacheData(List<Customer> customerList) 
        {
            CacheHelper.RemoveFromCache(cacheKey);
            CacheHelper.SetToCache(cacheKey, customerList);
        }

        private void CreateInitialCustomerObjects() 
        { 
            List<Customer> customerList = new List<Customer>();
            customerList.Add(new Customer { Id = 1, FirstName = "Mark", LastName = "Kevz", EmailAddress = "mark@gmail.com", PhoneNumber = "1234567890", CreateDate = DateTime.Now});
            customerList.Add(new Customer { Id = 2, FirstName = "Mark1", LastName = "Kevz1", EmailAddress = "mark1@gmail.com", PhoneNumber = "1234567890", CreateDate = DateTime.Now});
            customerList.Add(new Customer { Id = 3, FirstName = "Mark2", LastName = "Kevz2", EmailAddress = "mark2@gmail.com", PhoneNumber = "1234567890", CreateDate = DateTime.Now});
            customerList.Add(new Customer { Id = 4, FirstName = "Mark3", LastName = "Kevz3", EmailAddress = "mark@gmail.com", PhoneNumber = "1234567890", CreateDate = DateTime.Now });
            customerList.Add(new Customer { Id = 5, FirstName = "Mark4", LastName = "Kevz4", EmailAddress = "mark@gmail.com", PhoneNumber = "1234567890", CreateDate = DateTime.Now });
            CacheHelper.SetToCache(cacheKey, customerList);
        }
    }
}
