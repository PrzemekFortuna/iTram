using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class Gravity
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? GravityX { get; protected set; }
        public double? GravityY { get; protected set; }
        public double? GravityZ { get; protected set; }
    }
}