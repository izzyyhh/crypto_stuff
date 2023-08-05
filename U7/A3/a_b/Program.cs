// Lösung von Ismail Halili und Valentin Kiefl
using System;
using System.Text;
using System.Security.Cryptography;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nummer 3 a und b, Hashing");
            
            string text1 = "hello";
            string text2 = "hallo";
            string hash1 = "";
            string hash2 = "";

            if(args.Length > 0) {
                text1 = args[0];
            }

            using(SHA256 sha = SHA256.Create()) {
                byte[] hashBytes1 = sha.ComputeHash(Encoding.ASCII.GetBytes(text1));
                byte[] hashBytes2 = sha.ComputeHash(Encoding.ASCII.GetBytes(text2));

                foreach(byte b in hashBytes1) {
                    hash1 += b.ToString("x2");
                }

                foreach(byte b in hashBytes2) {
                    hash2 += b.ToString("x2");
                }
            }

            Console.WriteLine("Eingabe 1: \t{0}", text1);
            Console.WriteLine("Ausgabe 1: \t{0}", hash1);
            Console.WriteLine("Eingabe 2: \t{0}", text2);
            Console.WriteLine("Ausgabe 2: \t{0}", hash2);
        }
    }
}
