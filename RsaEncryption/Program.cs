using System;
using System.Security.Cryptography;
using System.Text;

namespace RsaEncryption
{
    /// <summary>
    /// 1. Decode public key to byte array
    /// 2. Do SHA256 hash the new password
    /// 3. Do RSA encryption the password using public key
    /// 4. Encode the resultant encrypted string to base64 
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            const string publicKeyString = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCrWmaVPDI1hvYV5MT7oNJJCpA10xbLzI3CqbBOihQ7nPFPfP+zKgbS03kVUbXEOxH8CnASxSeAucHIg3q4n2akfJdpxW+FZTqoENNB6HS3+Pl5f9BX2OartDSnvXn0Zas9YnyMuIo+1+SOgkPz0aIL3PHtPoQw4qBEOtMXkM4RHwIDAQAB";
            var publicKeyBytes = Convert.FromBase64String(publicKeyString);
            
            //SHA Hash 
            var newPassword = GenerateSsh256("helloWorld");

            //Preparing the password for encryption: Converting to byte array 
            var newPasswordBytes = Encoding.UTF8.GetBytes(newPassword);

            //Importing the public key into the RSA algorithm 
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            //Encrypting the data 
            var encryptedData = rsa.Encrypt(newPasswordBytes, false);

            //Converting the resultant encrypted string to base64
            var encryptedString = Convert.ToBase64String(encryptedData);
            Console.WriteLine(encryptedString);
        }

        private static string GenerateSsh256(string password)
        {
            var bytesToHash = Encoding.UTF8.GetBytes(password);
            var sha256 = SHA256.Create();
            var hashValue = sha256.ComputeHash(bytesToHash);
            var hexString = BitConverter.ToString(hashValue).Replace("-", "");
            return hexString;
        }
    }
}
