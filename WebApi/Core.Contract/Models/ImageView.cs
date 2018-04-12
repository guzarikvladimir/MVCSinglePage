using System;

namespace Core.Contract.Models
{
    public class ImageView
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }


        public string Url { get; set; }

        public ExifView Exif { get; set; }

        public ImageView()
        {
        }

        public ImageView(string name)
        {
            Name = name;
        }
    }
}