using System;
using System.Collections;
using System.Security.Cryptography;

namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Es folgt Aufgabe a (Folge von Zufallszahlen): ");
            AufgabeA();
            Console.WriteLine();
            Console.WriteLine("Es folgt Aufgabe b (Tests): ");
            AufgabeB();
        }

        public static void AufgabeA() {
            Console.WriteLine("Pseudozufallszahlen (byte) mit RNGCryptServiceProvider");

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[10];
            rng.GetBytes(buffer);
            
            foreach(byte b in buffer) Console.WriteLine(b);
        }

        public static void AufgabeB() {
            LCG lcgA = new LCG(24, 42, 529, 42);
            LCG lcgB = new LCG(24, 42, 524, 42);
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] bufferProvider = new byte[1000];
            provider.GetBytes(bufferProvider);

            BitArray sequenceLCGA = new BitArray(lcgA.RandomSeries(1000));
            BitArray sequenceLCGB = new BitArray(lcgB.RandomSeries(1000));
            BitArray sequenceProvider = new BitArray(bufferProvider);

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("LCGa mit Modulo 529");
            TestSequence(sequenceLCGA);
            Console.WriteLine("--------------------------------------\n");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("LCGb mit Modulo 524");
            TestSequence(sequenceLCGB);
            Console.WriteLine("--------------------------------------\n");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("RNGCryptoServiceProvider");
            TestSequence(sequenceProvider);
            Console.WriteLine("--------------------------------------\n");
        }

        public static void TestSequence(BitArray sequence) {
            double pFreq = Tests.FrequencyTest(sequence);

            Console.WriteLine("FreqTest:");
            if (pFreq < 0.01)
                Console.WriteLine("There is evidence that sequence is NOT random");
            else
                Console.WriteLine("Sequence passes NIST frequency test for randomness");

            double pBlock = Tests.BlockTest(sequence, 8);
            Console.WriteLine("BlockTest:");

            if (pBlock < 0.01)
                Console.WriteLine("There is evidence that sequence is NOT random");
            else
                Console.WriteLine("Sequence passes NIST block test for randomness");

            double pRuns = Tests.RunsTest(sequence);
            Console.WriteLine("RunsTest:");

            if (pRuns < 0.01)
                Console.WriteLine("There is evidence that sequence is NOT random");
            else
                Console.WriteLine("Sequence passes NIST runs test for randomness");

            Console.WriteLine("p-Values: ");
            Console.WriteLine($"Freq: {pFreq} \tBlock: {pBlock} \tRuns: {pRuns}");
        }
    }

}
