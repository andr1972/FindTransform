using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace FindTransform
{
    class BitmapWriter
    {
        private static void Transformation(MatColor im, MatColor output, Vector X)
        {
            for (int y = 0; y < output.h; ++y)
                for (int x = 0; x < output.w; ++x)
                {
                    PointD p1;
                    p1.x = (X[0] * x + X[1]* y + X[2]) /
                        (1 + X[3]* x + X[4] * y);
                    p1.y = (X[5] * x + X[6] * y + X[7] +
                        X[8] * x * x + X[9] * x * y) /
                        (1 + X[3] * x + X[4] * y);

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

        public static void Do(Vector X)
        {
            string path = "../../perspective.jpg";
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
            Transformation(im, output, X);
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
            bmp1.Save("../../output.jpg", myImageCodecInfo, myEncoderParameters);
        }
    }
}
