namespace Services.Handlers.Gyroscope
{
    public class RadPerSHandler : GyroscopeHandler
    {
        public override object Handle(object request)
        {
            var unit = "rad/s";
            var req = request as DBConnection.Entities.Sensors.Gyroscope;
            if (req.GyroscopeUnit== unit)
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
