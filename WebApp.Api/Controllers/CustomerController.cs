using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Models;
using WebApp.Core.Services.Interface;

namespace WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customerList = await _customerService.GetAll();
            return Ok(customerList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            var isAdded = await _customerService.CreateCustomer(customer);
            if (isAdded) 
                return Ok(customer);

            return BadRequest();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]Customer customer)
        {
            var isUpdated = await _customerService.UpdateCustomer(customer);
            if (isUpdated)
                return Ok(customer);

            return BadRequest();
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleted = await _customerService.DeleteCustomer(Id);
            if (isDeleted)
                return Ok();

            return BadRequest();
        }
    }
}
