using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApp.Util
{
    public static class ImageTransformation
    {
        public static byte[] Transform(HttpPostedFileBase file)
        {
            Image img = Image.FromStream(file.InputStream);

            int imageWidth = img.Width;
            int imageHeight = img.Height;

            ChangeWidthAndHeight(ref imageWidth, ref imageHeight);

            img = ResizeImage(img, imageWidth, imageHeight);

            return ToByteStream(img);
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public static void ChangeWidthAndHeight(ref int width,ref int height)
        {
            while (true)
            {
                if (width > 350 || height > 400)
                {
                    width -= width / 15;
                    height -= height / 15;
                }
                else
                {
                    break;
                }
            }
        }
        public static byte[] ToByteStream(Image image)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
            return xByte;
        }
    }
}