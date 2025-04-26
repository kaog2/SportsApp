// DTOs/Match/CreateMatchDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace SportsApp.API.DTOs.Match
{
    public class CreateMatchDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Range(2, 20)]
        public int MaxPlayers { get; set; } = 4;

        public bool IsPublic { get; set; } = true;
    }

    public class MatchResponseDto
    {
        public Guid Id { get; set; }
        public string? FacilityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxPlayers { get; set; }
        public bool IsPublic { get; set; }
    }
}
