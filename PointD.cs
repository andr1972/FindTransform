using System;

namespace FindTransform
{
    class PointD
    {
        public double x;
        public double y;
        public PointD(double x, double y) { this.x = x; this.y = y; }
        public override string ToString() { return "(" + x.ToString() + " " + y.ToString() + ")"; }

        static internal PointD[] fromDouble(double[] init)
        {
            int n = init.Length / 2;
            PointD[] P = new PointD[n];
            for (int i = 0; i < n; i++)
            {
                P[i] = new PointD(init[2 * i], init[2 * i + 1]);
            }
            return P;
        }
    }
}
