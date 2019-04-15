using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.DTO
{
    public class TripResDTO
    {
        public int TripId { get; set; }
        public int TramId { get; set; }
        public int UserId { get; set; }
        public bool IsFinished { get; set; }
        public float Length { get; set; }
        public DateTime StartTime { get; set; }

    }
}
