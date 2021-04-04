using System;
using System.Collections;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testvector = {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0};

            LFSR lfsr = new LFSR(testvector, 60);

            int[] result = lfsr.Steps(100);

            foreach(int bit in result) Console.Write(bit + ", ");
        }
    }
}
