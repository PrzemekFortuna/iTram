namespace DBConnection.DTO
{
    public class SensorsReadingUnitsDTO : SensorsReadingDTO
    {
        public string AccelerometerUnit { get; set; }
        public string GyroscopeUnit { get; set; }
        public string LocationUnit { get; set; }

    }
}