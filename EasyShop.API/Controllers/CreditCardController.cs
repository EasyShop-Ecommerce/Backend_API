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
    public class CreditCardController : ControllerBase
    {

        private readonly IGenericRepository<CreditCard> CreditCardRepo;
        private readonly IMapper mapper;

        public CreditCardController(IGenericRepository<CreditCard> _CreditCardRepo, IMapper _mapper)
        {
            CreditCardRepo = _CreditCardRepo;
            mapper = _mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<CreditCardDTO>>> GetAllCreditCards()
        {
            IReadOnlyList<CreditCard> CreditCards = await CreditCardRepo.GetAllAsync();
            if (CreditCards.Count == 0)
            {
                return StatusCode(200, "No CreditCards Found");
            }

            var CreditCardDTOs = new List<CreditCardDTO>();
            foreach (var CreditCard in CreditCards)
            {
                var CreditCardDTO = new CreditCardDTO
                {
                    Cardholder_name = CreditCard.Cardholder_name,
                    CardNumber=CreditCard.CardNumber,
                    ExpirationDate = CreditCard.ExpirationDate,
                    Customer=CreditCard.Customer.Name

                };

                CreditCardDTOs.Add(CreditCardDTO);
            }

            return Ok(CreditCardDTOs);
        }



        [HttpGet("{id:int}", Name = "GetOneCreditCardRoute")]
        public async Task<ActionResult<CreditCardDTO>> GetCreditCard(int id)
        {
            CreditCard CreditCard = await CreditCardRepo.GetByIdAsync(id);
            if (CreditCard == null)
            {
                return NotFound("This CreditCard Not Found");
            }
            var CreditCardDTO = new CreditCardDTO()
            {

                Cardholder_name = CreditCard.Cardholder_name,
                CardNumber = CreditCard.CardNumber,
                ExpirationDate = CreditCard.ExpirationDate,
                Customer = CreditCard.Customer.Name
            };

            return Ok(CreditCardDTO);
        }



        [HttpPost]
        public async Task<ActionResult> AddCreditCard(CreditCard CreditCard)
        {
            if (ModelState.IsValid)
            {
                String result = await CreditCardRepo.AddAsync(CreditCard);
                await Console.Out.WriteLineAsync($"AddResult=>{result}");
                string url = Url.Link("GetOneCreditCardRoute", new { id = CreditCard.Id });
                return Created(url, $"This CreditCard : {CreditCard.Cardholder_name} is Added");
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCreditCard(int id, CreditCard CreditCard)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    CreditCard c = await CreditCardRepo.GetByIdAsync(id);
                    if (c != null)
                    {
                        string result = await CreditCardRepo.EditAsync(id, CreditCard);
                        await Console.Out.WriteLineAsync($"EditResult=>{result}");
                        return StatusCode(200, $"This CreditCard : {c.Cardholder_name} is Edited");
                    }
                    else
                        return StatusCode(404, "This CreditCard Not Found");
                }
                else
                    return StatusCode(404, "Invalid ID");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CreditCard>> DeleteCreditCard(int id)
        {
            if (id > 0)
            {
                try
                {
                    CreditCard c = await CreditCardRepo.DeleteAsync(id);
                    if (c == null)
                        return NotFound("This CreditCard Not Found");
                    else
                        return StatusCode(200, $"This CreditCard : {c.Cardholder_name} is Deleted");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                return BadRequest("Invalid CreditCardID");
        }
    }
}
