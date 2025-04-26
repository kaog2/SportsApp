using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Models;
using System;

namespace SportsApp.API.Data
{
    public class SportsAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SportsAppDbContext(DbContextOptions<SportsAppDbContext> options) : base(options) { }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}
