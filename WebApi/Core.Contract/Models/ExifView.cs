using System;

namespace Core.Contract.Models
{
    public class ExifView
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public DateTime? DateAndTime { get; set; }

        public string Compression { get; set; }

        public double? ExposureTime { get; set; }

        public double? ExifVersion { get; set; }
    }
}