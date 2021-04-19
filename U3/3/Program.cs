using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("running ...\n");

            double average10K = LoopForStats(100, 10000);
            double average20K = LoopForStats(100, 20000);
            double average100K = LoopForStats(50, 100000);
            double average500K = LoopForStats(50, 500000);
            double average990K = LoopForStats(50, 990000);
            double average1M = LoopForStats(50, 1000000);


            Console.WriteLine($"for n: 10.000, average: {average10K}, expected: {Math.Sqrt(2*10000)}");
            Console.WriteLine($"for n: 20.000, average: {average20K}, expected: {Math.Sqrt(2*20000)}");
            Console.WriteLine($"for n: 100.000, average: {average100K}, expected: {Math.Sqrt(2*100000)}");
            Console.WriteLine($"for n: 500.000, average: {average500K}, expected: {Math.Sqrt(2*500000)}");
            Console.WriteLine($"for n: 990.000, average: {average990K}, expected: {Math.Sqrt(2*990000)}");
            Console.WriteLine($"for n: 1.000.000, average: {average1M}, expected: {Math.Sqrt(2*1000000)}");

            Console.WriteLine("\nfinished!");
        }

        static double LoopForStats(int times, int n) {
            // loop to get average
            List<int> results = new List<int>();

            for(int i = 0; i < times; i++) {
                int result = LoopTillCollision(n);
                if(result != -1) results.Add(result);
            }

            return results.Average();
        }

        static int LoopTillCollision(int n) {
            int[] alreadyOccurred = new int[n];
            alreadyOccurred[0] = RandomNumberGenerator.GetInt32(1, n + 1);

            for(int i = 1; i < alreadyOccurred.Length; i++) {
                // create random int
                int random =  RandomNumberGenerator.GetInt32(1, n + 1);
                // check if it is occured, then return amount of elements drawn
                if(alreadyOccurred.Contains(random)) return i + 1;
                // if not occurred already, then add to array
                alreadyOccurred[i] = random;
            }

            // if loop is done without collision, then it takes over n values to loop up till collision
            // return -1 for no collision
            Console.WriteLine("No collision occured!");
            return -1;
        }
    }
}
