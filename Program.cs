using System;

namespace FindTransform
{
    class Program
    {
        static void testFun5()
        {
            PointD[] A = PointD.fromDouble(new double[] { 0, 0, 0, 200, 200, 200, 200, 0, 100, 100 });
            PointD[] B = PointD.fromDouble(new double[] { -50, -200, -9.61538461538462, 19.0476190476191,
                216.981132075472, 84.2105263157895,
                186.274509803922, -155.555555555556, 87.378640776699, -61.5384615384615 });
            Fun5 fun = new Fun5(A, B);
            Newton newton = new Newton();
            Vector start = new Vector(new double[] { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 });
            Vector X = newton.Do(10, fun, start);
            if (X == null)
                Console.WriteLine("bad");
            else
                X.print();
        }

        static void testFun4()
        {
            PointD[] A = PointD.fromDouble(new double[] { 0, 0, 0, 200, 200, 200, 200, 0, 100, 100 });
            PointD[] B = PointD.fromDouble(new double[] { -50, -200, -9.61538461538462, 19.0476190476191,
                216.981132075472, 84.2105263157895,
                186.274509803922, -155.555555555556, 87.378640776699, -61.5384615384615 });
            Fun4 fun = new Fun4(A, B);
            Newton newton = new Newton();
            Vector start = new Vector(new double[] { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 });
            Vector X = newton.Do(10, fun, start);
            if (X == null)
                Console.WriteLine("bad");
            else
                X.print();
        }

        static void testFun4simple()
        {
            PointD[] B = PointD.fromDouble(new double[] { 162, 153, 742, 141, 84, 628, 807, 615});
            PointD[] A = PointD.fromDouble(new double[] { 200, 300, 200 + 180 * 3, 300, 200, 300 + 219 * 3 ,
                200 + 180 * 3, 300 + 219 * 3 });
            Fun4simple fun = new Fun4simple(A, B);
            Newton newton = new Newton();
            Vector start = new Vector(new double[] { 1, 0, 0, 0, 0, 0, 1, 0 });
            Vector X = newton.Do(8, fun, start);
            if (X == null)
                Console.WriteLine("bad");
            else
                X.print();
        }

        static void testFun5square()
        {
            PointD[] B = PointD.fromDouble(new double[] { 162, 153, 742, 141, 84, 628, 807, 615, 453,114 });
            PointD[] A = PointD.fromDouble(new double[] { 200, 300, 200 + 180 * 3, 300, 200, 300 + 219 * 3 ,
                200 + 180 * 3, 300 + 219 * 3, 200 + 90 * 3, 300 });
            Fun5square fun = new Fun5square(A, B);
            Newton newton = new Newton();
            Vector start = new Vector(new double[] { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 });
            Vector X = newton.Do(10, fun, start);
            if (X == null)
                Console.WriteLine("bad");
            else
                X.print();
        }

        static void Main(string[] args)
        {
            //testFun5square();
            BitmapWriter.Do();
        }
    }
}
