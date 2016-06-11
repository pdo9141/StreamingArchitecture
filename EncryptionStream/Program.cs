using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // encrypt
            byte[] key = { 11, 22, 33, 44, 55, 66, 77, 88, 99, 100, 200, 123, 156, 34, 89, 93};
            byte[] iv = { 34, 24, 32, 44, 55, 60, 13, 9, 22, 55, 77, 90, 23, 12, 13, 11 };
            byte[] data = Encoding.UTF8.GetBytes("this is a text to encrypt");

            using (SymmetricAlgorithm algorithm = Aes.Create())
                using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
                    using (FileStream stream = new FileStream(@"c:\temp\text.txt", FileMode.CreateNew))
                        using (CryptoStream crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                            crypto.Write(data, 0, data.Length);

            // decrypt
            StreamReader sr = null;
            using (SymmetricAlgorithm algorithm = Aes.Create())
                using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
                    using (FileStream stream = new FileStream(@"c:\temp\text.txt", FileMode.Open))
                        using (CryptoStream crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                        {
                            sr = new StreamReader(crypto);
                            Console.WriteLine(sr.ReadToEnd());
                        }

            Console.ReadLine();
        }
    }
}
