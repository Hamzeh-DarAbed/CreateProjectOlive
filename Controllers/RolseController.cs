using CreateProjectOlive.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreateProjectOlive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleMgr)
        {
            _roleManager = roleMgr;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRoleDto addRoleDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(addRoleDto.Name));
                    if (result.Succeeded)
                    {

                        return Ok("Role Created");
                    }
                    else
                    {

                        return BadRequest(result.Errors);
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
            }
            return BadRequest();
        }
    }
}