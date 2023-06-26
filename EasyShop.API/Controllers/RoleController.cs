using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("{roleName}")]
        public async Task< ActionResult > AddRole(string roleName) 
        {
            if(ModelState.IsValid)
            {

                 IdentityRole  identityRole=new IdentityRole() { Name=roleName};
                 IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return Ok("Role is Added Successfully");
                }
                else
                {
                    var errors=new List<string>();
                    foreach(var error in result.Errors)
                    {
                        errors.Add(error.Description.ToString());
                    }
                    return BadRequest(errors);
                }
            }
            else
            {
                return BadRequest("");
            }
        }
    }
}
