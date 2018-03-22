using System;
namespace FindTransform
{
    class Fun5: IFun
    {
        PointD[] A;
        PointD[] B;

        private double[] w = new double[] { 1, 1, 0.01, 10000, 10000, 1, 1, 0.01, 10000, 10000 };
        internal Fun5(PointD[] A, PointD[] B)
        {
            this.A = A;
            this.B = B;            
        }

        public double F(int index, Vector x)
        {
            int i = index / 2;
            if (index % 2 == 0)
                return (x[0] * A[i].x + x[1] * A[i].y + x[2]) / (1 + x[3] * A[i].x + x[4] * A[i].y) - B[i].x;
            else
                return (x[5] * A[i].x + x[6] * A[i].y + x[7]) / (1 + x[8] * A[i].x + x[9] * A[i].y) - B[i].y;
        }

        public double df(int index, int derivative, Vector x)
        {
            int i = index / 2;
            if (index % 2 == 0)
            {
                switch (derivative)
                {
                    case 0: return A[i].x / (1 + A[i].x * x[3] + A[i].y * x[4]);
                    case 1: return A[i].y / (1 + A[i].x * x[3] + A[i].y * x[4]);
                    case 2: return 1 / (1 + A[i].x * x[3] + A[i].y * x[4]);
                    case 3:
                        {
                            double d = 1 + A[i].x * x[3] + A[i].y * x[4];
                            return -A[i].x * (A[i].x * x[0] + A[i].y * x[1] + x[2]) / (d * d);
                        }
                    case 4:
                        {
                            double d = 1 + A[i].x * x[3] + A[i].y * x[4];
                            return -A[i].y * (A[i].x * x[0] + A[i].y * x[1] + x[2]) / (d * d);
                        }
                    default: return 0;
                }
            }
            else
            {
                switch (derivative)
                {
                    case 5: return A[i].x / (1 + A[i].x * x[8] + A[i].y * x[9]);
                    case 6: return A[i].y / (1 + A[i].x * x[8] + A[i].y * x[9]);
                    case 7: return 1 / (1 + A[i].x * x[8] + A[i].y * x[9]);
                    case 8:
                        {
                            double d = 1 + A[i].x * x[8] + A[i].y * x[9];
                            return -A[i].x * (A[i].x * x[5] + A[i].y * x[6] + x[7]) / (d * d);
                        }
                    case 9:
                        {
                            double d = 1 + A[i].x * x[8] + A[i].y * x[9];
                            return -A[i].y * (A[i].x * x[5] + A[i].y * x[6] + x[7]) / (d * d);
                        }
                    default: return 0;
                }
            }
        }
        public double[] weights() { return w; }
    }
}
