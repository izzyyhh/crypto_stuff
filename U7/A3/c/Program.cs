// Lösung von Ismail Halili und Valentin Kiefl
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace c
{
    class Program
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        static void Main(string[] args)
        {
            int times = 10;
            if(args.Length > 0) times = Int32.Parse(args[0]);

            int[] nValues = new int[times];

            for(int count = 0; count < times; count++) {
                nValues[count] = LoopTillCollision();
            }

            Console.WriteLine("average n = {0}, {1} runs", (int)nValues.Average(), times);
        }

        static int LoopTillCollision() {
            List<string> alreadyOccurredHashes = new List<string>();
            int counter = 0;

            while(true) {
                counter++;
                byte[] randomBytes = GetRandomBytes();
                string hashValue = SHA30(randomBytes);

                if(alreadyOccurredHashes.Contains(hashValue)) break;

                alreadyOccurredHashes.Add(hashValue);
            }

            Console.WriteLine("n = {0}", counter);

            return counter;
        }

        static byte[] GetRandomBytes() {
            byte[] randomBytes = new byte[4];
            rngCsp.GetBytes(randomBytes);

            return randomBytes;
        }

        static string SHA30(byte[] input) {

            using(SHA256 sha = SHA256.Create()) {
                byte[] hashBytes = sha.ComputeHash(input);
                BitArray bitArray = new BitArray(hashBytes);

                bool[] sha30HashBools = new bool[30];

                for(int count = 0; count < sha30HashBools.Length; count++) {
                    sha30HashBools[count] = bitArray[count];
                }

                return GetBitStringFromBools(sha30HashBools);
            }

        }

        static string GetBitStringFromBools(bool[] bools) {
            string bitString = "";

            foreach(bool b in bools) {
                bitString += b ? "1" : "0";
            }

            return bitString;
        }
    }
}
