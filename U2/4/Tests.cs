using System;
using System.Collections;

public class Tests
{
    public static double FrequencyTest(BitArray bitArray)
    {
        double sum = 0;
        for (int i = 0; i < bitArray.Length; ++i)
        {
            if (bitArray[i] == false) sum = sum - 1;
            else sum = sum + 1;
        }
        double testStat = Math.Abs(sum) / Math.Sqrt(bitArray.Length);
        double rootTwo = 1.414213562373095;
        double pValue = ErrorFunctionComplement(testStat / rootTwo);
        return pValue;
    }

    public static double BlockTest(BitArray bitArray, int blockLength)
    {
        int numBlocks = bitArray.Length / blockLength; // 'N'
        double[] proportions = new double[numBlocks];
        int k = 0; // ptr into bitArray
        for (int block = 0; block < numBlocks; ++block)
        {
            int countOnes = 0;
            for (int i = 0; i < blockLength; ++i)
            {
                if (bitArray[k++] == true)
                    ++countOnes;
            }
            proportions[block] = (countOnes * 1.0) / blockLength;
        }
        double summ = 0.0;
        for (int block = 0; block < numBlocks; ++block)
            summ = summ + (proportions[block] - 0.5) *
            (proportions[block] - 0.5);
        double chiSquared = 4 * blockLength * summ; // magic
        double a = numBlocks / 2.0;
        double x = chiSquared / 2.0;
        double pValue = GammaFunctions.GammaUpper(a, x);
        return pValue;
    }

    public static double RunsTest(BitArray bitArray)
    {
        double numOnes = 0.0;
        for (int i = 0; i < bitArray.Length; ++i)
            if (bitArray[i] == true)
                ++numOnes;
        double prop = (numOnes * 1.0) / bitArray.Length;
        // Double tau = 2.0 / Math.Sqrt(bitArray.Length * 1.0);
        // If (Math.Abs(prop - 0.5) >= tau)
        // Return 0.0; // Not-random short-circuit
        int runs = 1;
        for (int i = 0; i < bitArray.Length - 1; ++i)
            if (bitArray[i] != bitArray[i + 1])
                ++runs;
        double num = Math.Abs(
        runs - (2 * bitArray.Length * prop * (1 - prop)));
        double denom = 2 * Math.Sqrt(2.0 * bitArray.Length) *
        prop * (1 - prop);
        double pValue = ErrorFunctionComplement(num / denom);
        return pValue;
    }

    static double ErrorFunctionComplement(double x)
    {
        return 1 - ErrorFunction(x);
    }

    static double ErrorFunction(double x)
    {
        double p = 0.3275911;
        double a1 = 0.254829592;
        double a2 = -0.284496736;
        double a3 = 1.421413741;
        double a4 = -1.453152027;
        double a5 = 1.061405429;
        double t = 1.0 / (1.0 + p * x);
        double err = 1.0 - (((((a5 * t + a4) * t) +
        a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);
        return err;
    }
}