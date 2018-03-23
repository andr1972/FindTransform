using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace FindTransform
{
    class BitmapWriter
    {
        private static void Transformation(MatColor im, MatColor output)
        {
            for (int y = 0; y < output.h; ++y)
                for (int x = 0; x < output.w; ++x)
                {
                    PointD p1;
                    p1.x = (0.995908734520867 * x - 0.132430377748651 * y - 10.560320141038) /
                        (1 + 1.17786537813519E-05 * x - 0.000277557920657936 * y);
                    p1.y = (-0.414732683122513 * x + 0.487661277134173 * y + 59.8242757768033 +
                        0.000417226091721666 * x * x + 1.25892047681815E-05 * x * y) /
                        (1 + 1.17786537813519E-05 * x - 0.000277557920657936 * y);

                    byte valR, valG, valB;
                    im.ugetbilinear(p1.x, p1.y, out valR, out valG, out valB);
                    output.uset(x, y, valR, valG, valB);
                }
        }

        private static Bitmap LoadBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static void Do()
        {
            string path = @"..\..\perspective.jpg";
            Bitmap bmp0 = LoadBitmap(path);
            Bitmap bmp1 = new Bitmap(bmp0.Width, (int)(bmp0.Height * 1.5));
            Rectangle rect = new Rectangle(0, 0, bmp0.Width, bmp0.Height);
            BitmapData data = bmp0.LockBits(rect, ImageLockMode.ReadWrite, bmp0.PixelFormat);
            IntPtr ptr = data.Scan0;
            int w = bmp0.Width;
            int h = bmp0.Height;
            int numBytes = data.Stride * bmp0.Height;
            MatColor im = new MatColor(w, h);
            Marshal.Copy(ptr, im.bytes, 0, numBytes);
            bmp0.UnlockBits(data);
            MatColor output = new MatColor(bmp1.Width, bmp1.Height);
            Transformation(im, output);
            rect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
            data = bmp1.LockBits(rect, ImageLockMode.ReadWrite, bmp1.PixelFormat);
            ptr = data.Scan0;
            numBytes = data.Stride * bmp1.Height;
            Marshal.Copy(output.bytes, 0, ptr, numBytes);
            bmp1.UnlockBits(data);

            ImageCodecInfo myImageCodecInfo;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            Encoder myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 87L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"..\..\output.jpg", myImageCodecInfo, myEncoderParameters);
        }
    }
}
