using System.Security.Cryptography;
using System.Text;
using System;

namespace Wallet.Application.Utilities
{
    public static class CryptographyUtils
    {
        public static string Encrypt(string text)
        {
            string passPhrase = "P0O9I8U7Y6T5R4E3W2Q1"; // Encryption paraphrase
            string saltValue = "q$%^&*(iuytZXCVBNM<>LKJHGF6Fdsa@1`@3f&bgh(*90l';)9-78&65tfCS2321Rc54%$#8Hgy^5vCfds^r5(0*l;Y699)9lk&7-JhfD7)olKjijh";
            string hashAlgorithm = "SHA1"; // Encryption hashing algorithm
            int passwordIterations = 20; // Encryption password interaction
            string initVector = "?o*M^ygf98&j%ds@";
            int keySize = 256;

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);

            var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new System.IO.MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
    }
}
