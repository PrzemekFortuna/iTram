using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DBConnection.Entities.Sensors;

namespace DBConnection.Entities
{
    public class SensorsReading
    {
        public int Id { get; protected set; }        
        public int UserId { get; set; }
        public string NearestBeaconId { get; protected set; }
        public DateTime TimeStamp { get; set; }

        public Accelerometer Accelerometer { get; protected set; }
        public Gyroscope Gyroscope { get; protected set; }
        public Location Location { get; protected set; }
        public GameRotation GameRotation { get; protected set; }
        public GeomagneticRotation GeomagneticRotation { get; protected set; }
        public MagneticField MagneticField { get; protected set; }
        public Gravity Gravity { get; protected set; }
        public RotationVec RotationVec { get; protected set; }
        public double? Light { get; protected set; }
        public float? StepDetector { get; protected set; }
        public double? Pressure { get; protected set; }
        public double? Proximity { get; set; }
        public float? NumberOfSteps { get; set; }

        public int? BatteryLevel { get; protected set; }
        public bool? ImInTram { get; protected set; }
    }
}