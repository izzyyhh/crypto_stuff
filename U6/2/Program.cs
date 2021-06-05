using System;
using System.Security.Cryptography;
using System.Text;

class RSASignature
{
    static void Main()
    {
        try
        {
            // Create a UnicodeEncoder to convert between byte array and string.
            ASCIIEncoding ByteConverter = new ASCIIEncoding();

            string dataString = "Die Nachricht meiner Wahl!";

            // Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] signedData;
            byte[] signedData1;

            // Create a new instance of the RSACryptoServiceProvider class
            // and automatically create a new key-pair.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            // Export the key information to an RSAParameters object.
            // Export the private key for signing.
            RSAParameters Key = RSAalg.ExportParameters(true);

            signedData = HashAndSignBytes(originalData, Key);
            signedData1 = HashAndSignBytes(originalData, Key);

            // 2a) Erzeugen Sie sich zu einer Nachricht Ihrer Wahl zweimal eine Signatur.
            Console.WriteLine("Signatur 1: {0}", Convert.ToBase64String(signedData));
            Console.WriteLine();
            Console.WriteLine("Signatur 2: {0}", Convert.ToBase64String(signedData1));
            Console.WriteLine();
            Console.WriteLine();

            // 2b) Überprüfen Sie die erhaltene Signatur auf Gültigkeit.
            if(VerifySignedHash(originalData, signedData, Key))
            {
                Console.WriteLine("Erfolgreich verifiziert.");
            }
            else
            {
                Console.WriteLine("Signatur ungueltig. Verifikation fehlgeschlagen.");
            }

            // 2c) Modifizieren Sie die signierte Nachricht und bestätigen Sie, dass die Signatur nun nicht mehr gültig ist.
            Console.WriteLine("Es wird die Nachricht modifiziert...");
            // Ein Zeichen wird geloescht.
            byte[] modifiedData = ByteConverter.GetBytes(dataString.Substring(0, dataString.Length - 1));
            Console.WriteLine("Es wird signiert und anschliessend verifiziert...");

            signedData = HashAndSignBytes(modifiedData, Key);

            if(VerifySignedHash(originalData, signedData, Key))
            {
                Console.WriteLine("Erfolgreich verifiziert.");
            }
            else
            {
                Console.WriteLine("Die Nachricht wurde modifiziert. Verifikation fehlgeschlagen...");
            }
        }
        catch(ArgumentNullException)
        {
            Console.WriteLine("Die Nachricht wurde modifiziert. Verifikation fehlgeschlagen...");
        }
    }
    public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
    {
        try
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportParameters(Key);

            // Hash and Sign mit SHA256 und RSA
            return RSAalg.SignData(DataToSign, SHA256.Create());
        }
        catch(CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }
    }

    public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
    {
        try
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportParameters(Key);

            return RSAalg.VerifyData(DataToVerify, SHA256.Create(), SignedData);
        }
        catch(CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return false;
        }
    }
}