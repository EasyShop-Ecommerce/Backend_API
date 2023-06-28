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
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<Order> OrderRepo;
        private readonly IMapper mapper;

        public OrderController(IGenericRepository<Order> _OrderRepo, IMapper _mapper)
        {
            OrderRepo = _OrderRepo;
            mapper = _mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
            IReadOnlyList<Order> Orders = await OrderRepo.GetAllAsync();
            if (Orders.Count == 0)
            {
                return StatusCode(200, "No Orders Found");
            }

            var OrderDTOs = new List<OrderDTO>();
            foreach (var order in Orders)
            {
                var orderDTO = new OrderDTO
                {
                    Date = DateTime.Now,
                    TotalPrice = order.TotalPrice,
                    ShipPrice = order.ShipPrice,
                    Customer = order.Customer.Name,
                    CustomerPhone = order.Customer.Phone,
                    SellerName = order.Seller.FirstName,
                   // ShipperName = order.Shipper.Name,
                    PaymentMethod = order.PaymentMethod.Method,
                    Status = order.Status.StatusName
                };
                foreach (var product in order.OrderDetails)
                {
                    orderDTO.Products.Add(product.Product.BrandName);
                }

                foreach (var product in order.OrderDetails)
                {
                    orderDTO.Quantity.Add(product.Quantity);
                }

                OrderDTOs.Add(orderDTO);
            }

            return Ok(OrderDTOs);
        }

        [HttpGet("{id:int}", Name = "GetOneOrderRoute")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            Order Order = await OrderRepo.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound("This Order Not Found");
            }
            var OrderDTO = new OrderDTO()
            {

                Date = DateTime.Now,
                TotalPrice = Order.TotalPrice,
                ShipPrice = Order.ShipPrice,
                Customer = Order.Customer.Name,
                CustomerPhone = Order.Customer.Phone,
                SellerName = Order.Seller.FirstName,
               // ShipperName = Order.Shipper.Name,
                PaymentMethod = Order.PaymentMethod.Method,
                Status = Order.Status.StatusName
            };
            foreach (var product in Order.OrderDetails)
            {
                OrderDTO.Products.Add(product.Product.BrandName);
            }

            foreach (var product in Order.OrderDetails)
            {
                OrderDTO.Quantity.Add(product.Quantity);
            }

            return Ok(OrderDTO);
        }



        [HttpPost]
        public async Task<ActionResult> AddOrder(Order Order)
        {
            if (ModelState.IsValid)
            {
                Order createdOrder = await OrderRepo.AddAsync(Order);
                await Console.Out.WriteLineAsync($"AddResult=>{createdOrder}");
                string url = Url.Link("GetOneOrderRoute", new { id = Order.Id });
                return Created(url, createdOrder);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOrder(int id, Order Order)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    Order c = await OrderRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        await OrderRepo.EditAsync(id, Order);
                        //await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, Order);
                    }
                    else
                        return StatusCode(404, "This Order Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            if (id > 0)
            {
                try
                {
                    Order c = await OrderRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This Order Not Found");
                    else
                        return StatusCode(200, $"This Order : {c.Id} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid OrderID");
        }
    }
}
