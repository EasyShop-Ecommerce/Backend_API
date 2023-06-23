using AutoMapper;
using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IGenericRepository<OrderDetail> OrderDetailsRepo;
        private readonly IMapper mapper;

        public OrderDetailsController(IGenericRepository<OrderDetail> _OrderDetailsRepo, IMapper _mapper)
        {
            OrderDetailsRepo = _OrderDetailsRepo;
            mapper = _mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<OrderDetailsDTO>>> GetAllOrderDetailss()
        {
            IReadOnlyList<OrderDetail> OrderDetails = await OrderDetailsRepo.GetAllAsync();
            if (OrderDetails.Count == 0)
            {
                return StatusCode(200, "No OrderDetails Found");
            }

            var OrderDetailsDTOs = new List<OrderDetailsDTO>();
            foreach (var OrderDetail in OrderDetails)
            {
                var OrderDetailsDTO = new OrderDetailsDTO
                {
                    Order = OrderDetail.Order.Date,
                    UnitPrice=OrderDetail.UnitPrice,
                    Quantity = OrderDetail.Quantity,
                    ProductName = OrderDetail.Product.BrandName,
                    SectionName = OrderDetail.Product.Section.SectionName
                };

                OrderDetailsDTOs.Add(OrderDetailsDTO);
            }

            return Ok(OrderDetailsDTOs);
        }



        [HttpGet("{id1:int}/{id2:int}", Name = "GetOneOrderDetailsRoute")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int id1,int id2)
        {
            OrderDetail OrderDetails = await OrderDetailsRepo.GetByIdAsync(id1,id2);
            if (OrderDetails == null)
            {
                return NotFound("This OrderDetails Not Found");
            }
            var OrderDetailsDTO = new OrderDetailsDTO()
            {
                Order = OrderDetails.Order.Date,
                UnitPrice = OrderDetails.UnitPrice,
                Quantity = OrderDetails.Quantity,
                ProductName = OrderDetails.Product.BrandName,
                SectionName = OrderDetails.Product.Section.SectionName
            };
            return Ok(OrderDetailsDTO);
        }



        [HttpPost]
        public async Task<ActionResult> AddOrderDetails(OrderDetail OrderDetails)
        {
            if (ModelState.IsValid)
            {
                String result = await OrderDetailsRepo.AddAsync(OrderDetails);
                await Console.Out.WriteLineAsync($"AddResult=>{result}");
                string url = Url.Link("GetOneOrderDetailsRoute", new { id1 = OrderDetails.OrderId,id2=OrderDetails.ProductId});
                return Created(url, $"This OrderDetails : {OrderDetails.OrderId}/{OrderDetails.ProductId} is Added");
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id1:int}/{id2:int}")]
        public async Task<ActionResult> UpdateOrderDetails(int id1,int id2, OrderDetail OrderDetails)
        {
            if (ModelState.IsValid)
            {
                if (id1 > 0 && id2 > 0)
                {
                    OrderDetail c = await OrderDetailsRepo.GetByIdAsync(id1,id2);
                    if (c != null)
                    {
                        string result = await OrderDetailsRepo.EditAsync(id1,id2, OrderDetails);
                        await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, $"This OrderDetails : {c.OrderId}/{c.ProductId} is Edited");
                    }
                    else
                        return StatusCode(404, "This OrderDetails Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id1:int}/{id2:int}")]
        public async Task<ActionResult<OrderDetail>> DeleteOrderDetails(int id1,int id2)
        {
            if (id1 > 0 && id2 > 0)
            {
                try
                {
                    OrderDetail c = await OrderDetailsRepo.DeleteAsync(id1,id2);
                    if (c == null)
                        return NotFound("This OrderDetails Not Found");
                    else
                        return StatusCode(200, $"This OrderDetails : {c.OrderId} / {c.ProductId} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid OrderDetailsID");
        }
    }
}
