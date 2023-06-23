using System;
﻿namespace SquareEquationLib;
public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        if (-double.Epsilon<a && a<double.Epsilon)
        {
            throw new System.ArgumentException();
        }
        if ( new[] { a, b, c }.Any(double.IsNaN) || new[] { a, b, c }.Any(double.IsInfinity))
        {
            throw new  System.ArgumentException();
        }
        double d=b * b - 4 * a * c;
        double[] array1 = new double[] { };
        if (d <= -eps) return array1;
        else if (-eps < d && d < eps)
        { 
            double[] array2 = new double[1];
            array2[0] = -(b) / 2*a;
            return array2;
        }
        else
        {
            double[] array3 = new double[2];
            array3[0] = -(b + Math.Sign(b) * Math.Sqrt(d)) / 2;
            array3[1] = c / array3[0];
            return array3;
        }
    }
}
