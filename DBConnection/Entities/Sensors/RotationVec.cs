using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class RotationVec
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? RotationVecX { get; protected set; }
        public double? RotationVecY { get; protected set; }
        public double? RotationVecZ { get; protected set; }
    }
}