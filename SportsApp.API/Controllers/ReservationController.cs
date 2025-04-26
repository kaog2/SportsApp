using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Data;
using SportsApp.API.DTOs.Reservation;
using SportsApp.API.Enum;
using SportsApp.API.Models;
using System;
using System.Threading.Tasks;

namespace SportsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public ReservationController(SportsAppDbContext context)
        {
            _context = context;
        }

        // Cancel a reservation
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelReservation(Guid id, [FromBody] CancelReservationDto cancelDto)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
                return NotFound("Reservation not found.");

            if (reservation.Status != ReservationStatus.Active)
                return BadRequest($"Reservation cannot be cancelled. Current status: {reservation.Status}");

            if (reservation.StartTime <= DateTime.UtcNow.AddHours(1))
                return BadRequest("Cannot cancel a reservation less than 1 hour before start time.");

            reservation.Status = ReservationStatus.Cancelled;
            reservation.Notes = cancelDto.Reason;

            await _context.SaveChangesAsync();

            return Ok("Reservation cancelled successfully.");
        }

        // Update (reschedule) a reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] UpdateReservationDto updateDto)
        {
            var reservation = await _context.Reservations.Include(r => r.Court).FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound("Reservation not found.");

            if (reservation.Status != ReservationStatus.Active)
                return BadRequest($"Reservation cannot be modified. Current status: {reservation.Status}");

            if (reservation.StartTime <= DateTime.UtcNow.AddHours(1))
                return BadRequest("Cannot modify a reservation less than 1 hour before start time.");

            // Update reservation
            reservation.StartTime = updateDto.NewStartTime;
            reservation.EndTime = updateDto.NewEndTime;
            reservation.Status = ReservationStatus.Modified;
            reservation.Notes = updateDto.Reason;

            await _context.SaveChangesAsync();

            return Ok("Reservation updated successfully.");
        }
    }
}
