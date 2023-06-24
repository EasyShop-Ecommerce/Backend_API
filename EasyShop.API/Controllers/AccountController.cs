using EasyShop.API.DTOs;
using EasyShop.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager,IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config=config;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register (RegisterDTO registerDTO)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = registerDTO.Email; 
                user.UserName = registerDTO.UserName;
                var result= await _userManager.CreateAsync(user,registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account is Added Sucessfully");
                }
                else
                {
                    var errors=new List<IdentityError>();
                    foreach(var error in result.Errors)
                    {
                        errors.Add(error);
                    }
                    return BadRequest(errors);
                }
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login (LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user =await  _userManager.FindByNameAsync(loginDTO.UserName);
                if(user != null)
                {
                    bool found=await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (found)
                    {
                        // Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        // Get Role
                        var roles=await _userManager.GetRolesAsync(user);
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
                            claims:claims,
                            expires:DateTime.Now.AddHours(2),
                            signingCredentials:credentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Token),
                            expiration = Token.ValidTo
                        }) ;
                    }
                    return Unauthorized();    
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }
    }
}
