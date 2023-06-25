using AutoMapper;
using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IGenericRepository<Customer> CustomerRepo;
        private readonly IMapper mapper;

        public CustomerController(IGenericRepository<Customer> _CustomerRepo, IMapper _mapper)
		{
            CustomerRepo = _CustomerRepo;
            mapper= _mapper;
		}

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
           // var spec = new GetCustomerWithCreditCards();
            IReadOnlyList<Customer> customers = await CustomerRepo.GetAllAsync();
            if (customers.Count == 0)
            {
                return StatusCode(200, "No Customers Found");
            }
 
            var customerDTOs = new List<CustomerDTO>();
            foreach (var customer in customers)
            {
                var customerDTO = new CustomerDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Street = customer.Street,
                    City = customer.City,
                    Government = customer.Government,
                 
                };
                foreach (var credit in customer.CreditCards)
                {
                    customerDTO.creditCards.Add(credit.Cardholder_name);
                }
                foreach (var order in customer.Orders)
                {
                    customerDTO.orders.Add(order.TotalPrice);
                }

                customerDTOs.Add(customerDTO);
            }

            return Ok(customerDTOs);
        }

        [HttpGet("{id:int}", Name = "GetOneCustomerRoute")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var spec = new GetCustomerWithCreditCards(id);
            Customer customer = await CustomerRepo.GetEntityWithSpec(spec);
            if (customer == null)
            {
                return NotFound("This Customer Not Found");
            }
            var _customerDTO=new CustomerDTO();
            _customerDTO.Name= customer.Name;
            _customerDTO.Phone= customer.Phone;
            _customerDTO.Email= customer.Email;
            _customerDTO.Street= customer.Street;
            _customerDTO.City= customer.City;
            _customerDTO.Government= customer.Government;


            foreach (var credit in customer.CreditCards)
            {
                _customerDTO.creditCards.Add(credit.Cardholder_name);
            }
            foreach (var order in customer.Orders)
            {
                _customerDTO.orders.Add(order.TotalPrice);
            }
            return Ok(_customerDTO);
        }

     

        [HttpPost]
		public async Task<ActionResult> AddCustomer(Customer customer)
		{
			if(ModelState.IsValid) 
			{
				Customer createdCustomer=await CustomerRepo.AddAsync(customer);
                await Console.Out.WriteLineAsync($"AddResult=>{createdCustomer}");
                string url = Url.Link("GetOneCustomerRoute",new {id=customer.Id});
				return Created(url, createdCustomer);
			}
			return BadRequest(ModelState);
		}


		[HttpPut("{id:int}")]
		public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
		{
			if (ModelState.IsValid)
			{
				if (id > 0)
				{
					Customer c = await CustomerRepo.GetByIdAsync(id);
					if (c != null)
					{
						string result = await CustomerRepo.EditAsync(id, customer);
						await Console.Out.WriteLineAsync($"EditResult=>{result}");
						return StatusCode(200, $"This Customer : {c.Name} is Edited");
					}
					else
						return StatusCode(404, "This Customer Not Found");
				}
				else
					return StatusCode(404, "Invalid ID");
			}
			return BadRequest(ModelState);
		}


		[HttpDelete("{id:int}")]
		public async Task<ActionResult<Customer>> DeleteCustomer(int id)
		{
	       if(id > 0)
			{
                try
                {
                    Customer c = await CustomerRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This Customer Not Found");
                    else
                        return StatusCode(200,$"This Customer : {c.Name} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
		   else
				return BadRequest("Invalid CustomerID");
		}
    }
}
