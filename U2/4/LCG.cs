using System;

public class LCG {
    public int a, c, m, x0;

    public LCG(int a, int c, int m, int x0) {
        this.a = a;
        this.c = c;
        this.m = m;
        this.x0 = x0;
    }

    public int[] RandomSeries(int length) {
        int[] result = new int[length];
        result[0] = x0;

        for(int count = 1; count < length; count++) {
            result[count] = (a * result[count - 1] + c) % m;
        }

        return result;
    }
}