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
    public class StatusController : ControllerBase
    {

        private readonly IGenericRepository<Status> StatusRepo;
        private readonly IMapper mapper;

        public StatusController(IGenericRepository<Status> _StatusRepo, IMapper _mapper)
        {
            StatusRepo = _StatusRepo;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StatusDTO>>> GetAllStatuss()
        {
            IReadOnlyList<Status> Statuss = await StatusRepo.GetAllAsync();
            if (Statuss.Count == 0)
            {
                return StatusCode(200, "No Statuss Found");
            }

            var StatusDTOs = new List<StatusDTO>();
            foreach (var Status in Statuss)
            {
                var StatusDTO = new StatusDTO
                {
                    StatusName = Status.StatusName,

                };

                foreach (var order in Status.Orders)
                {
                    StatusDTO.orders.Add(order.Date);
                }

                StatusDTOs.Add(StatusDTO);
            }

            return Ok(StatusDTOs);
        }


        [HttpGet("{id:int}", Name = "GetOneStatusRoute")]
        public async Task<ActionResult<StatusDTO>> GetStatus(int id)
        {
            Status Status = await StatusRepo.GetByIdAsync(id);
            if (Status == null)
            {
                return NotFound("This Status Not Found");
            }
            var StatusDTO = new StatusDTO()
            {

                StatusName = Status.StatusName,

            };

            foreach (var order in Status.Orders)
            {
                StatusDTO.orders.Add(order.Date);
            }


            return Ok(StatusDTO);
        }


        [HttpPost]
        public async Task<ActionResult> AddStatus(Status Status)
        {
            if (ModelState.IsValid)
            {
                Status createdStatus= await StatusRepo.AddAsync(Status);
                await Console.Out.WriteLineAsync($"AddResult=>{createdStatus}");
                string url = Url.Link("GetOneStatusRoute", new { id = Status.Id });
                return Created(url, createdStatus);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateStatus(int id, Status Status)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    Status c = await StatusRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        string result = await StatusRepo.EditAsync(id, Status);
                        await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, $"This Status : {c.StatusName} is Edited");
                    }
                    else
                        return StatusCode(404, "This Status Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Status>> DeleteStatus(int id)
        {
            if (id > 0)
            {
                try
                {
                    Status c = await StatusRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This Status Not Found");
                    else
                        return StatusCode(200, $"This Status : {c.StatusName} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid StatusID");
        }
    }
}
