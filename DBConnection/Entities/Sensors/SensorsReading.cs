using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DBConnection.Entities.Sensors;

namespace DBConnection.Entities
{
    public class SensorsReading
    {
        public int Id { get; protected set; }

        public Accelerometer Accelerometer { get; protected set; }
        public Gyroscope Gyroscope { get; protected set; }
        public Location Location { get; protected set; }
        public int? BatteryLevel { get; protected set; }
    }
}