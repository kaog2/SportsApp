using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Data;
using SportsApp.API.DTOs.Match;
using SportsApp.API.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MatchController(SportsAppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            // Validación de colisión horaria
            var overlappingMatch = await _context.Matches
                .Where(m => m.FacilityId == dto.FacilityId &&
                            ((dto.StartTime >= m.StartTime && dto.StartTime < m.EndTime) ||
                             (dto.EndTime > m.StartTime && dto.EndTime <= m.EndTime)))
                .FirstOrDefaultAsync();

            if (overlappingMatch != null)
            {
                return BadRequest("Ya hay un partido agendado en ese horario en esta instalación.");
            }

            var match = new Match
            {
                Id = Guid.NewGuid(),
                FacilityId = dto.FacilityId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                MaxPlayers = dto.MaxPlayers,
                IsPublic = dto.IsPublic,
                Players = new List<ApplicationUser> { user }
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return Ok(new { match.Id });
        }

        [Authorize]
        [HttpPost("{matchId}/join")]
        public async Task<IActionResult> JoinMatch(Guid matchId)
        {
            var user = await _userManager.GetUserAsync(User);
            var match = await _context.Matches.Include(m => m.Players).FirstOrDefaultAsync(m => m.Id == matchId);

            if (match == null) return NotFound();
            if (match.Players.Any(p => p.Id == user.Id)) return BadRequest("Ya estás unido a este partido.");
            if (match.Players.Count >= match.MaxPlayers) return BadRequest("El partido está completo.");

            match.Players.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyMatches()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var matches = await _context.Matches
                .Include(m => m.Facility)
                .Include(m => m.Players)
                .Where(m => m.Players.Any(p => p.Id.ToString() == userId))
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetPublicMatches()
        {
            var matches = await _context.Matches
                .Include(m => m.Facility)
                .Where(m => m.IsPublic && m.StartTime > DateTime.UtcNow)
                .ToListAsync();

            return Ok(matches);
        }
    }
}
