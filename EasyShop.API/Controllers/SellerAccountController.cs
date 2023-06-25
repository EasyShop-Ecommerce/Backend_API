using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using EasyShop.Infrastructure.Data;
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
                var seller = new Seller()
                {
                    FirstName = registerDTO.FirstName,
                    MiddleName= registerDTO.MiddleName,
                    LastName = registerDTO.LastName,
                    Phone = registerDTO.Phone,
                    Email = registerDTO.Email,
                    BusinessName=registerDTO.BusinessName,
                    City = registerDTO.City,
                    Governorate = registerDTO.Government,
                    Street = registerDTO.Street,
                };
                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();

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
                user.SellerID = seller.Id;
                user.CustomerID = null;
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {

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
                        //  claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // Get Role
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

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
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token),
                            expiration = Token.ValidTo
                        });
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
