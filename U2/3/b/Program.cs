using System;

namespace b
{
    class Program
    {
        static void Main(string[] args)
        {
            LCG lcg = new LCG(24, 42, 529, 42);
            LCG lcg2 = new LCG(24, 42, 524, 42);

            int[] series = lcg.RandomSeries(1000);
            int[] series2 = lcg2.RandomSeries(1000);

            Console.WriteLine("Series with modulo 529");

            int counter = 0;

            for(int i = 0; i < 1000; i++) {
                if(series[i] == 42){
                  Console.WriteLine("repeats"); 
                  counter++;
                }
                Console.WriteLine(series2[i]);  
            }

            Console.WriteLine();
            Console.WriteLine("Second series with modulo 524");

            int counter2 = 0;

            for(int i = 0; i < 1000; i++) {
                if(series2[i] == 42){
                  Console.WriteLine("repeats");
                  counter2++;
                }
                Console.WriteLine(series2[i]);  
            }

            Console.WriteLine("Series 2 repeats more often: " + (counter2 > counter));

        }
    }
}
