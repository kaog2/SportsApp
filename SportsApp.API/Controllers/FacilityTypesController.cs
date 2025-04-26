using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Data;

namespace SportsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityTypesController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public FacilityTypesController(SportsAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var types = await _context.FacilityTypes.ToListAsync();
            return Ok(types);
        }
    }

}
