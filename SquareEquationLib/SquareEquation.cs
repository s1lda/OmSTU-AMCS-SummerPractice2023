using System;
﻿namespace SquareEquationLib;
public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        if (a==0 || new[] { a, b, c }.Any(double.IsNaN) || new[] { a, b, c }.Any(double.IsInfinity))
        {
            throw new  System.ArgumentException();
        }
        double x1;
        double x2;
        double[] array1 = {};
        double[] array2 = new double[1];
        double[] array3 = new double[2];
        double d = b * b - 4*a* c;
        if (d <= -double.Epsilon)
        {
            return array1;
        }
        if (-double.Epsilon < d && d < double.Epsilon)
        {
             array2[0]=-b/2*a;
             return array2;
        }
        if (d>0)
        {
            array3[0] = -(b+Math.Sign(b)*Math.Sqrt(d))/2;
            array3[1] = c / array3[0];
            return array3;
        }
    }
}
