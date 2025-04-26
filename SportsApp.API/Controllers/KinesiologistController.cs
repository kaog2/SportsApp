using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsApp.API.DTOs;
using SportsApp.API.Models;

namespace SportsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class KinesiologistController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public KinesiologistController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKinesiologist([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Role = "Kinesiologist",
                Specialty = model.Specialty
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "Kinesiologist");

            return Ok("Kinesiologist created successfully.");
        }
    }
}
