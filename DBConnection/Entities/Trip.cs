using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBConnection.Entities
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public float Length { get; set; }
        public bool IsFinished { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int TramId { get; set; }
        public Tram Tram { get; set; }
    }
}
