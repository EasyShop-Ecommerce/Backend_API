using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SellerAccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly DBContext _context;


        public SellerAccountController(UserManager<AppUser> userManager, IConfiguration config, DBContext context)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(SellerRegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var seller = new Seller();


                AppUser user = new AppUser();
                user.UserName = registerDTO.FirstName;
                user.MiddleName = registerDTO.MiddleName;
                user.LastName = registerDTO.LastName;
                user.BusinessName = registerDTO.BusinessName;
                user.Phone = registerDTO.Phone;
                user.Email = registerDTO.Email;
                user.Street = registerDTO.Street;
                user.City = registerDTO.City;
                user.Government = registerDTO.Government;
                user.CustomerID = null;

                seller.FirstName = registerDTO.FirstName;
                seller.MiddleName = registerDTO.MiddleName;
                seller.LastName = registerDTO.LastName;
                seller.Phone = registerDTO.Phone;
                seller.Email = registerDTO.Email;
                seller.BusinessName = registerDTO.BusinessName;
                seller.City = registerDTO.City;
                seller.Governorate = registerDTO.Government;
                seller.Street = registerDTO.Street;
                seller.Password = registerDTO.Password;
                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();

                user.SellerID = seller.Id;
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "seller");
                    return Ok(seller);
                }
                else
                {
                    //var errors=new List<IdentityError>();
                    //foreach(var error in result.Errors)
                    //{
                    //    errors.Add(error);
                    //}
                    return BadRequest(result.Errors);
                }
            }

            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (found)
                    {
                        // Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim("Id", user.Id.ToString()));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        claims.Add(new Claim(ClaimTypes.Role, "seller"));

                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        // create Token
                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: _config["JWT:ValidIssuer"],
                            audience: _config["JWT:ValidAudience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(2),
                            signingCredentials: credentials
                            );
                        var token = new JwtSecurityTokenHandler().WriteToken(Token);
                        user.Token = token;
                        loginDTO.Token = token;
                        loginDTO.Expiration = Token.ValidTo;
                        loginDTO.name = user.UserName;
                        loginDTO.sellerId = user.SellerID;

                        return Ok(loginDTO);
                    }
                    return Unauthorized("UnAuthorized Seller");
                }
                else
                    return Unauthorized("UnAuthorized Seller");
            }
            else
                return Unauthorized("UnAuthorized Seller");
        }
    }
}
