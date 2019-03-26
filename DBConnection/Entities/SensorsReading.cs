namespace DBConnection.Entities
{
    public class SensorsReading
    {
        public int Id { get; set; }
        public double Ax { get; set; }
        public double Ay { get; set; }
        public double Az { get; set; }
        public double Gx { get; set; }
        public double Gy { get; set; }
        public double Gz { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int BatteryLevel { get; set; }
    }
}