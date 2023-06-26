using AutoMapper;
using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
    [Authorize(Roles= "customer")]
    public class CustomerController : ControllerBase
	{
		private readonly IGenericRepository<Customer> CustomerRepo;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> _userManager;

       
        public CustomerController(IGenericRepository<Customer> _CustomerRepo, IMapper _mapper,UserManager<AppUser> userManager)
		{
            CustomerRepo = _CustomerRepo;
            mapper= _mapper;
            _userManager = userManager;
		}

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            IReadOnlyList<Customer> Customers = await CustomerRepo.GetAllAsync();
            if (Customers.Count == 0)
            {
                return StatusCode(200, "No Customers Found");
            }

            var CustomerDTOs = new List<CustomerDTO>();
            foreach (var customer in Customers)
            {
                var CustomerDTO = new CustomerDTO
                {
                    Id = customer.Id,
                    Name=customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Street = customer.Street,
                    City = customer.City,
                    Government = customer.Government,
                    

                };
                foreach (var credit in customer.CreditCards)
                {
                    CustomerDTO.creditCards.Add(credit.Cardholder_name);
                }
                foreach (var order in customer.Orders)
                {
                    CustomerDTO.orders.Add(order.TotalPrice);
                }
                
                CustomerDTOs.Add(CustomerDTO);
            }

            return Ok(CustomerDTOs);
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
            _customerDTO.Id = id;
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
                    Customer customer = await CustomerRepo.GetByIdAsync(id);   
                    var user =await  _userManager.FindByNameAsync(customer.Name);

                    if (user != null)
                    {
                       
                        // Delete the user using the UserManager
                        var result =await _userManager.DeleteAsync(user);

                        if (result !=null)
                        {
                            Customer c = await CustomerRepo.DeleteAsync(id);
                            return Ok("User deleted successfully");
                        }
                        else
                        {
                            // Handle any errors that occurred during deletion
                            return Ok("User Notdeleted successfully");

                        }
                    }
                    else
                    {
                        // User not found
                        return Ok("User Not Found");

                    }

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
