using System;

namespace Services.Handlers.Gyroscope
{
    public class RpmHandler : GyroscopeHandler
    {

        public override object Handle(object request)
        {
            var unit = "rpm";
            var req = request as DBConnection.Entities.Sensors.Gyroscope;
            if (req.GyroscopeUnit == unit)
            {
                var x = req.Gx * 2 * Math.PI / 60;
                var y = req.Gy * 2 * Math.PI / 60;
                var z = req.Gz * 2 * Math.PI / 60;
                req.SetGyroscope("rad/s", x, y, z);
                return this;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}