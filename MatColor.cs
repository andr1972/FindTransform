using System;

namespace FindTransform
{
    class MatColor
    {
        public int w, h;
        public byte[] bytes;
        public MatColor(int w, int h)
        {
            this.w = w;
            this.h = h;
            bytes = new byte[4 * w * h];
        }

        public void ugetbilinear(double x, double y, out byte valR, out byte valG, out byte valB)
        {
            if (x >= 0 && x < w - 1 && y >= 0 && y < h - 1)
            {
                int intpart_x = (int)x;
                int intpart_y = (int)y;
                double fracpart_x = x - intpart_x;
                double fracpart_y = y - intpart_y;

                double p0 = (1 - fracpart_x) * (1 - fracpart_y);
                double p1 = fracpart_x * (1 - fracpart_y);
                double p2 = (1 - fracpart_x) * fracpart_y;
                double p3 = fracpart_x * fracpart_y;

                int pos0 = (intpart_y * w + intpart_x) * 4;         
                int pos1 = pos0 + 4;
                int pos2 = pos0 + w * 4;
                int pos3 = pos0 + (w + 1) * 4;

                valR = (byte)Math.Round(p0 * bytes[pos0] + p1 * bytes[pos1] + p2 * bytes[pos2] + p3 * bytes[pos3]);
                valG = (byte)Math.Round(p0 * bytes[pos0 + 1] + p1 * bytes[pos1 + 1] + p2 * bytes[pos2 + 1] + p3 * bytes[pos3 + 1]);
                valB = (byte)Math.Round(p0 * bytes[pos0 + 2] + p1 * bytes[pos1 + 2] + p2 * bytes[pos2 + 2] + p3 * bytes[pos3 + 2]);
            }
            else
            {
                valR = valG = valB = 255;
            }
        }

        public void uset(int x, int y, byte valR, byte valG, byte valB)
        {
            bytes[(y * w + x) * 4] = valR;
            bytes[(y * w + x) * 4 + 1] = valG;
            bytes[(y * w + x) * 4 + 2] = valB;
            bytes[(y * w + x) * 4 + 3] = 0xFF;//Alpha
        }
    }
}
