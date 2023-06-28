using AutoMapper;
using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
    [Authorize(Roles = "seller")]
    public class SellerController : ControllerBase
	{
		private readonly IGenericRepository<Seller> SellerRepo;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> _userManager;


        public SellerController(IGenericRepository<Seller> _SellerRepo, IMapper _mapper, UserManager<AppUser> userManager)
		{
            SellerRepo = _SellerRepo;
            mapper= _mapper;
            _userManager= userManager;
		}



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SellerDTO>>> GetAllSellers()
        {
            // var spec = new GetSellerWithCreditCards();
            IReadOnlyList<Seller> Sellers = await SellerRepo.GetAllAsync();
            if (Sellers.Count == 0)
            {
                return StatusCode(200, "No Sellers Found");
            }

            var SellerDTOs = new List<SellerDTO>();
            foreach (var Seller in Sellers)
            {
                var SellerDTO = new SellerDTO
                {
                    Id=Seller.Id,
                    FirstName = Seller.FirstName,
                    MiddleName = Seller.MiddleName,
                    LastName = Seller.LastName,
                    Email = Seller.Email,
                    Password = Seller.Password,
                    Phone = Seller.Phone,
                    SSN = Seller.SSN,
                    BusinessName = Seller.BusinessName,
                    Street = Seller.Street,
                    City = Seller.City,
                    Governorate = Seller.Governorate

                };
                foreach (var product in Seller.ProductSellers)
                {
                    SellerDTO.SellerProducts.Add(new ProductSellersDTO { ProductId=product.ProductId,Price=product.Price,ProductQuantity=product.Quantity});
                }
                foreach (var order in Seller.Orders)
                {
                    SellerDTO.OrdersDate.Add(order.Date);
                }
                foreach (var order in Seller.Orders)
                {
                    SellerDTO.OrdersPrice.Add(order.TotalPrice);
                }
                //foreach (var store in Seller.StoreProducts)
                //{
                //    SellerDTO.StoreProducts.Add(store.Quantity);
                //}

                SellerDTOs.Add(SellerDTO);
            }

            return Ok(SellerDTOs);
        }

        [HttpGet("{id:int}", Name = "GetOneSellerRoute")]
        public async Task<ActionResult<SellerDTO>> GetSeller(int id)
        {
            Seller Seller = await SellerRepo.GetByIdAsync(id);
            if (Seller == null)
            {
                return NotFound("This Seller Not Found");
            }
            var SellerDTO = new SellerDTO()
            {
                Id = Seller.Id,
                FirstName = Seller.FirstName,
                MiddleName = Seller.MiddleName,
                LastName = Seller.LastName,
                Email = Seller.Email,
                Password=Seller.Password,
                Phone = Seller.Phone,
                SSN = Seller.SSN,
                BusinessName = Seller.BusinessName,
                Street = Seller.Street,
                City = Seller.City,
                Governorate = Seller.Governorate,

            };
            foreach (var product in Seller.ProductSellers)
            {
                SellerDTO.SellerProducts.Add(new ProductSellersDTO { ProductId = product.ProductId, Price = product.Price, ProductQuantity = product.Quantity });
            }
            foreach (var order in Seller.Orders)
            {
                SellerDTO.OrdersDate.Add(order.Date);
            }
            foreach (var order in Seller.Orders)
            {
                SellerDTO.OrdersPrice.Add(order.TotalPrice);
            }
            //foreach (var store in Seller.StoreProducts)
            //{
            //    SellerDTO.StoreProducts.Add(store.Quantity);
            //}


            return Ok(SellerDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddSeller(Seller Seller)
        {
            if (ModelState.IsValid)
            {
                Seller createdSeller = await SellerRepo.AddAsync(Seller);
                await Console.Out.WriteLineAsync($"AddResult=>{createdSeller}");
                string url = Url.Link("GetOneSellerRoute", new { id = Seller.Id });
                return Created(url, createdSeller);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSeller(int id, Seller Seller)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    Seller c = await SellerRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        string result = await SellerRepo.EditAsync(id, Seller);
                        await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, $"This Seller : {c.FirstName} is Edited");
                    }
                    else
                        return StatusCode(404, "This Seller Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<Seller>> DeleteSeller(int id)
        //{
        //      if(id > 0)
        //	{
        //              try
        //              {
        //                  Seller c = await SellerRepo.DeleteAsync(id);
        //                  if (c == null)
        //                      return NotFound("This Seller Not Found");
        //                  else
        //                      return StatusCode(200,$"This Seller : {c.FirstName} is Deleted");
        //              }
        //              catch (ArgumentException ex)
        //              {
        //                  return BadRequest(ex.Message);
        //              }
        //          }
        //   else
        //		return BadRequest("Invalid SellerID");
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Seller>> DeleteSeller(int id)
        {
            if (id > 0)
            {
                try
                {
                    Seller seller = await SellerRepo.GetByIdAsync(id);
                    var user = await _userManager.FindByNameAsync(seller.FirstName);

                    if (user != null)
                    {
                        // Delete the user using the UserManager
                        var result = await _userManager.DeleteAsync(user);

                        if (result != null)
                        {
                            Seller c = await SellerRepo.DeleteAsync(id);
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
                return BadRequest("Invalid SellerID");
        }
    }
}
