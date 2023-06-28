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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IGenericRepository<PaymentMethod> PaymentMethodRepo;
        private readonly IMapper mapper;

        public PaymentMethodController(IGenericRepository<PaymentMethod> _PaymentMethodRepo, IMapper _mapper)
        {
            PaymentMethodRepo = _PaymentMethodRepo;
            mapper = _mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<PaymentMethodDTO>>> GetAllPaymentMethods()
        {
            IReadOnlyList<PaymentMethod> PaymentMethods = await PaymentMethodRepo.GetAllAsync();
            if (PaymentMethods.Count == 0)
            {
                return StatusCode(200, "No PaymentMethods Found");
            }

            var PaymentMethodDTOs = new List<PaymentMethodDTO>();
            foreach (var PaymentMethod in PaymentMethods)
            {
                var PaymentMethodDTO = new PaymentMethodDTO
                {
                    Method = PaymentMethod.Method,
                };

                var orderDTos = new List<OrderDTO>();

                foreach (var order in PaymentMethod.Orders)
                {
                    var orderDTO = new OrderDTO
                    {
                        Date = order.Date,
                        TotalPrice = order.TotalPrice,
                        ShipPrice = order.ShipPrice,
                        Customer = order.Customer.Name
                    };
                    orderDTos.Add(orderDTO);
                }

                PaymentMethodDTO.Orders = orderDTos;
                PaymentMethodDTOs.Add(PaymentMethodDTO);
            }

            return Ok(PaymentMethodDTOs);
        }



        [HttpGet("{id:int}", Name = "GetOnePaymentMethodRoute")]
        public async Task<ActionResult<PaymentMethodDTO>> GetPaymentMethod(int id)
        {
            PaymentMethod PaymentMethod = await PaymentMethodRepo.GetByIdAsync(id);
            if (PaymentMethod == null)
            {
                return NotFound("This PaymentMethod Not Found");
            }
            var PaymentMethodDTO = new PaymentMethodDTO()
            {

                Method= PaymentMethod.Method,

            };
            var orderDTos = new List<OrderDTO>();

            foreach (var order in PaymentMethod.Orders)
            {
                var orderDTO = new OrderDTO
                {
                    Date = order.Date,
                    TotalPrice = order.TotalPrice,
                    ShipPrice = order.ShipPrice,
                    Customer = order.Customer.Name
                };
                orderDTos.Add(orderDTO);
            }

            PaymentMethodDTO.Orders = orderDTos;

            return Ok(PaymentMethodDTO);
        }



        [HttpPost]
        public async Task<ActionResult> AddPaymentMethod(PaymentMethod PaymentMethod)
        {
            if (ModelState.IsValid)
            {
                PaymentMethod createdPaymentMethod = await PaymentMethodRepo.AddAsync(PaymentMethod);
                await Console.Out.WriteLineAsync($"AddResult=>{createdPaymentMethod}");
                string url = Url.Link("GetOnePaymentMethodRoute", new { id = PaymentMethod.Id });
                return Created(url, createdPaymentMethod);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePaymentMethod(int id, PaymentMethod PaymentMethod)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    PaymentMethod c = await PaymentMethodRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        await PaymentMethodRepo.EditAsync(id, PaymentMethod);
                       // await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, PaymentMethod);
                    }
                    else
                        return StatusCode(404, "This PaymentMethod Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(int id)
        {
            if (id > 0)
            {
                try
                {
                    PaymentMethod c = await PaymentMethodRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This PaymentMethod Not Found");
                    else
                        return StatusCode(200, $"This PaymentMethod : {c.Method} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid PaymentMethodID");
        }

    }
}
