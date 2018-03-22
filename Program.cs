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

        static void Main(string[] args)
        {
            testFun5();
            testFun4();
        }
    }
}
