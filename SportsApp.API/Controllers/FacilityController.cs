using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Data;
using SportsApp.API.DTOs.Facility;
using SportsApp.API.Models;
using System;
using System.Threading.Tasks;

namespace SportsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public FacilityController(SportsAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facilities = await _context.Facilities.ToListAsync();
            return Ok(facilities);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFacilityDto dto)
        {
            var type = await _context.FacilityTypes.FindAsync(dto.FacilityTypeId);
            if (type == null) return BadRequest("Tipo de instalación no válido.");

            Facility facility;

            switch (type.Name)
            {
                case "Court":
                    facility = new Court
                    {
                        Id = Guid.NewGuid(),
                        Name = dto.Name,
                        Location = dto.Location,
                        Surface = dto.Surface,
                        PricePerHour = dto.PricePerHour,
                        Sport = dto.Sport,
                        FacilityTypeId = dto.FacilityTypeId
                    };
                    _context.Courts.Add((Court)facility);
                    break;

                default:
                    return BadRequest("Tipo de instalación no soportado.");
            }

            await _context.SaveChangesAsync();
            return Ok(new { facility.Id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateFacilityDto dto)
        {
            var type = await _context.FacilityTypes.FindAsync(dto.FacilityTypeId);
            if (type == null) return BadRequest("Tipo de instalación no válido.");

            switch (type.Name)
            {
                case "Court":
                    var court = await _context.Courts.FindAsync(id);
                    if (court == null) return NotFound();

                    court.Name = dto.Name;
                    court.Location = dto.Location;
                    court.Surface = dto.Surface;
                    court.PricePerHour = dto.PricePerHour;
                    court.Sport = dto.Sport;
                    court.FacilityTypeId = dto.FacilityTypeId;
                    break;

                default:
                    return BadRequest("Tipo de instalación no soportado.");
            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var facility = await _context.Courts.FindAsync(id);
            if (facility == null) return NotFound();

            _context.Courts.Remove(facility);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
