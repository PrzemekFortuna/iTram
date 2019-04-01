namespace DBConnection.DTO
{
    public class SensorsReadingDTO
    {
        public int Id { get; set; }
        public double Ax { get; set; }
        public double? Ay { get; set; }
        public double? Az { get; set; }

        public double? Gx { get; set; }
        public double? Gy { get; set; }
        public double? Gz { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        
        public int? BatteryLevel { get; set; }
    }
}