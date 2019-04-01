using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class Accelerometer
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? Ax { get; protected set; }
        public double? Ay { get; protected set; }
        public double? Az { get; protected set; }
        [NotMapped]
        public string AccelerometerUnit { get; protected set; }
    }
}