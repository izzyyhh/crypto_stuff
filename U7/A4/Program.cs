// Lösung von Ismail Halili und Valentin Kiefl
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace A4
{
    class Program
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static HMACSHA256 hmac;

        static void Main(string[] args)
        {
            // a) hmac256 hash
            // with random key
            byte[] secretKey = new byte[64];
            rng.GetBytes(secretKey);
            
            hmac = new HMACSHA256(secretKey);

            string message = "Hello world!";
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            BitArray messageBits = new BitArray(messageBytes);

            byte[] tag = hmac.ComputeHash(messageBytes);
            byte[] tag1 = hmac.ComputeHash(messageBytes);

            // zwei tags von der gleichen nachricht verglichen
            // Sollwert: True
            Console.WriteLine("Verification, 2 gleiche Nachrichten haben gleichen Tag:");
            Console.WriteLine(tag1.SequenceEqual(tag));
            
            // b) nachricht verändern
            // nun wird Nachricht um ein Bit verändert
            // erstes bit flippen
            messageBits[0] = messageBits[0] ? false : true;
            byte[] modifiedMessageBytes = BitArrayToByteArray(messageBits);
            byte[] tagOfModifiedMsg = hmac.ComputeHash(modifiedMessageBytes);
            // Sollwert: False, da Nachricht verändert wurde
            Console.WriteLine("Verification, veränderte Nachricht:");
            Console.WriteLine(tagOfModifiedMsg.SequenceEqual(tag)); 

            // c) tag verändern, erstes bit flippen
            BitArray tagBits = new BitArray(tag);
            tagBits[0] = tagBits[0] ? false : true;
            byte[] modifiedTagBytes = BitArrayToByteArray(tagBits);
            // Sollwert: False, da Tag verändert wurde
            Console.WriteLine("Verification, veränderter Tag:");
            Console.WriteLine(modifiedTagBytes.SequenceEqual(tag));
        }

        
        

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
    }
}
