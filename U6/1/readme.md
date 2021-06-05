Die Beantwortung der Fragen finden Sie im Code als WriteLines und Kommentaren.  
  
Der Output vom Code und Comments: 


// 1a)  
// Verschlüsseln Sie die gleiche Nachricht Ihrer Wahl bei gleichem Schlüssel zweimal mit RSA OAEP.  
Verschlüsselung mit gleichem Key.  
  
1: PmwehjEsfKc7XsKY5WDWCakucztNPSkMLLSsakvPSkRw1f9WKJyFPX9QRZXvdpQLDsJ2b4HE34N+5Y7KRwAPqvQkLwLkdfUm67wh/cHe7HaI360rKixIs/tadzVggQUVvwfUi2PIijHCW/4Xq4ODG/CeAdDj7w3m4alc9NsimqM=  
  
2: gIpDKyZvaNMr2QQOBB0jDAoZ0zEtvccjS2HfNz5+fyndbvzRx9DpvBwwrB30tfWNRFSpTHxk0bUmqYWpl3OnEKDJnu9fYX3NZ/PA3/WYY5YFFqH846L+3UQEd095UQ+WkyrDLPKVlI1lodeFTOUayOM9rYlkNSjnFqlttN+fNJY=  

Die Verschlüsselungen sind komplett verschieden, obwohl der Key gleich ist.  
Das liegt zum Einen am Padding und zum Anderen an die Hashfunktionen, die verwendet werden.  

// 1b)  
// Entschlüsseln Sie die beiden Ciphertexte wieder und prüfen Sie, ob Sie die ursprüngliche Nachricht erhalten.  
Decrypted 1: Data to Encrypt  
Decrypted 2: Data to Encrypt  
Es kommt die ursprüngliche Nachricht raus: True
  
//1c)  
//  Modifizieren Sie einen der Ciphertexte und entschlüsseln Sie ihn.  
  
Das erste Byte wird im Ciphertext verändert...
Nun wird die modifizierte Nachricht entschlüsselt...  
  
// Es wird eine Exception geworfen. Dazu gibt es eine Erklärung. Beim OAEP wird ein Padding  
// erzeugt und durch diesen hat man einen Integritätsschutz. Es werden Nullen rangehängt. Und  
// wenn beim entschlüsseln, die Nullen an der Nachricht nicht mehr existieren oder Anzahl ander ist,  
// wird ein Fehler geworfen. 

System.Security.Cryptography.CryptographicException: Cryptography_OAEPDecoding
   at Internal.NativeCrypto.CapiHelper.DecryptKey(SafeKeyHandle safeKeyHandle, Byte[] encryptedData, Int32 encryptedDataLength, Boolean fOAEP, Byte[]& decryptedData)
   at System.Security.Cryptography.RSACryptoServiceProvider.Decrypt(Byte[] rgb, Boolean fOAEP)
   at RSAOAEP.RSADecrypt(Byte[] DataToDecrypt, RSAParameters RSAKeyInfo, Boolean DoOAEPPadding) in D:\Git\Gitlab\Krypto\U6\1\Program.cs:line 120
Decrypting modified Data failed.
