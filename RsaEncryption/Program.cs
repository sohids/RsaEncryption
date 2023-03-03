using System;
using System.Security.Cryptography;
using System.Text;

namespace  RsaEncryption
{
    public class Program
    {
        public static void Main()
        {
            const string publicKeyString = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCrWmaVPDI1hvYV5MT7oNJJCpA10xbLzI3CqbBOihQ7nPFPfP+zKgbS03kVUbXEOxH8CnASxSeAucHIg3q4n2akfJdpxW+FZTqoENNB6HS3+Pl5f9BX2OartDSnvXn0Zas9YnyMuIo+1+SOgkPz0aIL3PHtPoQw4qBEOtMXkM4RHwIDAQAB";
            const string newPassword = "Hello World";
            var publicKeyBytes = Convert.FromBase64String(publicKeyString);

            using var rsa = new RSACryptoServiceProvider();
            var publicKeyParameters = new RSAParameters
            {
                Modulus = publicKeyBytes,
                Exponent = new byte[] { 1, 0, 1 }
            };

            rsa.ImportParameters(publicKeyParameters);

            var uuid = Guid.NewGuid().ToString();
            var uuidBytes = Encoding.UTF8.GetBytes(uuid);
            var encryptableText = Encoding.UTF8.GetBytes(newPassword);
            var dataToEncrypt = new byte[uuidBytes.Length + encryptableText .Length];
            uuidBytes.CopyTo(dataToEncrypt, 0);
            encryptableText .CopyTo(dataToEncrypt, uuidBytes.Length);

            var encryptedData = rsa.Encrypt(dataToEncrypt, false);
            var encryptedString = Convert.ToBase64String(encryptedData);

            // Display the encrypted string and UUID
            Console.WriteLine("Encrypted string: {0}", encryptedString);
            //Console.WriteLine("Original String: {0}", encryptableText );

            byte[] bytes = Encoding.UTF8.GetBytes(encryptedString);
            var base64String = Convert.ToBase64String(bytes);

            Console.WriteLine("UUID: {0}", uuid);
        }
    }
}
