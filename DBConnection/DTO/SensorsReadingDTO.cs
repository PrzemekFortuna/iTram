using System;

namespace DBConnection.DTO
{
    public class SensorsReadingDTO
    {
        public int Id { get; set; }
        public string NearestBeaconId { get; set; }

        public double? Ax { get; set; }
        public double? Ay { get; set; }
        public double? Az { get; set; }

        public double? Gx { get; set; }
        public double? Gy { get; set; }
        public double? Gz { get; set; }

        public double? Light { get; set; }

        public double? Pressure { get; set; }

         public double? GameRotationVecX { get; set; }
         public double? GameRotationVecY { get; set; }
         public double? GameRotationVecZ { get; set; }

         public double? MagneticFieldX { get; set; }
         public double? MagneticFieldY { get; set; }
         public double? MagneticFieldZ { get; set; }

         public double? GeomagneticRotationVecX { get; set; }
         public double? GeomagneticRotationVecY { get; set; }
         public double? GeomagneticRotationVecZ { get; set; }

         public double? GravityX { get; set; }
         public double? GravityY { get; set; }
         public double? GravityZ { get; set; }

         public double? RotationVecX { get; set; }
         public double? RotationVecY { get; set; }
         public double? RotationVecZ { get; set; }

        public float NumberOfSteps { get; set; }
         public float StepDetector { get; set; }

        public double? Proximity { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        
        public int? BatteryLevel { get; set; }

        public bool? ImInTram { get; set; }
    }
}