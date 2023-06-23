using System;
﻿namespace SquareEquationLib;
public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        if (a==0 || new[] { a, b, c }.Any(double.IsNaN) || new[] { a, b, c }.Any(double.IsInfinity))
        {
            throw new ArgumentException("Ошибка");
        }
        double x1;
        double x2;  
        double[] array = new double[2];
        double d = b * b - 4 * a * c;
        if (d < 0)
        {
            array = new double[0];
        }
        if (Math.Abs(d)<double.Epsilon)
        {
             x1 =-b/2*a;
             array = new double[] { x1 };
        }
        if (d>0)
        {
            x1 = -(b+Math.Sign(b)*Math.Sqrt(d))/2;
            x2 = c / x1;
            array = new double[] { x1, x2 };
        }
        return array;
    }
}
