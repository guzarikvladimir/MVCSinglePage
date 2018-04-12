using System;

namespace Core.Contract.Models
{
    public class ExifView
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public DateTime? DataAndTime { get; set; }

        public string Compression { get; set; }

        public TimeSpan ExposureTime { get; set; }

        public double? ExifVersion { get; set; }
    }
}