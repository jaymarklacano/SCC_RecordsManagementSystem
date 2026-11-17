using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SCCRecordsManagementSystem
{
    // Converts pictures between the formats used by Windows Forms (Bitmap),
    // OpenCV (Mat) and the database (byte[]).
    public static class ImageHelper
    {
        // Bitmap -> Mat (OpenCV uses the B,G,R colour order, the same as a 24-bit Bitmap).
        public static Mat BitmapToMat(Bitmap bitmap)
        {
            Rectangle area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(area, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            try
            {
                Mat mat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC3);
                int rowBytes = bitmap.Width * 3;
                int matStep = (int)mat.Step();
                byte[] row = new byte[rowBytes];

                // Copy row by row because a Bitmap can have extra padding bytes at the end of each row.
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Marshal.Copy(IntPtr.Add(data.Scan0, y * data.Stride), row, 0, rowBytes);
                    Marshal.Copy(row, 0, IntPtr.Add(mat.Data, y * matStep), rowBytes);
                }
                return mat;
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        // Mat (3 colour channels) -> Bitmap.
        public static Bitmap MatToBitmap(Mat mat)
        {
            Bitmap bitmap = new Bitmap(mat.Width, mat.Height, PixelFormat.Format24bppRgb);
            Rectangle area = new Rectangle(0, 0, mat.Width, mat.Height);
            BitmapData data = bitmap.LockBits(area, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            try
            {
                int rowBytes = mat.Width * 3;
                int matStep = (int)mat.Step();
                byte[] row = new byte[rowBytes];

                for (int y = 0; y < mat.Height; y++)
                {
                    Marshal.Copy(IntPtr.Add(mat.Data, y * matStep), row, 0, rowBytes);
                    Marshal.Copy(row, 0, IntPtr.Add(data.Scan0, y * data.Stride), rowBytes);
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        // Bitmap -> JPEG bytes (this is what we save in the database).
        public static byte[] ToJpegBytes(Bitmap bitmap)
        {
            ImageCodecInfo jpegCodec = null;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    jpegCodec = codec;
                }
            }

            using (MemoryStream stream = new MemoryStream())
            using (EncoderParameters parameters = new EncoderParameters(1))
            {
                parameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                bitmap.Save(stream, jpegCodec, parameters);
                return stream.ToArray();
            }
        }

        // JPEG bytes from the database -> Bitmap (a copy, so the stream can be closed safely).
        public static Bitmap BytesToBitmap(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            using (Image image = Image.FromStream(stream))
            {
                return new Bitmap(image);
            }
        }
    }
}
