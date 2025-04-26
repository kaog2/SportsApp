using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Data;
using SportsApp.API.DTOs.Reservation;
using SportsApp.API.Enum;
using SportsApp.API.Models;
using SportsApp.API.Services;
using System;
using System.Threading.Tasks;

namespace SportsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly INotificationService _notificationService;

        public ReservationController(SportsAppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto createDto)
        {
            // Here you would get the current logged-in user
            var userId = new Guid(User.FindFirst("sub")?.Value);

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                CourtId = createDto.CourtId,
                UserId = userId,
                StartTime = createDto.StartTime,
                EndTime = createDto.EndTime,
                Status = ReservationStatus.Active
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Send confirmation email
            var user = await _context.Users.FindAsync(userId);
            await _notificationService.SendEmailAsync(
                user.Email,
                "Reservation confirmed",
                $"Your reservation is confirmed for {reservation.StartTime:G}."
            );

            return Ok(new { reservation.Id });
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

            await _notificationService.SendEmailAsync(
                reservation.User.Email,
                "Reservation cancelled",
                $"Your reservation on {reservation.StartTime:G} has been cancelled."
            );

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

            await _notificationService.SendEmailAsync(
                reservation.User.Email,
                "Reservation updated",
                $"Your reservation has been changed to start at {reservation.StartTime:G}."
            );


            return Ok("Reservation updated successfully.");
        }
    }
}
