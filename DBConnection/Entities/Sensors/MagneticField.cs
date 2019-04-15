using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class MagneticField
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? MagneticFieldX { get; protected set; }
        public double? MagneticFieldY { get; protected set; }
        public double? MagneticFieldZ { get; protected set; }
    }
}