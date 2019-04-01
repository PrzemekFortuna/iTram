namespace Services.Handlers.Location
{
    public class DecimalDegreesHandler : LocationHandler
    {
        public override object Handle(object request)
        {
            var unit = "dd";
            var req = request as DBConnection.Entities.Sensors.Location;
            if (req.LocationUnit == unit)
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
