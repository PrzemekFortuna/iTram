namespace Services.Handlers.Accelerometer
{
    public class MeterPerSecondSquaredHandler : AccelerometerHandler
    {
        public override object Handle(object request)
        {
            var unit = "m/s^2";
            var req = request as DBConnection.Entities.Sensors.Accelerometer;
            if (req.AccelerometerUnit == unit)
            {
                return this;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}