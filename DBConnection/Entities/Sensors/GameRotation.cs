using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class GameRotation
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public double? GameRotationVecX { get; set; }
        public double? GameRotationVecY { get; set; }
        public double? GameRotationVecZ { get; set; }
    }
}