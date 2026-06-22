using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace APE115_ProyectoGT03GP4
{
    public static class Crypto
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

        /// <summary>
        /// Cifra texto usando AES-256.
        /// </summary>
        /// 

        public static string Encrypt(string texto)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
                using (var ms = new MemoryStream())
                {

                    ms.Write(aes.IV, 0, iv.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new System.IO.StreamWriter(cs))
                    {
                        sw.Write(texto);
                    }
                    return Convert.ToBase64String(ms.ToArray());

                }
            }
        }

        /// <summary>
        /// Descifra un texto en Base64 previamente cifrado con Encrypt.
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;

            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;

                // Extraemos el IV del inicio del array de bytes
                byte[] iv = new byte[aes.BlockSize / 8]; // 16 bytes para AES
                byte[] cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
