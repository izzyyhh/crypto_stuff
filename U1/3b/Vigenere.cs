using System;
using System.IO;
using System.Collections.Generic;

namespace _3b
{
    public class Vigenere
    {
        public char[] alphabet;
        public string cipher;

        public Vigenere(string ciphertextFile){
            char[] alphabetChars = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', '.', ',', '-', ':', ';', Char.Parse("'"), '(', ')', '?', '!'};
            this.alphabet = alphabetChars;
            this.cipher = ReadCipherFromFile(ciphertextFile);
        }

        public string ReadCipherFromFile(string textFile){
            string file = textFile;

            if(File.Exists(file)){
                string text = File.ReadAllText(file);

                return text;

            } else {
                Console.WriteLine("Could not find file for ciphertext");
                return null;
            }
        }

        public void AttackVigenereCipher(int keylength){
            //get group for each letter from the key, in this exercise each 20th char
            string[] groups = GetGroups(keylength);
            string[,] decodedGroups = new string[groups.Length, alphabet.Length];

            //decode each group, with every possible shift (alphabet from 0-46)
            for (int groupIdx = 0; groupIdx < groups.Length; groupIdx++)
            {
                string currentGroup = groups[groupIdx];

                for(int shift = 0; shift < alphabet.Length; shift++){
                    string decodedGroup = DecodeGroup(currentGroup, shift);

                    decodedGroups[groupIdx, shift] = decodedGroup;
                }
            }

            //analyze decoded groups on their frequencies for char 'e' and 'space', take the decryption (right shift value), which resembles the german language
            //form the key with the result
            int[] keyShifts = new int[keylength];
            string key = "";

            for (int groupIdx = 0; groupIdx < keylength; groupIdx++)
            {
                //here the correct shift is returned with the help of the method 'GetCorrectShiftValueForGroup()'
                //this method includes the analysis of the freqs
                int correctShift = GetCorrectShiftValueForGroup(decodedGroups, groupIdx);

                //the right shift value is saved for every group
                keyShifts[groupIdx] = correctShift;
            }

            //this loop just forms the key
            foreach (int shift in keyShifts)
            {
                key += alphabet[shift];
            }

            string decodedCipher = DecodeCipher(cipher, keyShifts);
            
            Vigenere.CreateFileWithResult("attack_result.txt", key, decodedCipher);
        }

        public static void CreateFileWithResult(string file, string key, string result){

            using(StreamWriter writer = new StreamWriter(file)){
                writer.WriteLine("this is the key: " + key);
                writer.WriteLine();
                writer.WriteLine(result);
            }
        }

        public string DecodeCipher(string ciphertext, int[] keyShifts){
            string decodedCipher = "";

            for(int count = 0; count < ciphertext.Length; count += keyShifts.Length){
                for(int group = 0; group < keyShifts.Length; group++){
                    char currentChar = ciphertext[count++];
                    int position = Array.IndexOf(alphabet, currentChar);
                    int newPosition = (int) Vigenere.Nfmod(position - keyShifts[group], alphabet.Length);
                    
                    decodedCipher += alphabet[newPosition];
                }
            }

            return decodedCipher;
        }

        private int GetCorrectShiftValueForGroup(string[,] decodedGroups, int groupIdx){
            int correctShift = 0;
            ShiftAnalysisHelper[] shiftAnalysisArr = new ShiftAnalysisHelper[alphabet.Length];
            
                for(int shift = 0; shift < alphabet.Length; shift++){
                    //analyze each group with a dictionary, take the one with best match
                    //return its shift value
                    string decodedGroup = decodedGroups[groupIdx, shift];

                    Dictionary<char, int> letterFreqs = GetLetterFreqsFromGroup(decodedGroup);
                    // char mostFrequentChar = letterFreqs.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                    shiftAnalysisArr[shift] = new ShiftAnalysisHelper(letterFreqs['e'], letterFreqs[' '], shift);
                }

                ShiftAnalysisHelper bestMatch = GetHighestScoreMatch(shiftAnalysisArr);
                correctShift = bestMatch.shift;

                return correctShift;
        }

        //here the the helper object with the highest score is returned, which is
         //most likely to represent the german language
        private ShiftAnalysisHelper GetHighestScoreMatch(ShiftAnalysisHelper[] arr){
            ShiftAnalysisHelper helper = new ShiftAnalysisHelper(0, 0, 0);
            foreach (var item in arr)
            {
                if( item == null ) continue;

                if(helper.Score() < item.Score()){
                    helper = item;
                }
            }

            return helper;
        }

        //this returns a dictionary which is used for the freq-attack
        //the letter and its count is saved in it
        private Dictionary<char, int> GetLetterFreqsFromGroup(string group){
            Dictionary<char, int> letterFreqs = new Dictionary<char, int>();

            foreach (char c in alphabet)
            {
                letterFreqs.Add(c, 0);
            }

            foreach (char c in group)
            {
                if(letterFreqs.ContainsKey(c)){
                    letterFreqs[c]++;
                }
            }

            return letterFreqs;
        }
        //decode a group with a certain shiftvalue, it is portant to substract the shift from the current value
        //because in the encryption this value is added
        private string DecodeGroup(string group, int shiftValue){
            string decodedGroup = "";

            foreach (char c in group)
            {
                int position = Array.IndexOf(alphabet, c);
                int newPosition = (int) Vigenere.Nfmod(position - shiftValue, alphabet.Length);

                decodedGroup += alphabet[newPosition];
            }

            return decodedGroup;
        }
        //getting a group means, getting each 20th char in this exercise, this group gets analyzed later on
        private string[] GetGroups(int keylength){
            string[] groups = new string[keylength];

            for(int count = 0; count < keylength; count++){
                string group = "";

                for(int cipherIdx = count; cipherIdx < cipher.Length; cipherIdx += keylength){
                    group += cipher[cipherIdx];
                }
                groups[count] = group;
            }

            return groups;
        }
        //self written modulo, because c# does not exlude negative modulos results
        static double Nfmod(float a,float b){
            return a - b * Math.Floor(a / b);
        }
    }
}

