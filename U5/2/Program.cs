using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger[] groupNums = new BigInteger[12345700]; 
            List<BigInteger> computedNums = new List<BigInteger>();


            for(int i = 0; i < groupNums.Length; i++) {
                groupNums[i] = i + 1;
            }

            BigInteger computed = 1;
            uint g = 2;
            uint counter = 0;

            while(true) {
                if(counter == 12345700) break;
                if(computedNums.Count == groupNums.Length) break;

                if(!groupNums.Contains(computed) || computedNums.Contains(computed)) {
                    // current generator is not the generator 
                    // clear computed nums and begin with another g
                    computedNums.Clear();
                    g++;
                    Console.WriteLine(g);
                    counter = 0;
                } else if(groupNums.Contains(computed)) {
                    Console.WriteLine("reset");
                    computedNums.Add(computed);
                    counter++;
                } else {
                    Console.WriteLine("rest");
                    computedNums.Clear();
                    g++;
                    counter = 0;
                }

                computed = computed * g % (groupNums.Length + 1);
            }

            Console.WriteLine("g is: {0}", g);
        }
    }
}
