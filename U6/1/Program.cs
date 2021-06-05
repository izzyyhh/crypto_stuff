using System;
using System.Security.Cryptography;
using System.Text;

class RSAOAEP
{

    static void Main()
    {
        try
        {
            //Create a UnicodeEncoder to convert between byte array and string.
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");

            byte[] encryptedData;
            byte[] decryptedData;
            byte[] encryptedData2;
            byte[] decryptedData2;

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSAParameters publicKey = RSA.ExportParameters(false);
                RSAParameters privateKey = RSA.ExportParameters(true);
                bool useOAEP = true;

                // 1a)
                // Verschlüsseln Sie die gleiche Nachricht Ihrer Wahl bei gleichem Schlüssel zweimal mit RSA OAEP.

                encryptedData = RSAEncrypt(dataToEncrypt, publicKey, useOAEP);
                encryptedData2 = RSAEncrypt(dataToEncrypt, publicKey, useOAEP);

                Console.WriteLine("Verschlüsselung mit gleichem Key.");
                Console.WriteLine();
                Console.WriteLine("1: {0}", Convert.ToBase64String(encryptedData));
                Console.WriteLine();
                Console.WriteLine("2: {0}", Convert.ToBase64String(encryptedData2));
                Console.WriteLine("\nDie Verschlüsselungen sind komplett verschieden, obwohl der Key gleich ist.");
                Console.WriteLine("Das liegt zum Einen am Padding und zum Anderen an die Hashfunktionen, die verwendet werden.");
                Console.WriteLine();

                // 1b)
                // Entschlüsseln Sie die beiden Ciphertexte wieder und prüfen Sie, ob Sie die ursprüngliche Nachricht erhalten.
                decryptedData = RSADecrypt(encryptedData, privateKey, useOAEP);
                decryptedData2 = RSADecrypt(encryptedData2, privateKey, useOAEP);
                Console.WriteLine("Decrypted 1: {0}", ByteConverter.GetString(decryptedData));
                Console.WriteLine("Decrypted 2: {0}", ByteConverter.GetString(decryptedData2));
                Console.WriteLine("Es kommt die ursprüngliche Nachricht raus: {0}", ByteConverter.GetString(decryptedData) == ByteConverter.GetString(decryptedData2));

                //1c)
                //  Modifizieren Sie einen der Ciphertexte und entschlüsseln Sie ihn.
                Console.WriteLine("Das erste Byte wird im Ciphertext verändert... ");
                encryptedData2[0] = encryptedData2[0] == 0 ? (byte)1 : (byte)0;

                Console.WriteLine("Nun wird die modifizierte Nachricht entschlüsselt...");
                byte[] decryptedModfiedData = RSADecrypt(encryptedData2, privateKey, useOAEP);
                Console.WriteLine("Decrypted Modified: {0}", ByteConverter.GetString(decryptedModfiedData));
                
                // Es wird eine Exception geworfen. Dazu gibt es eine Erklärung. Beim OAEP wird ein Padding
                // erzeugt und durch diesen hat man einen Integritätsschutz. Es werden Nullen rangehängt. Und
                // wenn beim entschlüsseln, die Nullen an der Nachricht nicht mehr existieren oder Anzahl ander ist,
                // wird ein Fehler geworfen. 


            }
        }
        catch (ArgumentNullException)
        {
            //Catch this exception in case the encryption did
            //not succeed.
            Console.WriteLine("Decrypting modified Data failed.");
        }
    }

    public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] encryptedData;
            //Create a new instance of RSACryptoServiceProvider.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            return encryptedData;
        }
        //Catch and display a CryptographicException  
        //to the console.
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }
    }

    public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
    {
        try
        {
            byte[] decryptedData;
            //Create a new instance of RSACryptoServiceProvider.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            return decryptedData;
        }
        //Catch and display a CryptographicException  
        //to the console.
        catch (CryptographicException e)
        {
            Console.WriteLine("\n" + e.ToString());

            return null;
        }
    }
}