using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entities.Sensors
{
    [Table("SensorsReadings")]
    public class Location
    {
        [Key, ForeignKey("Reading")]
        public int Id { get; protected set; }
        public SensorsReading Reading { get; set; }
        public string Latitude { get; protected set; }
        public string Longitude { get; protected set; }
        [NotMapped]
        public string LocationUnit { get; protected set; }

        public void SetLocation(string unit, string latitude, string longitude)
        {
            LocationUnit = unit;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}