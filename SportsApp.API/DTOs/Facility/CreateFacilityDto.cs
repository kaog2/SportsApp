// DTOs/Facility/CreateFacilityDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace SportsApp.API.DTOs.Facility
{
    public class CreateFacilityDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public string? Surface { get; set; }

        [Range(0, 100000)]
        public decimal PricePerHour { get; set; }

        [Required]
        public Guid FacilityTypeId { get; set; } // Foreign Key

        public string? Sport { get; set; } // Solo para Court
    }

    public class FacilityResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Surface { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Sport { get; set; }
    }
}
