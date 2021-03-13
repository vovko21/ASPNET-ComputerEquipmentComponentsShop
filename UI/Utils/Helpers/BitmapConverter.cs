using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace UI.Utils.Helpers
{
    public class BitmapConverter
    {
        public static Image Converter(Stream inputStream)
        {
           return Image.FromStream(inputStream);
        }
    }
}