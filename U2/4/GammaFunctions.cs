
using System;

public class GammaFunctions
{
    public static double GammaLower(double a, double x)
    {
        // Incomplete Gamma 'P' (lower) aka 'igam'
        if (x < 0.0 || a <= 0.0)
            throw new Exception("Bad args in GammaLower");
        if (x < a + 1)
            return GammaLowerSer(a, x); // No surprise
        else
            return 1.0 - GammaUpperCont(a, x); // Indirectly is faster
    }

    public static double GammaUpper(double a, double x)
    {
        // Incomplete Gamma 'Q' (upper)
        if (x < 0.0 || a <= 0.0)
            throw new Exception("Bad args in GammaUpper");
        if (x < a + 1)
            return 1.0 - GammaLowerSer(a, x); // Indirect is faster
        else
            return GammaUpperCont(a, x);
    }
    public static double LogGamma(double x)
    {
        double[] coef = new double[6] { 76.18009172947146, -86.50532032941677,
                                        24.01409824083091, -1.231739572450155,
                                        0.1208650973866179E-2, -0.5395239384953E-5 };

        double LogSqrtTwoPi = 0.91893853320467274178;
        double denom = x + 1;
        double y = x + 5.5;
        double series = 1.000000000190015;
        for (int i = 0; i < 6; ++i)
        {
            series += coef[i] / denom;
            denom += 1.0;
        }
        return (LogSqrtTwoPi + (x + 0.5) * Math.Log(y) -
        y + Math.Log(series / x));
    }
    private static double GammaLowerSer(double a, double x)
    {
        // Incomplete GammaLower (computed by series expansion)
        if (x < 0.0)
            throw new Exception("x param less than 0.0 in GammaLowerSer");
        double gln = LogGamma(a);
        double ap = a;
        double del = 1.0 / a;
        double sum = del;
        for (int n = 1; n <= 100; ++n)
        {
            ++ap;
            del *= x / ap;
            sum += del;
            if (Math.Abs(del) < Math.Abs(sum) * 3.0E-7) // Close enough?
                return sum * Math.Exp(-x + a * Math.Log(x) - gln);
        }
        throw new Exception("Unable to compute GammaLowerSer " +
        "to desired accuracy");
    }
    private static double GammaUpperCont(double a, double x)
    {
        // Incomplete GammaUpper computed by continuing fraction
        if (x < 0.0)
            throw new Exception("x param less than 0.0 in GammaUpperCont");
        double gln = LogGamma(a);
        double b = x + 1.0 - a;
        double c = 1.0 / 1.0E-30; // Div by close to double.MinValue
        double d = 1.0 / b;
        double h = d;
        for (int i = 1; i <= 100; ++i)
        {
            double an = -i * (i - a);
            b += 2.0;
            d = an * d + b;
            if (Math.Abs(d) < 1.0E-30) d = 1.0E-30; // Got too small?
            c = b + an / c;
            if (Math.Abs(c) < 1.0E-30) c = 1.0E-30;
            d = 1.0 / d;
            double del = d * c;
            h *= del;
            if (Math.Abs(del - 1.0) < 3.0E-7)
                return Math.Exp(-x + a * Math.Log(x) - gln) * h;  // Close enough?
        }
        throw new Exception("Unable to compute GammaUpperCont " +
        "to desired accuracy");
    }
}