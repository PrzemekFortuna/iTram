using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class GeomagneticRotation
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? GeomagneticRotationVecX { get; protected set; }
        public double? GeomagneticRotationVecY { get; protected set; }
        public double? GeomagneticRotationVecZ { get; protected set; }
    }
}