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
    public class ShipperController : ControllerBase
    {
        private readonly IGenericRepository<Shipper> ShipperRepo;
        private readonly IMapper mapper;

        public ShipperController(IGenericRepository<Shipper> _ShipperRepo, IMapper _mapper)
        {
            ShipperRepo = _ShipperRepo;
            mapper = _mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<ShipperDTO>>> GetAllShippers()
        {
            IReadOnlyList<Shipper> shippers = await ShipperRepo.GetAllAsync();
            if (shippers.Count == 0)
            {
                return StatusCode(200, "No shippers Found");
            }

            var ShipperDTOs = new List<ShipperDTO>();
            foreach (var shipper in shippers)
            {
                var shipperDTO = new ShipperDTO
                {
                    Id = shipper.Id,
                    Name = shipper.Name,
                    PricePerKm = shipper.PricePerKm,
                    Email=shipper.Email,
                    Password=shipper.Password

                };

                foreach (var product in shipper.Products)
                {
                    shipperDTO.ShipperProducts.Add(product.Id);
                }

                //foreach (var order in shipper.Orders)
                //{
                //    shipperDTO.orders.Add(order.Date);
                //}

                //foreach (var order in shipper.Orders)
                //{
                //    shipperDTO.ordersPrice.Add(order.TotalPrice);
                //}

                //foreach (var order in shipper.Orders)
                //{
                //    shipperDTO.ordersShip.Add(order.ShipPrice);
                //}

                ShipperDTOs.Add(shipperDTO);
            }

            return Ok(ShipperDTOs);
        }



        [HttpGet("{id:int}", Name = "GetOneShipperRoute")]
        public async Task<ActionResult<ShipperDTO>> GetShipper(int id)
        {
            Shipper Shipper = await ShipperRepo.GetByIdAsync(id);
            if (Shipper == null)
            {
                return NotFound("This Shipper Not Found");
            }
            var ShipperDTO = new ShipperDTO()
            {
                Name = Shipper.Name,
                PricePerKm = Shipper.PricePerKm,
                Email = Shipper.Email,
                Password = Shipper.Password

            };

            foreach (var product in Shipper.Products)
            {
                ShipperDTO.ShipperProducts.Add(product.Id);
            }

            //foreach (var order in Shipper.Orders)
            //{
            //    ShipperDTO.orders.Add(order.Date);
            //}

            //foreach (var order in Shipper.Orders)
            //{
            //    ShipperDTO.ordersPrice.Add(order.TotalPrice);
            //}

            //foreach (var order in Shipper.Orders)
            //{
            //    ShipperDTO.ordersShip.Add(order.ShipPrice);
            //}


            return Ok(ShipperDTO);
        }



        [HttpPost]
        public async Task<ActionResult> AddShipper(Shipper Shipper)
        {
            if (ModelState.IsValid)
            {
                Shipper createdShipper = await ShipperRepo.AddAsync(Shipper);
                await Console.Out.WriteLineAsync($"AddResult=>{createdShipper}");
                string url = Url.Link("GetOneShipperRoute", new { id = Shipper.Id });
                return Created(url, createdShipper);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateShipper(int id, Shipper Shipper)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    Shipper c = await ShipperRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        await ShipperRepo.EditAsync(id, Shipper);
                        await Console.Out.WriteLineAsync($"EditResult=>{Shipper}");
                        return StatusCode(200, Shipper);
                    }
                    else
                        return StatusCode(404, "This Shipper Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Shipper>> DeleteShipper(int id)
        {
            if (id > 0)
            {
                try
                {
                    Shipper c = await ShipperRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This Shipper Not Found");
                    else
                        return StatusCode(200, $"This Shipper : {c.Name} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid ShipperID");
        }
    }
}
