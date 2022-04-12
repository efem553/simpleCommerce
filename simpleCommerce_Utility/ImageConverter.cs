using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace simpleCommerce_Utility
{
    public static class ImageConverter
    {

        public static Image ConvertBase64StringToImage(string imageBase64String)
        {
            var imageBytes = Convert.FromBase64String(imageBase64String);
            var imageStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            imageStream.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(imageStream, true);
            return image;
        }
        public static string ConvertImageToBase64String(Image image)
        {
            var imageStream = new MemoryStream();
            image.Save(imageStream, ImageFormat.Png);
            imageStream.Position = 0;
            var imageBytes = imageStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }


    }
}
