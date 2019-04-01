using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class Gyroscope
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? Gx { get; protected set; }
        public double? Gy { get; protected set; }
        public double? Gz { get; protected set; }
        [NotMapped]
        public string GyroscopeUnit { get; protected set; }

        public void SetGyroscope(string unit, double? x, double? y, double? z)
        {
            GyroscopeUnit = unit;
            Gx = x;
            Gy = y;
            Gz = z;
        }
    }
}