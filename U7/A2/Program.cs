// Lösung von Ismail Halili und Valentin Kiefl

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AesCcm_Example
{
    class Program
    {
        public static void Main()
        {
            string original = "Here is some data to encrypt!";
            byte[] bytes = Encoding.ASCII.GetBytes(original);
            

            string nonce = CalculateNonce();
            byte[] bytesNonce = Encoding.ASCII.GetBytes(nonce);

            byte[] cipher = new byte[bytes.Length];
            byte[] tag = new byte[4];

            byte[] fakeCipher = new byte[bytes.Length];
            byte[] fakeTag = new byte[4];

            byte fcE;
            byte ftE;

            byte[] result = new byte[bytes.Length];
            byte[] resultAfterCipherModified = new byte[bytes.Length];
            byte[] resultAfterTagModified = new byte[bytes.Length];

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {

                // a 
                AesCcm myCcm= new AesCcm(myAes.Key);
                myCcm.Encrypt(bytesNonce, bytes, cipher, tag);

                Console.WriteLine(original);
                string cipherString = Encoding.ASCII.GetString(cipher);
                Console.WriteLine("Ciphertext: " + cipherString);
                
                // b
                myCcm.Decrypt(bytesNonce, cipher, tag, result);
                string resultString = Encoding.ASCII.GetString(result);
                Console.WriteLine("Decrypted Text / original Text: " + resultString);

                // c -> einkommentieren zum testen
                // schmeißt fehler da tags nicht übereinstimmen
                /* 
                fakeCipher = cipher;
                fcE = fakeCipher[0];
                fcE++;
                fakeCipher[0] = fcE;
                myCcm.Decrypt(bytesNonce, fakeCipher, tag, resultAfterCipherModified);
                string resultStringCipher = Encoding.ASCII.GetString(resultAfterCipherModified);
                Console.WriteLine("Result after modifing cipher: " + resultStringCipher);
                */

                // d -> einkommentieren zum testen
                // schmeißt fehler da tags nicht übereinstimmen
                /*
                fakeTag = tag;
                ftE = fakeTag[0];
                ftE++;
                fakeTag[0] = ftE;
                myCcm.Decrypt(bytesNonce, cipher, fakeTag, resultAfterTagModified);
                string resultStringTag = Encoding.ASCII.GetString(resultAfterTagModified);
                Console.WriteLine("Result after modifing tag: " + resultStringTag);
                */
            }
        }

        private static string CalculateNonce()
        {
            //Allocate a buffer
            var ByteArray = new byte[7];
            //Generate a cryptographically random set of bytes
            using (var Rnd = RandomNumberGenerator.Create())
            {
                Rnd.GetBytes(ByteArray);
            }
            //Base64 encode and then return
            return Convert.ToBase64String(ByteArray);
        }
    }
}