using System;
using System.Collections.Generic;

namespace _2b
{
    class Program
    {
        static void Main(string[] args)
        {
            //caesar++ 
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            //alle moeglichen keys in einem array, da der key unbekannt ist
            int[] keys = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25 };
            //der ciphertext von der angabe, der entshluesselt werden muss
            int[] ciphertext = { 20,17,9,24,17,10,20,5,19,24,23,17,4,16,23,11,10,23,21,1,2,17,6,6,10 };
            //entschluesselte nachrichten, mit allen keys entschluesselt (brute-force)
            //list mit decrypted arrays
            List<int[]> messagesListInt = new List<int[]>();
            List<string> messagesListString = new List<string>();

            for(int count = 0; count < keys.Length; count++){
                int[] decrypted = new int[ciphertext.Length];

                //hier wird entschluesselt
                //der es wird der shift value abgezogen, da in der verschluesselung dazugerechnet wird
                for(int ci = 0; ci < ciphertext.Length; ci++){
                    decrypted[ci] = (int) nfmod((ciphertext[ci] - keys[count]), 26);
                }

                messagesListInt.Add(decrypted);
            }

            //hier wird die nachricht geformt mithilfe des alphabets, da bisher nur zahlen gespeichert wurden 
            foreach(int[] intMessage in messagesListInt){
                string stringMessage = "";

                foreach(int mi in intMessage){
                    stringMessage += alphabet[mi];
                }

                messagesListString.Add(stringMessage);
            }

            //ausgeben jeder nachricht
            foreach(string message in messagesListString){
                Console.WriteLine(message);
            }

            //loesung: dashatdochganzgutgeklappt
            Console.WriteLine();
            Console.WriteLine("Die Loesung lautet: " + messagesListString[16]);
        }
        
        static double nfmod(float a,float b)
        {
            return a - b * Math.Floor(a / b);
        }
    }
}
