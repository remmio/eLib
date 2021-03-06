﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace eLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImagesHelper
    {
        #region Image To Bytes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageControl"></param>
        /// <returns></returns>
        public static byte[] FromImageSource (this BitmapImage imageControl )
        {
            try
            {
                var memStream = new MemoryStream();
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageControl));
                encoder.Save(memStream);
                return memStream.GetBuffer();
            }
            catch (Exception) { return null;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] BitmapToByte2 ( BitmapImage bitmapImage )
        {
            byte[] data;
            var encoder = new JpegBitmapEncoder ();
            encoder.Frames.Add (BitmapFrame.Create (bitmapImage));
            using(var ms = new MemoryStream ())
            {
                encoder.Save (ms);
                data = ms.ToArray ();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToByte ( BitmapImage bitmap )
        {
            var stream = bitmap.StreamSource;
            byte[] buffer;
            if (stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((int)stream.Length);
            }
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static byte[] BitmapToByte ( string imagePath )
        {
            var fs = new FileStream (imagePath, FileMode.Open, FileAccess.Read);
            var imgBytes = new byte[fs.Length];
            fs.Read (imgBytes, 0, Convert.ToInt32 (fs.Length));
            var encodeData = Convert.ToBase64String (imgBytes, Base64FormattingOptions.InsertLineBreaks);
            return new[] { byte.Parse (encodeData) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns></returns>
        public static byte[] BitmapArrayFromFile ( string imageFilePath )
        {
            if(!File.Exists (imageFilePath)) return null;

            var fs = new FileStream (imageFilePath, FileMode.Open, FileAccess.Read);
            var imgByteArr = new byte[fs.Length];
            fs.Read (imgByteArr, 0, Convert.ToInt32 (fs.Length));
            fs.Close ();
            return imgByteArr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this BitmapImage bitmap )
        {
            var stream = bitmap.StreamSource;
            byte[] buffer;
            if(stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((int)stream.Length);
            }
            return buffer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public  static byte[] AsByteArray(this Image imageIn)
        {
            try
            {
                var ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
               return new byte[0] ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }


        #endregion




        #region Bytes To Image

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteVal"></param>
        /// <returns></returns>
        public static BitmapImage AsPhoto (this byte[] byteVal )
        {
            if(byteVal == null) return null;

            try
            {
                var strmImg = new MemoryStream (byteVal);
                var myBitmapImage = new BitmapImage ();
                myBitmapImage.BeginInit ();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit ();
                return myBitmapImage;
            }
            catch(Exception ex)
            {
                ex.Log();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BitmapImage FromFile ( string path )
        {
            var img = new BitmapImage ();
            img.BeginInit ();
            img.UriSource = new Uri (path, UriKind.RelativeOrAbsolute);
            img.EndInit ();
            return img;
        }


        #endregion




        #region LOT OF THINGS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp1"></param>
        /// <param name="bmp2"></param>
        /// <returns></returns>
        public static CompareResult Compare(Bitmap bmp1, Bitmap bmp2)
        {
            var cr = CompareResult.CiCompareOk;

            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size) cr = CompareResult.CiSizeMismatch;

            else
            {
                //Sizes are the same so start comparing pixels
                for (var x = 0; x < bmp1.Width && cr == CompareResult.CiCompareOk; x++)
                    for (var y = 0; y < bmp1.Height && cr == CompareResult.CiCompareOk; y++)
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y)) cr = CompareResult.CiPixelMismatch;
            }
            return cr;
        }


        /// <summary>
        /// Open Dialog for Image File
        /// </summary>
        public static string OpenImageFileDialog()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files (*.png, *.jpg, *.jpeg, *.jpe, *.jfif, *.gif, *.bmp, *.tif, *.tiff)|*.png;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.bmp;*.tif;*.tiff|" +
                    "PNG (*.png)|*.png|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|TIFF (*.tif, *.tiff)|*.tif;*.tiff|" +
                    "All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK) return ofd.FileName;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static ImageFormat GetImageFormat(string filePath)
        {
            ImageFormat imageFormat = ImageFormat.Png;
            var ext = FilesHelper.FilesHelper.GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                switch (ext)
                {
                default:
                    // ReSharper disable once RedundantCaseLabel
                case "png":
                    imageFormat = ImageFormat.Png;
                    break;
                case "jpg":
                case "jpeg":
                case "jpe":
                case "jfif":
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case "gif":
                    imageFormat = ImageFormat.Gif;
                    break;
                case "bmp":
                    imageFormat = ImageFormat.Bmp;
                    break;
                case "tif":
                case "tiff":
                    imageFormat = ImageFormat.Tiff;
                    break;
                }
            }

            return imageFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SaveImage(Image img, string filePath)
        {
            img.Save(filePath, GetImageFormat(filePath));
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SaveImageFileDialog(Image img, string filePath = default(string))
        {
            using (var sfd = new SaveFileDialog())
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var folder = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(folder))
                    {
                        sfd.InitialDirectory = folder;
                    }
                    sfd.FileName = Path.GetFileNameWithoutExtension(filePath);
                }

                sfd.DefaultExt = ".png";
                sfd.Filter = "PNG (*.png)|*.png|JPEG (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|TIFF (*.tif, *.tiff)|*.tif;*.tiff";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveImage(img, sfd.FileName);
                    return sfd.FileName;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImage(Image img, Size size) => ResizeImage(img, size.Width, size.Height);

        /// <summary>
        /// 
        /// </summary>
        public static Image LoadImage(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && FilesHelper.FilesHelper.IsImageFile(filePath) && File.Exists(filePath))
                {
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
                }
            }
            catch (Exception e)
            {
                e.Log();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImage(Image img, int width, int height)
        {
            if (width < 1 || height < 1 || (img.Width == width && img.Height == height))
            {
                return img;
            }

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (img)
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.CompositingMode = CompositingMode.SourceOver;

                using (var ia = new ImageAttributes())
                {
                    ia.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(img, new Rectangle(0, 0, width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                }
            }

            return bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImageByPercentage(Image img, float percentage) => ResizeImageByPercentage(img, percentage, percentage);

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImageByPercentage(Image img, float percentageWidth, float percentageHeight)
        {
            var width = (int)(percentageWidth / 100 * img.Width);
            var height = (int)(percentageHeight / 100 * img.Height);
            return ResizeImage(img, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImage(Image img, Size size, bool allowEnlarge, bool centerImage = true) => ResizeImage(img, size.Width, size.Height, allowEnlarge, centerImage);

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImage(Image img, int width, int height, bool allowEnlarge, bool centerImage = true) => ResizeImage(img, width, height, allowEnlarge, centerImage, Color.Transparent);

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImage(Image img, int width, int height, bool allowEnlarge, bool centerImage, Color backColor)
        {
            double ratio;
            int newWidth, newHeight;

            if (!allowEnlarge && img.Width <= width && img.Height <= height)
            {
                ratio = 1.0;
                newWidth = img.Width;
                newHeight = img.Height;
            }
            else
            {
                var ratioX = (double)width / img.Width;
                var ratioY = (double)height / img.Height;
                ratio = ratioX < ratioY ? ratioX : ratioY;
                newWidth = (int)(img.Width * ratio);
                newHeight = (int)(img.Height * ratio);
            }

            var newX = 0;
            var newY = 0;

            if (centerImage)
            {
                newX += (int)((width - img.Width * ratio) / 2);
                newY += (int)((height - img.Height * ratio) / 2);
            }

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            using (img)
            {
                g.Clear(backColor);
                g.DrawImage(img, newX, newY, newWidth, newHeight);
            }

            return bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image ResizeImageLimit(Image img, Size size) => ResizeImageLimit(img, size.Width, size.Height);

        /// <summary>If image size bigger than "size" then resize it and keep aspect ratio else return image.</summary>
        public static Image ResizeImageLimit(Image img, int width, int height)
        {
            if (img.Width <= width && img.Height <= height)
            {
                return img;
            }

            var ratioX = (double)width / img.Width;
            var ratioY = (double)height / img.Height;
            var ratio = ratioX < ratioY ? ratioX : ratioY;
            var newWidth = (int)(img.Width * ratio);
            var newHeight = (int)(img.Height * ratio);

            return ResizeImage(img, newWidth, newHeight);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image CropImage(Image img, Rectangle rect)
        {
            if (img != null && rect.X >= 0 && rect.Y >= 0 && rect.Width > 0 && rect.Height > 0 &&
                new Rectangle(0, 0, img.Width, img.Height).Contains(rect))
            {
                using (var bmp = new Bitmap(img))
                {
                    return bmp.Clone(rect, bmp.PixelFormat);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Bitmap CropBitmap(Bitmap bmp, Rectangle rect)
        {
            if (bmp != null && rect.X >= 0 && rect.Y >= 0 && rect.Width > 0 && rect.Height > 0 &&
                new Rectangle(0, 0, bmp.Width, bmp.Height).Contains(rect))
            {
                return bmp.Clone(rect, bmp.PixelFormat);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Image CropImage(Image img, Rectangle rect, GraphicsPath gp)
        {
            if (img != null && rect.Width > 0 && rect.Height > 0 && gp != null)
            {
                var bmp = new Bitmap(rect.Width, rect.Height);
                bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (var region = new Region(gp))
                    {
                        g.Clip = region;
                        g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
                    }
                }

                return bmp;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Bitmap RotateImage(Image inputImage, float angleDegrees, bool upsize, bool clip)
        {
            // Test for zero rotation and return a clone of the input image
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (angleDegrees == 0f)
                return (Bitmap)inputImage.Clone();

            // Set up old and new image dimensions, assuming upsizing not wanted and clipping OK
            var oldWidth = inputImage.Width;
            var oldHeight = inputImage.Height;
            var newWidth = oldWidth;
            var newHeight = oldHeight;
            var scaleFactor = 1f;

            // If upsizing wanted or clipping not OK calculate the size of the resulting bitmap
            if (upsize || !clip)
            {
                var angleRadians = angleDegrees * Math.PI / 180d;

                var cos = Math.Abs(Math.Cos(angleRadians));
                var sin = Math.Abs(Math.Sin(angleRadians));
                newWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
                newHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
            }

            // If upsizing not wanted and clipping not OK need a scaling factor
            if (!upsize && !clip)
            {
                scaleFactor = Math.Min((float)oldWidth / newWidth, (float)oldHeight / newHeight);
                newWidth = oldWidth;
                newHeight = oldHeight;
            }

            // Create the new bitmap object.
            var newBitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
            newBitmap.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            // Create the Graphics object that does the work
            using (Graphics graphicsObject = Graphics.FromImage(newBitmap))
            {
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                // Set up the built-in transformation matrix to do the rotation and maybe scaling
                graphicsObject.TranslateTransform(newWidth / 2f, newHeight / 2f);

                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (scaleFactor != 1f)
                    graphicsObject.ScaleTransform(scaleFactor, scaleFactor);

                graphicsObject.RotateTransform(angleDegrees);
                graphicsObject.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

                // Draw the result
                graphicsObject.DrawImage(inputImage, 0, 0, inputImage.Width, inputImage.Height);
            }

            return newBitmap;
        }




        #endregion







    }

    /// <summary>
    /// 
    /// </summary>
    public enum CompareResult
    {
        /// <summary>
        /// 
        /// </summary>
        CiCompareOk,

        /// <summary>
        /// 
        /// </summary>
        CiPixelMismatch,

        /// <summary>
        /// 
        /// </summary>
        CiSizeMismatch
    }
}
