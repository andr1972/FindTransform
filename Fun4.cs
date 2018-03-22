using System;

namespace FindTransform
{
    class Fun4: IFun
    {
        PointD[] A;
        PointD[] B;

        private double[] w = new double[] { 1, 1, 0.01, 10000, 10000, 1, 1, 0.01, 10000, 10000 };
        internal Fun4(PointD[] A, PointD[] B)
        {
            this.A = A;
            this.B = B;
        }
        public double F(int index, Vector x)
        {
            if (index < 8)
            {
                int i = index / 2;
                if (index % 2 == 0)
                    return (x[0] * A[i].x + x[1] * A[i].y + x[2]) / (1 + x[3] * A[i].x + x[4] * A[i].y) - B[i].x;
                else
                    return (x[5] * A[i].x + x[6] * A[i].y + x[7]) / (1 + x[8] * A[i].x + x[9] * A[i].y) - B[i].y;
            }
            else
            {
                int i1, i2;
                if (index == 8)
                {
                    i1 = 0;
                    i2 = 1;
                }
                else
                {
                    i1 = 3;
                    i2 = 2;
                }
                double Ax12 = A[i1].x + A[i2].x;
                double Ay12 = A[i1].y + A[i2].y;
                return (-B[i1].x + (x[0] * Ax12 + Ay12 * x[1] + 2 * x[2]) / (2 + Ax12 * x[3] + Ay12 * x[4]))
                / (-B[i1].x + B[i2].x) - (-B[i1].y + (Ax12 * x[5] + Ay12 * x[6] +2 * x[7]) / (2 + Ax12 * x[8] + Ay12 * x[9])) / (-B[i1].y + B[i2].y);              
            }
        }

        public double df(int index, int derivative, Vector x)
        {
            if (index < 8)
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
            else
            {
                int i1, i2;
                if (index == 8)
                {
                    i1 = 0;
                    i2 = 1;
                }
                else
                {
                    i1 = 3;
                    i2 = 2;
                }
                double Ax12 = A[i1].x + A[i2].x;
                double Ay12 = A[i1].y + A[i2].y;
                switch (derivative)
                {
                    case 0: return Ax12 / ((-B[i1].x + B[i2].x) * (2 + Ax12 * x[3] + Ay12 * x[4]));
                    case 1: return Ay12 / ((-B[i1].x + B[i2].x) * (2 + Ax12 * x[3] + Ay12 * x[4]));
                    case 2: return 2 / ((-B[i1].x + B[i2].x) * (2 + Ax12 * x[3] + Ay12 * x[4]));
                    case 3:
                        {
                            double d = 2 + Ax12 * x[3] + Ay12 * x[4];
                            return -((Ax12 * (Ax12 * x[0] + Ay12 * x[1] + 2 * x[2])) / ((-B[i1].x + B[i2].x) * (d * d)));
                        }
                    case 4:
                        {
                            double d = 2 + Ax12 * x[3] + Ay12 * x[4];
                            return -((Ay12 * (Ax12 * x[0] + Ay12 * x[1] + 2 * x[2])) / ((-B[i1].x + B[i2].x) * (d * d)));
                        }
                    case 5:
                        return -(Ax12 / ((-B[i1].y + B[i2].y) * (2 + Ax12 * x[8] + Ay12 * x[9])));
                    case 6:
                        return -(Ay12 / ((-B[i1].y + B[i2].y) * (2 + Ax12 * x[8] + Ay12 * x[9])));
                    case 7:
                        return -(2 / ((-B[i1].y + B[i2].y) * (2 + Ax12 * x[8] + Ay12 * x[9])));
                    case 8:
                        {
                            double d = 2 + Ax12 * x[8] + Ay12 * x[9];
                            return (Ax12 * (Ax12 * x[5] + Ay12 * x[6] + 2 * x[7])) / ((-B[i1].y + B[i2].y) * (d * d));
                        }
                    case 9:
                        {
                            double d = 2 + Ax12 * x[8] + Ay12 * x[9];
                            return (Ay12 * (Ax12 * x[5] + Ay12 * x[6] + 2 * x[7])) / ((-B[i1].y + B[i2].y) * (d * d));
                        }
                    default: return 0;
                }
            }
        }
        public double[] weights() { return w; }
    }
}
