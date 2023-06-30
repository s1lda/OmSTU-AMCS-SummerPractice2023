namespace SquareEquationLib;
public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double epsilon=1e-6;
        if (-epsilon < a & a < epsilon)
        {
            throw new System.ArgumentException("Invalid Values");

        }
        if ( new[] { a, b, c }.Any(double.IsNaN) || new[] { a, b, c }.Any(double.IsInfinity))
        {
            throw new System.ArgumentException("Invalid Values");

        }
        double d=b * b - 4 *a*c;
        double[] array1 = new double[] { };
        double[] array3 = new double[2];
        if (d <= -epsilon) return array1;
        else if (-epsilon < d & d < epsilon)
        { 
            double[] array2 = new double[1];
            array2[0] = -(b) / 2*a;
            return array2;
        }
       else if (b < epsilon)
        {
            double x1 = -((b + Math.Sqrt(d)) / 2);
            double x2 = c / x1;
            array3[0] = x1;
            array3[1] = x2;
            return array3;
        }
        else
        {
            double x1 = -((b + Math.Sign(b) * Math.Sqrt(d)) / 2);
            double x2 = c / x1;
            array3[0] = x1;
            array3[1] = x2;
            return array3;
        }
    }
}
