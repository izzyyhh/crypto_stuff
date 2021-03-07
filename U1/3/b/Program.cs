using System;

namespace _3b
{
    class Program
    {
        static void Main(string[] args)
        {
            Vigenere v = new Vigenere("ciphertext.txt");
            int keylength = 20;
            v.AttackVigenereCipher(keylength);

            Console.WriteLine("The result of the attack is saved in the file 'attack_result.txt'");
        }
    }
}
