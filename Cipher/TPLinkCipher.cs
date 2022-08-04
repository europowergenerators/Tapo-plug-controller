using System;
using System.IO;
using System.Security.Cryptography;

namespace TapoPlugController.Cipher
{
    public class TPLinkCipher
    {
        private readonly byte[] iv = null;
        private readonly byte[] key = null;

        public static string MimeEncoder(byte[] Bytes)
        {
            return Convert.ToBase64String(Bytes, Base64FormattingOptions.InsertLineBreaks);
        }

        public TPLinkCipher(byte[] ByteArray1, byte[] ByteArray2)
        {
            iv = ByteArray2;
            key = ByteArray1;
        }

        public string Encrypt(string Data)
        {
            if (Data == null || Data.Length <= 0) { throw new ArgumentNullException("Data"); }
            byte[] Encrypted = null;
            using (Aes _AESAlg = Aes.Create())
            {
                _AESAlg.IV = iv;
                _AESAlg.Key = key;
                _AESAlg.Mode = CipherMode.CBC;
                _AESAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform _Encryptor = _AESAlg.CreateEncryptor(_AESAlg.Key, _AESAlg.IV);
                using (MemoryStream _MSEncrypt = new MemoryStream())
                {
                    using (CryptoStream _CSEncrypt = new CryptoStream(_MSEncrypt, _Encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter _SWEncrypt = new StreamWriter(_CSEncrypt))
                        {
                            _SWEncrypt.Write(Data);
                        }
                        Encrypted = _MSEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(Encrypted, Base64FormattingOptions.None);
        }
        public string Decrypt(string Data)
        {
            byte[] Encrypted = Convert.FromBase64String(Data);
            if (Encrypted == null || Encrypted.Length <= 0) { throw new ArgumentNullException("Data"); }
            string Decrypted = null;
            using (Aes _AESAlg = Aes.Create())
            {
                _AESAlg.IV = iv;
                _AESAlg.Key = key;
                _AESAlg.Mode = CipherMode.CBC;
                _AESAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform _Decryptor = _AESAlg.CreateDecryptor(_AESAlg.Key, _AESAlg.IV);
                using (MemoryStream _MSDecrypt = new MemoryStream(Encrypted))
                {
                    using (CryptoStream _CSDecrypt = new CryptoStream(_MSDecrypt, _Decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader _SRDecrypt = new StreamReader(_CSDecrypt))
                        {
                            Decrypted = _SRDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return Decrypted;
        }
    }
}
