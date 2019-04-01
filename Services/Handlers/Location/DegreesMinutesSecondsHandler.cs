﻿using System;
using System.Text.RegularExpressions;

namespace Services.Handlers.Location
{
    public class DegreesMinutesSecondsHandler : LocationHandler
    {
        public override object Handle(object request)
        {
            var unit = "dms";
            var req = request as DBConnection.Entities.Sensors.Location;
            if (req.LocationUnit == unit)
            {
                var latitude = Convert(req.Latitude);
                var longitude = Convert(req.Longitude);
                req.SetLocation("dd", latitude, longitude);
                return this;
            }
            else
            {
                return base.Handle(request);
            }
        }

        private string Convert(string point)
        {
            //Example: 17.21.18S
            var multiplier = (point.Contains("S") || point.Contains("W")) ? -1 : 1; //handle south and west

            point = Regex.Replace(point, "[^0-9.]", ""); //remove the characters

            var pointArray = point.Split('.'); //split the string.

            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            var degrees = Double.Parse(pointArray[0]);
            var minutes = Double.Parse(pointArray[1]) / 60;
            var seconds = Double.Parse(pointArray[2]) / 3600;

            return ((degrees + minutes + seconds) * multiplier).ToString();
        }
    }
}